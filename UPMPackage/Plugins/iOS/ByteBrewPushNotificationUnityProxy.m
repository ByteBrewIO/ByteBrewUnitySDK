//
//  ByteBrewPushNotificationUnityProxy.m
//  ByteBrewiOSPlugin
//
//  Created by Cameron Hozouri on 12/18/21.
//  Copyright © 2021 Mad Cow Studios Inc. All rights reserved.
//

#import <UIKit/UIKit.h>
#import <objc/runtime.h>
#import "ByteBrewHandler.h"
#import "UnityAppController.h"

@implementation UIApplication(ByteBrewPush)

+ (void) load {

    NSUserDefaults *preferences = [NSUserDefaults standardUserDefaults];
    
    NSString* pushSetting = [preferences stringForKey:@"BB_PUSH_SET"];
    //Check for if the developer wants push notifications, this will either be nil or 0
    //We do have the possibility of them turning this on and then off which will result
    //in the next next session setting the preference to the perfered push setting
    //We deal with this by calling all original swizzled methods so any other push notification platform
    //Will get the notification as well and checking that
    //The push notifications that are only sent by ByteBrew are tracked by bytebrew sdk
    
    if(pushSetting != nil) {
        if([pushSetting isEqualToString:@"1"]) {
            //We meed to check if the app is being ran in debug, unit tests, or testflights
            //This prevents from if a push notification is received it wont crash UnityAppController
            NSString *overrideAnalytics = [[NSBundle mainBundle] objectForInfoDictionaryKey:@"PushTestConfig"];
            if(([self isRunningTests] || [self isTestFlight]) && (!overrideAnalytics || [overrideAnalytics isEqualToString:@"NO"])) {
                NSLog(@"ByteBrew Push: Testing build has no push notification analytics. This will disable once in production environment or overwrtiten.");
                return;
            }
            [self SwizzleLaunch];
        }
    }
    
}

+(BOOL) isRunningTests {
    NSDictionary* environment = [[NSProcessInfo processInfo] environment];
    NSString* injectBundle = environment[@"XCInjectBundle"];
    return [[injectBundle pathExtension] isEqualToString:@"xctest"];
}

+(BOOL) isTestFlight {
    NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
    NSString *receiptURLString = [receiptURL path];
    NSLog(@"ByteBrew Testing: %@", receiptURLString);
    return ([receiptURLString rangeOfString:@"sandboxReceipt"].location != NSNotFound);
}

+ (void) SwizzleLaunch {
    
    NSLog(@"ByteBrew Swizzling didFinishLaunchingWithOptions");
    Class byteApplicationClass = [self class];
    
    SEL originalFinishLaunchSEL = @selector(application:didFinishLaunchingWithOptions:);
    SEL extendedFinishLaunchSEL = @selector(extendedApplication:didFinishLaunchingWithOptions:);
    
    //Original App delegate method
    Method originalFinishLaunchMethod;
    
    // Swizzling the method
    Method extendedFinishLaunchMethod = class_getInstanceMethod(byteApplicationClass, extendedFinishLaunchSEL);
    
    
    if(class_getInstanceMethod([UnityAppController class], originalFinishLaunchSEL) != nil) {
        class_addMethod([UnityAppController class], extendedFinishLaunchSEL, method_getImplementation(extendedFinishLaunchMethod), method_getTypeEncoding(extendedFinishLaunchMethod));

        extendedFinishLaunchMethod = class_getInstanceMethod([UnityAppController class], extendedFinishLaunchSEL);
        
        originalFinishLaunchMethod = class_getInstanceMethod([UnityAppController class], originalFinishLaunchSEL);
        
        method_exchangeImplementations(originalFinishLaunchMethod, extendedFinishLaunchMethod);
        
        NSLog(@"ByteBrew Push Notifications: Finished Swizzling current didFinishLaunchingWithOptions implementation");

    } else {
        class_addMethod([UnityAppController class], originalFinishLaunchSEL, method_getImplementation(extendedFinishLaunchMethod), method_getTypeEncoding(extendedFinishLaunchMethod));
        
        NSLog(@"ByteBrew Push Notifications: No current implementation adding custom didFinishLaunchingWithOptions");

    }
}

- (BOOL)extendedApplication:(UIApplication *)application
didFinishLaunchingWithOptions:(NSDictionary<UIApplicationLaunchOptionsKey, id> *)launchOptions {
    NSLog(@"Setting Up ByteBrew Push");
    
    //We start a small part of push notifications to basically swizzle small parts for grabbing push payloads and for useful analytics
    //Other Parts of the SDK will be initialized when the scene or SDKs start
    [ByteBrewHandler LowLevelPushStart];
    
    if([self respondsToSelector:@selector(extendedApplication:didFinishLaunchingWithOptions:)])
        return [self extendedApplication:application didFinishLaunchingWithOptions:launchOptions];
    
    return YES;
}

@end
