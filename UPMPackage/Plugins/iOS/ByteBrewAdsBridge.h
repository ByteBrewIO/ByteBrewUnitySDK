// ByteBrewAdsBridge.h
// Public C API for Unity to P/Invoke (symbols must be visible)

#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

// Receiver: GameObject name that will receive all callbacks
__attribute__((visibility("default"))) void ByteBrewAds_SetUnityReceiver(const char* gameObjectName);

// Ads SDK entry points
__attribute__((visibility("default"))) void ByteBrewAds_InitAds(void);
__attribute__((visibility("default"))) void ByteBrewAds_FlushAllAds(void);
__attribute__((visibility("default"))) void ByteBrewAds_LoadInterstitialCrossPromoAd(const char* adUnitID);
__attribute__((visibility("default"))) void ByteBrewAds_LoadInterstitialCrossPromoAdWithCtrlOnly(const char* adUnitID, bool ctrlOnly);
__attribute__((visibility("default"))) void ByteBrewAds_LoadRewardedCrossPromoAd(const char* adUnitID);
__attribute__((visibility("default"))) void ByteBrewAds_LoadRewardedCrossPromoAdWithCtrlOnly(const char* adUnitID, bool ctrlOnly);
__attribute__((visibility("default"))) void ByteBrewAds_ShowInterstitialCrossPromoAd(const char* adUnitID);
__attribute__((visibility("default"))) void ByteBrewAds_ShowInterstitialCrossPromoAdWithCtrlOnly(const char* adUnitID, bool ctrlOnly);
__attribute__((visibility("default"))) void ByteBrewAds_ShowRewardedCrossPromoAd(const char* adUnitID);
__attribute__((visibility("default"))) void ByteBrewAds_ShowRewardedCrossPromoAdWithCtrlOnly(const char* adUnitID, bool ctrlOnly);

// Queries / helpers
__attribute__((visibility("default"))) bool ByteBrewAds_IsInitialized(void);
__attribute__((visibility("default"))) bool ByteBrewAds_IsCrossPromoAdLoaded(const char* adUnitID);
__attribute__((visibility("default"))) bool ByteBrewAds_IsCrossPromoAdLoadedWithCtrlOnly(const char* adUnitID, bool ctrlOnly);
__attribute__((visibility("default"))) bool ByteBrewAds_IsAdShowing(void);

#ifdef __cplusplus
} // extern "C"
#endif
