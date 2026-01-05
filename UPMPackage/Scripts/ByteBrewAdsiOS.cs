// Assets/ByteBrew/Runtime/Ads/ByteBrewAds.iOS.cs
#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
using UnityEngine;

namespace ByteBrewSDK
{
    internal static class ByteBrewAdsIOS
    {
        [DllImport("__Internal")] static extern void ByteBrewAds_SetUnityReceiver(string go);
        [DllImport("__Internal")] static extern void ByteBrewAds_InitAds();
        [DllImport("__Internal")] static extern void ByteBrewAds_LoadInterstitialCrossPromoAd(string adUnitID);
        [DllImport("__Internal")] static extern void ByteBrewAds_LoadInterstitialCrossPromoAdWithCtrlOnly(string adUnitID, bool ctrlOnly);
        [DllImport("__Internal")] static extern void ByteBrewAds_LoadRewardedCrossPromoAd(string adUnitID);
        [DllImport("__Internal")] static extern void ByteBrewAds_LoadRewardedCrossPromoAdWithCtrlOnly(string adUnitID, bool ctrlOnly);
        [DllImport("__Internal")] static extern void ByteBrewAds_ShowInterstitialCrossPromoAd(string adUnitID);
        [DllImport("__Internal")] static extern void ByteBrewAds_ShowInterstitialCrossPromoAdWithCtrlOnly(string adUnitID, bool ctrlOnly);
        [DllImport("__Internal")] static extern void ByteBrewAds_ShowRewardedCrossPromoAd(string adUnitID);
        [DllImport("__Internal")] static extern void ByteBrewAds_ShowRewardedCrossPromoAdWithCtrlOnly(string adUnitID, bool ctrlOnly);
        [DllImport("__Internal")] static extern void ByteBrewAds_FlushAllAds();
        [DllImport("__Internal")] static extern bool ByteBrewAds_IsInitialized();
        [DllImport("__Internal")] static extern bool ByteBrewAds_IsCrossPromoAdLoaded(string adUnitID);
        [DllImport("__Internal")] static extern bool ByteBrewAds_IsCrossPromoAdLoadedWithCtrlOnly(string adUnitID, bool ctrlOnly);
        [DllImport("__Internal")] static extern bool ByteBrewAds_IsAdShowing();

        static GameObject _receiverGO;
        static BB_AdsiOSReceiver _receiverComp;
        static bool _registeredWithBridge;

        public static void EnsureReceiver()
        {
            if (_receiverComp != null) {
                return;
            }

            // Try to find an existing one (e.g., after domain reload)
            var existing = GameObject.Find("ByteBrewAds_iOSReceiver");
            if (existing != null) {
                _receiverGO = existing;
                _receiverComp = existing.GetComponent<BB_AdsiOSReceiver>();
            }

            if (_receiverComp == null)
            {
                _receiverGO = new GameObject("ByteBrewAds_iOSReceiver");
                Object.DontDestroyOnLoad(_receiverGO);
                _receiverGO.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
                _receiverComp = _receiverGO.AddComponent<BB_AdsiOSReceiver>();
            }

            if (!_registeredWithBridge)
            {
                ByteBrewAds_SetUnityReceiver(_receiverGO.name);
                _registeredWithBridge = true;
                Debug.Log("[ByteBrewAds][iOS] Receiver registered: " + _receiverGO.name);
            }
        }

        public static void InitAds()                                                    => ByteBrewAds_InitAds();
        public static void LoadInterstitialCrossPromoAd(string adUnitID)                => ByteBrewAds_LoadInterstitialCrossPromoAd(adUnitID);
        public static void LoadInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) => ByteBrewAds_LoadInterstitialCrossPromoAdWithCtrlOnly(adUnitID, ctrlOnly);
        public static void LoadRewardedCrossPromoAd(string adUnitID)                    => ByteBrewAds_LoadRewardedCrossPromoAd(adUnitID);
        public static void LoadRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)     => ByteBrewAds_LoadRewardedCrossPromoAdWithCtrlOnly(adUnitID, ctrlOnly);
        public static void ShowInterstitialCrossPromoAd(string adUnitID)                => ByteBrewAds_ShowInterstitialCrossPromoAd(adUnitID);
        public static void ShowInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) => ByteBrewAds_ShowInterstitialCrossPromoAdWithCtrlOnly(adUnitID, ctrlOnly);
        public static void ShowRewardedCrossPromoAd(string adUnitID)                    => ByteBrewAds_ShowRewardedCrossPromoAd(adUnitID);
        public static void ShowRewardedCrossPromoAd(string adUnitID, bool ctrlOnly)     => ByteBrewAds_ShowRewardedCrossPromoAdWithCtrlOnly(adUnitID, ctrlOnly);
        public static void FlushAllAds()                                                => ByteBrewAds_FlushAllAds();
        public static bool IsInitialized()                                              => ByteBrewAds_IsInitialized();
        public static bool IsCrossPromoAdLoaded(string adUnitID)                        => ByteBrewAds_IsCrossPromoAdLoaded(adUnitID);
        public static bool IsCrossPromoAdLoaded(string adUnitID, bool ctrlOnly)         => ByteBrewAds_IsCrossPromoAdLoadedWithCtrlOnly(adUnitID, ctrlOnly);
        public static bool IsAdShowing()                                                => ByteBrewAds_IsAdShowing();
    }

    // Receives UnitySendMessage(JSON) from Obj-C bridge
    internal class BB_AdsiOSReceiver : MonoBehaviour
    {
        [System.Serializable] class Payload
        {
            public string type, error, feedback;
            public int index;
            public ByteBrewAdDataCommon ad;
        }

        static Payload P(string json)
        {
            if (string.IsNullOrEmpty(json)) {
                return new Payload();
            }
            try { 
                return JsonUtility.FromJson<Payload>(json); 
            } catch { 
                return new Payload(); 
            }
        }

        // ---- INIT ----
        public void OnAdsInitSuccess(string _)                  { ByteBrewAds.RaiseInitSuccess(); }
        public void OnAdsInitFailure(string _)                  { ByteBrewAds.RaiseInitFailure(); }

        // ---- LOAD ----
        // Interstitial
        public void OnInterstitialAdLoaded(string json)         { var p = P(json); ByteBrewAds.RaiseInterstitialAdLoaded(p.ad); }
        public void OnInterstitialAdLoadError(string json)      { var p = P(json); ByteBrewAds.RaiseInterstitialAdLoadError(p.error, p.ad); }
        // Rewarded
        public void OnRewardedAdLoaded(string json)             { var p = P(json); ByteBrewAds.RaiseRewardedAdLoaded(p.ad); }
        public void OnRewardedAdLoadError(string json)          { var p = P(json); ByteBrewAds.RaiseRewardedAdLoadError(p.error, p.ad); }

        // ---- EVENTS ----
        // Interstitial
        public void OnInterstitialAdStarted(string json)        { var p = P(json); ByteBrewAds.RaiseInterstitialAdStarted(p.ad); }
        public void OnInterstitialAdClicked(string json)        { var p = P(json); ByteBrewAds.RaiseInterstitialAdClicked(p.ad); }
        public void OnInterstitialAdCompleted(string json)      { var p = P(json); ByteBrewAds.RaiseInterstitialAdCompleted(p.ad); }
        public void OnInterstitialAdDismissed(string json)      { var p = P(json); ByteBrewAds.RaiseInterstitialAdDismissed(p.ad); }
        public void OnInterstitialAdError(string json)          { var p = P(json); ByteBrewAds.RaiseInterstitialAdError(p.error, p.ad); }
        // Rewarded
        public void OnRewardedAdStarted(string json)            { var p = P(json); ByteBrewAds.RaiseRewardedAdStarted(p.ad); }
        public void OnRewardedAdClicked(string json)            { var p = P(json); ByteBrewAds.RaiseRewardedAdClicked(p.ad); }
        public void OnRewardedAdCompleted(string json)          { var p = P(json); ByteBrewAds.RaiseRewardedAdCompleted(p.ad); }
        public void OnRewardedAdRewarded(string json)           { var p = P(json); ByteBrewAds.RaiseRewardedAdRewarded(p.ad); }
        public void OnRewardedAdDismissed(string json)          { var p = P(json); ByteBrewAds.RaiseRewardedAdDismissed(p.ad); }
        public void OnRewardedAdError(string json)              { var p = P(json); ByteBrewAds.RaiseRewardedAdError(p.error, p.ad); }
    }
}
#endif
