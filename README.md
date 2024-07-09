# ByteBrewUnitySDK
 ByteBrew is the free easy to integrate Unity SDK that provides your game with Real-Time Analytic Tracking, Monetization Tracking, Attribution, ATT (App Tracking Transparency) Unity Custom Completion Handler, Remote Configs and A/B Testing.

 ## Summary
 A product of ByteBrew’s all-in-one platform, the ByteBrew SDK gives you instant integration to the following key features and tools for your games on the ByteBrew Dashboard:

Real-Time Analytics:
 - Engagement Analytics: Automatically start tracking your game’s key performance indicators such as new user events, DAU, session length, playtime, and retention reporting
 - Custom Filtering: Breakdown and segment your players using custom filters such as; device type, geography, build version, and date ranges

Push Notifications:
 - Send Cross-Platform Cloud Notifications to your players around the globe with ByteBrew's Push Notifications.

Player Progression:
 - Game level statistics tracking to breakdown level wins, fails, scores and more.
 - Progression funneling system: Monitor your user's progression and set up custom event funnels to visually track how your player's progression through your games. 
 -Locate key churn points in your games by tracking how long players take to advance through events.

Monetization:
 - Start tracking your game’s in-app purchases with real value using our Receipt Validation tool to turn your game into a business.  
 - Track and visualize your game's in-game advertising revenue from any ad networks you monetize. 

Attribution: 
 - ByteBrew attribution is integrated with the top mobile ad networks for basic attribution and SKAdnetwork with more added each week. Track every impression, click, and install across all your running advertising campaigns down to creative level detail.

Remote Configs: 
 - Make in-game edits to your game remotely without having to upload a new build to the app stores using the following types of configurations.
 - Single Remote Configs: Gives you the ability to change individual custom values in-game such as: boss enemy health, player statistics, or level difficulty
 - Grouped Configs: This advanced feature gives you the ability to change multiple in-game values on an interval period. This can be used for making a rotating shop item, or a daily reward special for your game

A/B Testing: 
 - This feature gives you the ability to change multiple in-game values by which test a user is attached to. This can be used to track multiple KPIs that can effect their performance based on the config set for each user.

App Tracking Transparency Handler:
 - Custom iOS 14 App Tracking Transparency solution to manage user tracking consent and deliver you ad tracking status on users. 

 [ByteBrew Dashboard](https://dashboard.bytebrew.io): Setup your mobile games and view your real-time analytics.

 ## Installation
 You can download the Unity package from this github to install straight into your project.

 Follow the [documentation](https://docs.bytebrew.io/sdk/unity) to continue integration.

 Create your ByteBrew account to access the [dashboard](https://dashboard.bytebrew.io/register), or if you do have an account login [here](https://dashboard.bytebrew.io/login).

## Updating The SDK
Remove the current SDK "ByteBrewSDK" from your Assets folder.
Then download the Unity package from this github to install straight into your project, and re-input your SDK Keys.


 ## Community
 Join our Community of Developers on Discord [here](https://discord.gg/sAp4f3tJte)! 


 ## ChangeLog
### 0.1.6
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor WebGL update.
 - Notes:
    - To Properly update the SDK from a version pre 0.1.3, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. From 0.1.3 and forward all you need to do is reimport the SDK. 
 
### 0.1.5
 - Update to ByteBrew Unity SDK
 - Updates:
    - WebGL Support.
 - Notes:
    - To Properly update the SDK from a version pre 0.1.3, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. From 0.1.3 and forward all you need to do is reimport the SDK. 

### 0.1.4
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor iOS updates.
    - iOS updated for Privacy Manifest.
 - Notes:
    - To Properly update the SDK from a version pre 0.1.3, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. From 0.1.3 and forward all you need to do is reimport the SDK. 
 
### 0.1.3
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor Android & iOS updates.
    - Updates for Google Ads Attribution.
    - iOS updated for ATT.
    - Added new revenue parameter for track ad event.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. 
 
### 0.1.2
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor Android & iOS updates.
    - Updates for Apple Search Ads, and Google Ads Attribution.
    - Android - lowered minimum Android API level to API 19 (Android 4.4).
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. 

### 0.1.1
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor android updates.
    - Update for prioritizing delivery of test push notifications.
    - Other minor push notification updates.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. 

### 0.1.0
 - Update to ByteBrew Unity SDK
 - Fixes:
    - Minor dependency fixes
 - Updates:
    - Update for new A/B Testing features and fixes.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys. 

### 0.0.9
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor updates to code(android).
    - Added error on run if the SDK Key or GameID input is empty.
    - Added IsByteBrewInitialized method.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys.
 
 ### 0.0.8
 - Update to ByteBrew Unity SDK
 - Updates:
    - Minor updates to legacy code(android).
 - Fixes:
    - Unity purchase validation bug patch.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys.

 ### 0.0.7
 - Update to ByteBrew Unity SDK
 - Additions:
    - Update with Purchase Validation Callback.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys.
 
 ### 0.0.6.1
 - Update to ByteBrew Unity SDK
 - Fixes:
    - Unity Android SDK - Fix for "TrackGoogleInAppPurchaseEvent" to parse transaction receipt.
 - Changes:
    - SDK change for Android with Google Policy Compliance Update.
 - Notes:
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys.

 ### 0.0.6
 - Update to ByteBrew Unity SDK
 - Additions:
    - Push Notifications: Launch of Push Notifications in ByteBrew with a one line of code integration in the SDK! Method "StartPushNotifications".
    - Custom User Data Attributes: Add Key-Value Pair attributes to your user for segmentation use. Method name "SetCustomUserDataAttribute".
    - Remote Config Read Check: New method called "HasRemoteConfigsBeenSet" to check whether remote configs have been retrieved without a callback, boolean will return.
    - Get User ID: Ability to retrieve the ByteBrew User ID for your reference, mostly to use for push notifications. Method name "GetUserID".
 - Fixes:
    - Unity SDK - Fix for prefab not saving correctly in scene when adding
    - Unity SDK - Spelling Error on IAP Purchase Tracking method.
- Changes:
    -Unity SDK - Updated with new method for "TrackAdEvent" which utilizes a enum value instead of string for placementType parameter. Future ByteBrew SDKs will remove the older version of "TrackAdEvent".
- Notes:
    - Checkout documentation for further use of Push Notifications and more.
    - To Properly update the SDK, make sure to remove the whole "ByteBrewSDK" folder and reimport the new SDK, and don't forget to re-input your SDK Keys.

 ### 0.0.5
 - Update to ByteBrew Unity SDK
 - Fixes:
    - Android - Ability to test your game in Unity Editor without ByteBrew Initializing
    - iOS - Ability to test your game in Unity Editor without ByteBrew Initializing
 ### 0.0.4
 - First launch of ByteBrew SDK availible for Unity Developers




## Footer
Copyright and Property of [ByteBrew](https://bytebrew.io). Use of ByteBrew SDK are subject to ByteBrew's [Terms of Service](https://docs.bytebrew.io/BBSettings/termsservice) and [Privacy Policy](https://docs.bytebrew.io/BBSettings/privacypolicy).

ByteBrew SDK is licensed under MIT
