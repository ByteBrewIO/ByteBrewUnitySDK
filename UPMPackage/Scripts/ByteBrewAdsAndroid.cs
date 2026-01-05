#if UNITY_ANDROID && !UNITY_EDITOR
using UnityEngine;

namespace ByteBrewSDK
{
    public static class ByteBrewAdsAndroid
    {
        static bool _wiredTried;
        static bool _wiredOK;

        public static void WireListenersIfNeeded()
        {
            if (_wiredTried) {
                return;
            }
            _wiredTried = true;

            try
            {
                ByteBrewAndroidHandler.SetAdInitEventListener (new BBAndroidAdInitProxy());
                ByteBrewAndroidHandler.SetInterstitialAdLoadEventListener (new BBAndroidInterstitialAdLoadProxy());
                ByteBrewAndroidHandler.SetInterstitialAdEventListener (new BBAndroidInterstitialAdEventProxy());
                ByteBrewAndroidHandler.SetRewardedAdLoadEventListener (new BBAndroidRewardedAdLoadProxy());
                ByteBrewAndroidHandler.SetRewardedAdEventListener (new BBAndroidRewardedAdEventProxy());
                _wiredOK = true;
                Debug.Log("[ByteBrewAds][Android] Listeners wired.");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("[ByteBrewAds][Android] Wire failed: " + ex);
                _wiredOK = false;
            }
        }

        public static void InitializeAds()                                              => ByteBrewAndroidHandler.InitializeAds();
        public static bool IsInitialized()                                              => ByteBrewAndroidHandler.IsAdsInitialized();
        public static void FlushAllAds()                                                => ByteBrewAndroidHandler.FlushAllAds();
        public static void LoadInterstitialCrossPromoAd(string adUnitID)                => ByteBrewAndroidHandler.LoadInterstitialCrossPromoAd(adUnitID);
        public static void LoadInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) => ByteBrewAndroidHandler.LoadInterstitialCrossPromoAd(adUnitID, ctrlOnly);
        public static void LoadRewardedCrossPromoAd(string adUnitID)                    => ByteBrewAndroidHandler.LoadRewardedCrossPromoAd(adUnitID);
        public static void LoadRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)     => ByteBrewAndroidHandler.LoadRewardedCrossPromoAd(adUnitID, ctrlOnly);
        public static bool IsCrossPromoAdLoaded(string adUnitID)                        => ByteBrewAndroidHandler.IsCrossPromoAdLoaded(adUnitID);
        public static bool IsCrossPromoAdLoaded(string adUnitID, bool ctrlOnly)         => ByteBrewAndroidHandler.IsCrossPromoAdLoaded(adUnitID, ctrlOnly);
        public static bool IsAdShowing()                                                => ByteBrewAndroidHandler.IsAdShowing();
        public static void ShowInterstitialCrossPromoAd(string adUnitID)                => ByteBrewAndroidHandler.ShowInterstitialCrossPromoAd(adUnitID);
        public static void ShowInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) => ByteBrewAndroidHandler.ShowInterstitialCrossPromoAd(adUnitID, ctrlOnly);
        public static void ShowRewardedCrossPromoAd(string adUnitID)                    => ByteBrewAndroidHandler.ShowRewardedCrossPromoAd(adUnitID);
        public static void ShowRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)     => ByteBrewAndroidHandler.ShowRewardedCrossPromoAd(adUnitID, ctrlOnly);
    }

    // ---- Mapping AndroidJavaObject -> common DTO
    internal static class BBAndroidAdMap
    {
        public static ByteBrewAdDataCommon Map(AndroidJavaObject jo)
        {
            if (jo == null) {
                return new ByteBrewAdDataCommon();
            }
            var d = new ByteBrewAdDataCommon();
            d.adUnitID     = jo.Call<string>("getAdUnitID");
            d.isCTRLOnly   = jo.Call<bool>  ("getIsCTRLOnly");
            d.infoString   = jo.Call<string>("getInfoString");
            return d;
        }
    }

    // ---- Proxies -> raise ad events ----
    public class BBAndroidAdInitProxy : AndroidJavaProxy
    {
        public BBAndroidAdInitProxy() : base("com.bytebrew.bytebrewlibrary.bytebrewads.AdInitEventListener") {}
        public void onAdsInitSuccess() => ByteBrewAds.RaiseInitSuccess();
        public void onAdsInitFailure() => ByteBrewAds.RaiseInitFailure();
    }

    public class BBAndroidInterstitialAdLoadProxy : AndroidJavaProxy
    {
        public BBAndroidInterstitialAdLoadProxy() : base("com.bytebrew.bytebrewlibrary.bytebrewads.AdLoadEventListener") {}
        public void onAdLoaded(AndroidJavaObject ad)                => ByteBrewAds.RaiseInterstitialAdLoaded(BBAndroidAdMap.Map(ad));
        public void onAdLoadError(string err, AndroidJavaObject ad) => ByteBrewAds.RaiseInterstitialAdLoadError(err, BBAndroidAdMap.Map(ad));
    }

    public class BBAndroidRewardedAdLoadProxy : AndroidJavaProxy
    {
        public BBAndroidRewardedAdLoadProxy() : base("com.bytebrew.bytebrewlibrary.bytebrewads.AdLoadEventListener") {}
        public void onAdLoaded(AndroidJavaObject ad)                => ByteBrewAds.RaiseRewardedAdLoaded(BBAndroidAdMap.Map(ad));
        public void onAdLoadError(string err, AndroidJavaObject ad) => ByteBrewAds.RaiseRewardedAdLoadError(err, BBAndroidAdMap.Map(ad));
    }

    public class BBAndroidInterstitialAdEventProxy : AndroidJavaProxy
    {
        public BBAndroidInterstitialAdEventProxy() : base("com.bytebrew.bytebrewlibrary.bytebrewads.InterstitialAdEventListener") {}
        public void onAdStarted(AndroidJavaObject ad)               => ByteBrewAds.RaiseInterstitialAdStarted(BBAndroidAdMap.Map(ad));
        public void onAdClicked(AndroidJavaObject ad)               => ByteBrewAds.RaiseInterstitialAdClicked(BBAndroidAdMap.Map(ad));
        public void onAdCompleted(AndroidJavaObject ad)             => ByteBrewAds.RaiseInterstitialAdCompleted(BBAndroidAdMap.Map(ad));
        public void onAdDismissed(AndroidJavaObject ad)             => ByteBrewAds.RaiseInterstitialAdDismissed(BBAndroidAdMap.Map(ad));
        public void onAdError(string err, AndroidJavaObject ad)     => ByteBrewAds.RaiseInterstitialAdError(err, BBAndroidAdMap.Map(ad));
    }

    public class BBAndroidRewardedAdEventProxy : AndroidJavaProxy
    {
        public BBAndroidRewardedAdEventProxy() : base("com.bytebrew.bytebrewlibrary.bytebrewads.RewardedAdEventListener") {}
        public void onAdStarted(AndroidJavaObject ad)               => ByteBrewAds.RaiseRewardedAdStarted(BBAndroidAdMap.Map(ad));
        public void onAdClicked(AndroidJavaObject ad)               => ByteBrewAds.RaiseRewardedAdClicked(BBAndroidAdMap.Map(ad));
        public void onAdCompleted(AndroidJavaObject ad)             => ByteBrewAds.RaiseRewardedAdCompleted(BBAndroidAdMap.Map(ad));
        public void onAdRewarded(AndroidJavaObject ad)              => ByteBrewAds.RaiseRewardedAdRewarded(BBAndroidAdMap.Map(ad));
        public void onAdDismissed(AndroidJavaObject ad)             => ByteBrewAds.RaiseRewardedAdDismissed(BBAndroidAdMap.Map(ad));
        public void onAdError(string err, AndroidJavaObject ad)     => ByteBrewAds.RaiseRewardedAdError(err, BBAndroidAdMap.Map(ad));
    }
}
#endif
