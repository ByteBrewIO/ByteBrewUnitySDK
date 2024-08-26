using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ByteBrewSDK
{
    public class ByteBrewiOSHandler : MonoBehaviour
    {
#if UNITY_IPHONE && !(UNITY_EDITOR)
        [DllImport("__Internal")]
        private static extern void InitializeByteBrew(string gameID, string gameKey, string engineVersion, string buildVersion, string bundleID);

        [DllImport("__Internal")]
        private static extern void StartPushNotifications();

        [DllImport("__Internal")]
        private static extern void CreateCustomEvent(string logEvent);

        [DllImport("__Internal")]
        private static extern void ValidateiOSIAPEvent(string logEvent);

        [DllImport("__Internal")]
        private static extern void AddCustomData(string data);

        [DllImport("__Internal")]
        private static extern void requestForAppTrackingTransparency();

        [DllImport("__Internal")]
        private static extern bool HasRemoteConfigs();

        [DllImport("__Internal")]
        private static extern void LoadRemoteConfigs();

        [DllImport("__Internal")]
        private static extern string getRemoteConfigForKey(string key, string defaultValue);

        [DllImport("__Internal")]
        private static extern string GetCurrentUserID();

        [DllImport("__Internal")]
        private static extern void StopTracking();

        [DllImport("__Internal")]
        private static extern void StartTracking();

        public static bool InitializePlugin(string gameID, string gameKey, string unityVersion, string buildVerison, string bundleID)
        {
            InitializeByteBrew(gameID, gameKey, unityVersion, buildVerison, bundleID);
            return true;
        }

        public static void SetPushNotifications()
        {
            StartPushNotifications();
        }

        public static void SendCustomEvent(ByteLog log)
        {
            string logEvent = JsonUtility.ToJson(log);
            logEvent = SerializeByteLog(logEvent, log.externalData);
            CreateCustomEvent(logEvent);
        }

        public static void ValidateIAPEvent(ByteLog log)
        {
            string logEvent = JsonUtility.ToJson(log);
            logEvent = SerializeByteLog(logEvent, log.externalData);
            ValidateiOSIAPEvent(logEvent);
        }

        public static void SendCustomUserData(Dictionary<string, string> log)
        {
            string data = SerlizeDictionaryItems(log);
            AddCustomData(data);
        }

        public static void PromptForAppTrackingTransparency()
        {
            requestForAppTrackingTransparency();
        }

        public static bool CheckIfRemoteConfigsAreSet()
        {
            return HasRemoteConfigs();
        }

        public static void RetrieveRemoteConfigs()
        {
            LoadRemoteConfigs();
        }

        public static string GetRemoteConfigValue(string key, string defaultValue)
        {
            return getRemoteConfigForKey(key, defaultValue);
        }

        public static void EnableTracking()
        {
            StartTracking();
        }

        public static void DisableTracking()
        {
            StopTracking();
        }

        public static string GetUserID()
        {
            return GetCurrentUserID();
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
