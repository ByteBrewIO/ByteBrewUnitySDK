using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ByteBrewSDK
{
    public class ByteBrewAndroidHandler : MonoBehaviour
    {
#if UNITY_ANDROID && !(UNITY_EDITOR)
        private static AndroidJavaObject byteBrewHandler;
        private static AndroidJavaObject byteBrewListener;
        private static AndroidJavaObject byteBrewAds;
        private static AndroidJavaObject byteBrewPushNotifications;
        private static AndroidJavaObject playerActivity;

        private static bool initializationCalled = false;

        public static bool InitializePlugin(string unityVersion, string buildVerison, string bundleID, string gameID, string gameKey)
        {
            byteBrewHandler = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.ByteBrewHandler");
            byteBrewListener = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.ByteBrewListener");
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            playerActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var application = playerActivity.Call<AndroidJavaObject>("getApplication");
            byteBrewListener.CallStatic("CreateListeners", application);
            AndroidJavaObject context = playerActivity.Call<AndroidJavaObject>("getApplicationContext");
            byteBrewHandler.Call("InitializeByteBrew", gameID, gameKey, unityVersion, buildVerison, bundleID, context);

            initializationCalled = true;
            return true;
        }

        public static void StartPushNotifications()
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            byteBrewPushNotifications = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.ByteBrewPushNotifications");
            byteBrewPushNotifications.CallStatic("StartByteBrewPushNotifications", currentActivity);
        }

        public static void SendCustomEvent(ByteLog log)
        {
            string logEvent = JsonUtility.ToJson(log);
            logEvent = SerializeByteLog(logEvent, log.externalData);

            string[] items = new string[] { logEvent };

            byteBrewHandler.Call("CreateCustomEvent", items);
        }

        public static void ValidateIAPEvent(ByteLog log)
        {
            string logEvent = JsonUtility.ToJson(log);
            logEvent = SerializeByteLog(logEvent, log.externalData);

            string[] items = new string[] { logEvent };

            byteBrewHandler.Call("ValidateIAPEvent", items);
        }

        public static void SendCustomUserData(Dictionary<string, string> log)
        {
            string data = SerlizeDictionaryItems(log);

            string[] items = new string[] { data };

            byteBrewHandler.Call("SendCustomDataAttributes", items);
        }

        public static void RetrieveRemoteConfigs()
        {
            byteBrewHandler.Call("LoadRemoteConfigurations");
        }

        public static bool CheckIfRemoteConfigsAreSet()
        {
            return bool.Parse(byteBrewHandler.CallStatic<string>("IsRemoteConfigsSet"));
        }

        public static string GetRemoteConfigValue(string key, string defaultValue)
        {
            return byteBrewHandler.CallStatic<string>("GetRemoteConfigForKey", key, defaultValue);
        }

        public static AndroidJavaObject GetContext() 
        {
            try {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
                return context;
            } catch {
                return null;
            }
        }

        public static void InitializeAds() {
            AndroidJavaObject context = GetContext();
            if (context == null) {
                return;
            }
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }

            byteBrewAds.CallStatic("initAds", context);
        }

        public static bool IsAdsInitialized() {
            if (byteBrewAds == null) {
                return false;
            }

            return byteBrewAds.CallStatic<bool>("isInitialized");
        }

        public static void SetInterstitialAdEventListener(BBAndroidInterstitialAdEventProxy adEventListener) {
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }
            if (adEventListener == null) {
                return;
            }

            byteBrewAds.CallStatic("setInterstitialAdEventListener", adEventListener);
        }

        public static void SetInterstitialAdLoadEventListener(BBAndroidInterstitialAdLoadProxy adLoadEventListener) {
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }
            if (adLoadEventListener == null) {
                return;
            }

            byteBrewAds.CallStatic("setInterstitialAdLoadEventListener", adLoadEventListener);
        }

        public static void SetRewardedAdEventListener(BBAndroidRewardedAdEventProxy adEventListener) {
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }
            if (adEventListener == null) {
                return;
            }

            byteBrewAds.CallStatic("setRewardedAdEventListener", adEventListener);
        }

        public static void SetRewardedAdLoadEventListener(BBAndroidRewardedAdLoadProxy adLoadEventListener) {
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }
            if (adLoadEventListener == null) {
                return;
            }

            byteBrewAds.CallStatic("setRewardedAdLoadEventListener", adLoadEventListener);
        }

        public static void SetAdInitEventListener(BBAndroidAdInitProxy adInitEventListener) {
            if (byteBrewAds == null) {
                byteBrewAds = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.bytebrewads.ByteBrewAds");
            }
            if (adInitEventListener == null) {
                return;
            }

            byteBrewAds.CallStatic("setAdInitEventListener", adInitEventListener);
        }

        public static void FlushAllAds() {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("flushAllAds");
        }

        public static bool IsCrossPromoAdLoaded(string adUnitID) {
            if (byteBrewAds == null) {
                return false;
            }

            return byteBrewAds.CallStatic<bool>("isCrossPromoAdLoaded", adUnitID);
        }

        public static bool IsCrossPromoAdLoaded(string adUnitID, bool ctrlOnly) {
            if (byteBrewAds == null) {
                return false;
            }

            return byteBrewAds.CallStatic<bool>("isCrossPromoAdLoaded", adUnitID, ctrlOnly);
        }

        public static void LoadInterstitialCrossPromoAd(string adUnitID) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("loadInterstitialCrossPromoAd", adUnitID);
        }

        public static void LoadInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("loadInterstitialCrossPromoAd", adUnitID, ctrlOnly);
        }

        public static void LoadRewardedCrossPromoAd(string adUnitID) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("loadRewardedCrossPromoAd", adUnitID);
        }

        public static void LoadRewardedCrossPromoAd(string adUnitID, bool ctrlOnly) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("loadRewardedCrossPromoAd", adUnitID, ctrlOnly);
        }

        public static bool IsAdShowing() {
            if (byteBrewAds == null) {
                return false;
            }

            return byteBrewAds.CallStatic<bool>("isAdShowing");
        }

        public static void ShowInterstitialCrossPromoAd(string adUnitID) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("showInterstitialCrossPromoAd", adUnitID);
        }

        public static void ShowInterstitialCrossPromoAd(string adUnitID, bool ctrlOnly) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("showInterstitialCrossPromoAd", adUnitID, ctrlOnly);
        }

        public static void ShowRewardedCrossPromoAd(string adUnitID) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("showRewardedCrossPromoAd", adUnitID);
        }

        public static void ShowRewardedCrossPromoAd(string adUnitID, bool ctrlOnly) {
            if (byteBrewAds == null) {
                return;
            }

            byteBrewAds.CallStatic("showRewardedCrossPromoAd", adUnitID, ctrlOnly);
        }


        public static void DisableTracking()
        {
            if(byteBrewHandler == null)
            {
                byteBrewHandler = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.ByteBrewHandler");
                byteBrewHandler.Call("StopTracking", GetContext());
            }
            else
            {
                byteBrewHandler.Call("StopTracking", GetContext());
            }

        }

        public static void EnableTracking()
        {
            if (byteBrewHandler == null)
            {
                byteBrewHandler = new AndroidJavaObject("com.bytebrew.bytebrewlibrary.ByteBrewHandler");
                byteBrewHandler.Call("StartTracking", GetContext());
            }
            else
            {
                byteBrewHandler.Call("StartTracking", GetContext());
            }

        }

        public static string GetCurrentUserID()
        {
            if(!initializationCalled) 
                return "";
            
            return byteBrewHandler.CallStatic<string>("GetUserID");
        }

        private static string SerializeByteLog(string jsonItem, Dictionary<string, string> neededToSerialize)
        {
            string endJson = "";

            string indexed = jsonItem.Substring(1);

            string dictionaryStr = "\"externalData\":{";

            int countData = neededToSerialize.Count;

            int currentCount = 0;

            if(countData == 1)
            {
                foreach (var item in neededToSerialize)
                {
                    string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                    dictionaryStr += (temp + "},");
                }
            }
            else
            {
                foreach (var item in neededToSerialize)
                {
                    if (currentCount < (countData - 1))
                    {
                        string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                        dictionaryStr += (temp + ",");
                    }
                    else if (currentCount == (countData-1))
                    {
                        string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                        dictionaryStr += (temp + "},");
                    }
                    currentCount++;

                }
            }

            endJson = "{" + dictionaryStr + indexed;

            return endJson;
        }

        public static string SerlizeDictionaryItems(Dictionary<string, string> neededToSerialize)
        {
            string dictionaryStr = "{";

            int countData = neededToSerialize.Count;

            int currentCount = 0;

            if(countData == 1)
            {
                foreach (var item in neededToSerialize)
                {
                    string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                    dictionaryStr += (temp + "}");
                }
            }
            else
            {
                foreach (var item in neededToSerialize)
                {
                    if (currentCount < (countData - 1))
                    {
                        string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                        dictionaryStr += (temp + ",");
                    }
                    else if (currentCount == (countData-1))
                    {
                        string temp = "\"" + item.Key + "\":" + "\"" + item.Value + "\"";
                        dictionaryStr += (temp + "}");
                    }
                    currentCount++;

                }
            }

            return dictionaryStr;
        }
#endif
    }

}
