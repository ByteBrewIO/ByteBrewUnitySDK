using System;
using UnityEngine;

namespace ByteBrewSDK
{
    [Serializable]
    public class ByteBrewAdDataCommon
    {
        public string adUnitID, infoString;
        public bool isCTRLOnly;
        public override string ToString() => infoString ?? adUnitID ?? base.ToString();
    }

    public static class ByteBrewAds
    {
        // ===== INIT =====
        public static event Action OnAdsInitSuccess;
        public static event Action OnAdsInitFailure;

        // ===== LOAD =====
        // Interstitial
        public static event Action<ByteBrewAdDataCommon> OnInterstitialAdLoaded;
        public static event Action<string, ByteBrewAdDataCommon> OnInterstitialAdLoadError;
        // Rewarded
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdLoaded;
        public static event Action<string, ByteBrewAdDataCommon> OnRewardedAdLoadError;

        // ===== EVENTS =====
        // Interstitial
        public static event Action<ByteBrewAdDataCommon> OnInterstitialAdStarted;
        public static event Action<ByteBrewAdDataCommon> OnInterstitialAdClicked;
        public static event Action<ByteBrewAdDataCommon> OnInterstitialAdCompleted;
        public static event Action<ByteBrewAdDataCommon> OnInterstitialAdDismissed;
        public static event Action<string, ByteBrewAdDataCommon> OnInterstitialAdError;
        // Rewarded
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdStarted;
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdClicked;
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdCompleted;
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdRewarded;
        public static event Action<ByteBrewAdDataCommon> OnRewardedAdDismissed;
        public static event Action<string, ByteBrewAdDataCommon> OnRewardedAdError;

        // ========= Public API =========

        /// <summary>
        /// Initialize the ads SDK subsystem for ByteBrew
        /// </summary>
        public static void InitializeAds()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.InitializeAds();
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.InitAds();
#else
            Debug.Log("[ByteBrewAds] InitializeAds: Not a mobile runtime.");
#endif
        }

        /// <summary>
        /// Check if the ads SDK has been initialized
        /// </summary>
        public static bool IsAdsInitialized()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ByteBrewAdsAndroid.IsInitialized();
#elif UNITY_IOS && !UNITY_EDITOR
            return ByteBrewAdsIOS.IsInitialized();
#else
            return false;
#endif
        }
        
        /// <summary>
        /// Flushes all loaded ads from ByteBrew
        /// </summary>
        public static void FlushAllAds()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.FlushAllAds();
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.FlushAllAds();
#else
            Debug.Log("[ByteBrewAds] ShowVideoAd: Not a mobile runtime.");
#endif
        }

        /// <summary>
        /// Check if a cross promo ad is loaded and ready to be shown
        /// </summary>
        /// <returns>Boolean, true if an ad is loaded and ready to be shown, false otherwise.</returns>
        public static bool IsCrossPromoAdLoaded(string adUnitID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ByteBrewAdsAndroid.IsCrossPromoAdLoaded(adUnitID);
#elif UNITY_IOS && !UNITY_EDITOR
            return ByteBrewAdsIOS.IsCrossPromoAdLoaded(adUnitID);
#else
            return false;
#endif
        }
        
        /// <summary>
        /// Check if a cross promo ad is loaded and ready to be shown
        /// </summary>
        /// <returns>Boolean, true if an ad is loaded and ready to be shown, false otherwise.</returns>
        public static bool IsCrossPromoAdLoaded(string adUnitID, bool ctrlOnly)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ByteBrewAdsAndroid.IsCrossPromoAdLoaded(adUnitID, ctrlOnly);
#elif UNITY_IOS && !UNITY_EDITOR
            return ByteBrewAdsIOS.IsCrossPromoAdLoaded(adUnitID, ctrlOnly);
#else
            return false;
#endif
        }

        /// <summary>
        /// Load an interstitial cross promo ad from ByteBrew
        /// </summary>
        public static void LoadInterstitialCrossPromoAd(string adUnitID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.LoadInterstitialCrossPromoAd(adUnitID);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.LoadInterstitialCrossPromoAd(adUnitID);
#else
            Debug.Log("[ByteBrewAds] LoadInterstitialCrossPromoAd: Not a mobile runtime.");
#endif
        }

        /// <summary>
        /// Load an interstitial cross promo ad from ByteBrew
        /// </summary>
        public static void LoadInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.LoadInterstitialCrossPromoAd(adUnitID, ctrlOnly);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.LoadInterstitialCrossPromoAd(adUnitID, ctrlOnly);
#else
            Debug.Log("[ByteBrewAds] LoadInterstitialCrossPromoAd: Not a mobile runtime.");
#endif
        }
        
        /// <summary>
        /// Load a rewarded cross promo ad from ByteBrew
        /// </summary>
        public static void LoadRewardedCrossPromoAd(string adUnitID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.LoadRewardedCrossPromoAd(adUnitID);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.LoadRewardedCrossPromoAd(adUnitID);
#else
            Debug.Log("[ByteBrewAds] LoadRewardedCrossPromoAd: Not a mobile runtime.");
#endif
        }

        /// <summary>
        /// Load a rewarded cross promo ad from ByteBrew
        /// </summary>
        public static void LoadRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.LoadRewardedCrossPromoAd(adUnitID, ctrlOnly);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.LoadRewardedCrossPromoAd(adUnitID, ctrlOnly);
#else
            Debug.Log("[ByteBrewAds] LoadRewardedCrossPromoAd: Not a mobile runtime.");
#endif
        }

        /// <summary>
        /// Check if an ad is already showing
        /// </summary>
        /// <returns>Boolean, true if an ad is showing, false otherwise.</returns>
        public static bool IsAdShowing()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return ByteBrewAdsAndroid.IsAdShowing();
#elif UNITY_IOS && !UNITY_EDITOR
            return ByteBrewAdsIOS.IsAdShowing();
#else
            return false;
#endif
        }

        /// <summary>
        /// Show a loaded interstitial cross promo ad from ByteBrew
        /// </summary>
        public static void ShowInterstitialCrossPromoAd(string adUnitID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.ShowInterstitialCrossPromoAd(adUnitID);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.ShowInterstitialCrossPromoAd(adUnitID);
#else
            Debug.Log("[ByteBrewAds] ShowInterstitialCrossPromoAd: Not a mobile runtime.");
#endif
        }
        
        /// <summary>
        /// Show a loaded interstitial cross promo ad from ByteBrew
        /// </summary>
        public static void ShowInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.ShowInterstitialCrossPromoAd(adUnitID, ctrlOnly);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.ShowInterstitialCrossPromoAd(adUnitID, ctrlOnly);
#else
            Debug.Log("[ByteBrewAds] ShowInterstitialCrossPromoAd: Not a mobile runtime.");
#endif
        }
        
        /// <summary>
        /// Show a loaded rewarded cross promo ad from ByteBrew
        /// </summary>
        public static void ShowRewardedCrossPromoAd(string adUnitID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.ShowRewardedCrossPromoAd(adUnitID);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.ShowRewardedCrossPromoAd(adUnitID);
#else
            Debug.Log("[ByteBrewAds] ShowRewardedCrossPromoAd: Not a mobile runtime.");
#endif
        }
        
        /// <summary>
        /// Show a loaded rewarded cross promo ad from ByteBrew
        /// </summary>
        public static void ShowRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ByteBrewAdsAndroid.WireListenersIfNeeded();
            ByteBrewAdsAndroid.ShowRewardedCrossPromoAd(adUnitID, ctrlOnly);
#elif UNITY_IOS && !UNITY_EDITOR
            ByteBrewAdsIOS.EnsureReceiver();
            ByteBrewAdsIOS.ShowRewardedCrossPromoAd(adUnitID, ctrlOnly);
#else
            Debug.Log("[ByteBrewAds] ShowRewardedCrossPromoAd: Not a mobile runtime.");
#endif
        }

        // ========= Internal raise helpers (called by platform glue) =========
        internal static void RaiseInitSuccess()                                             => OnAdsInitSuccess?.Invoke();
        internal static void RaiseInitFailure()                                             => OnAdsInitFailure?.Invoke();

        // Interstitial Load Events
        internal static void RaiseInterstitialAdLoaded(ByteBrewAdDataCommon d)              => OnInterstitialAdLoaded?.Invoke(d);
        internal static void RaiseInterstitialAdLoadError(string e, ByteBrewAdDataCommon d) => OnInterstitialAdLoadError?.Invoke(e, d);
        // Rewarded Load Events
        internal static void RaiseRewardedAdLoaded(ByteBrewAdDataCommon d)                  => OnRewardedAdLoaded?.Invoke(d);
        internal static void RaiseRewardedAdLoadError(string e, ByteBrewAdDataCommon d)     => OnRewardedAdLoadError?.Invoke(e, d);

        // Interstitial Ad Events
        internal static void RaiseInterstitialAdStarted(ByteBrewAdDataCommon d)             => OnInterstitialAdStarted?.Invoke(d);
        internal static void RaiseInterstitialAdClicked(ByteBrewAdDataCommon d)             => OnInterstitialAdClicked?.Invoke(d);
        internal static void RaiseInterstitialAdCompleted(ByteBrewAdDataCommon d)           => OnInterstitialAdCompleted?.Invoke(d);
        internal static void RaiseInterstitialAdDismissed(ByteBrewAdDataCommon d)           => OnInterstitialAdDismissed?.Invoke(d);
        internal static void RaiseInterstitialAdError(string e, ByteBrewAdDataCommon d)     => OnInterstitialAdError?.Invoke(e, d);
        // Rewarded Ad Events
        internal static void RaiseRewardedAdStarted(ByteBrewAdDataCommon d)                 => OnRewardedAdStarted?.Invoke(d);
        internal static void RaiseRewardedAdClicked(ByteBrewAdDataCommon d)                 => OnRewardedAdClicked?.Invoke(d);
        internal static void RaiseRewardedAdCompleted(ByteBrewAdDataCommon d)               => OnRewardedAdCompleted?.Invoke(d);
        internal static void RaiseRewardedAdRewarded(ByteBrewAdDataCommon d)                => OnRewardedAdRewarded?.Invoke(d);
        internal static void RaiseRewardedAdDismissed(ByteBrewAdDataCommon d)               => OnRewardedAdDismissed?.Invoke(d);
        internal static void RaiseRewardedAdError(string e, ByteBrewAdDataCommon d)         => OnRewardedAdError?.Invoke(e, d);
    }
}
