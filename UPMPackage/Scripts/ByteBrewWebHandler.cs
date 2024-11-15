using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace ByteBrewSDK {
    public class ByteBrewWebHandler : MonoBehaviour {
#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void InitializeByteBrew(string appID, string appKey, string appVersion);
        public static bool InitializeByteBrewWeb(string appID, string appKey, string appVersion) {
            try {
                InitializeByteBrew(appID, appKey, appVersion);
                return true;
            } catch (Exception e) {
                Debug.LogError("Error initializing ByteBrew: " + e.Message);
                return false;
            }
        }

        [DllImport("__Internal")]
        public static extern bool IsByteBrewInitialized();

        [DllImport("__Internal")]
        public static extern void NewCustomEvent(string eventName);

        [DllImport("__Internal")]
        private static extern void NewCustomEventWithString(string eventName, string value);

        [DllImport("__Internal")]
        private static extern void NewCustomEventWithNumber(string eventName, float value);

        public static void NewCustomEvent(string eventName, string value) {
            NewCustomEventWithString(eventName, value);
        }

        public static void NewCustomEvent(string eventName, float value) {
            NewCustomEventWithNumber(eventName, value);
        }

        private static string DictionaryToString(Dictionary<string, string> dictionary) {
            //format is key1=value1;key2=value2;key3=value3;
            string result = "";
            if (dictionary == null) {
                return result;
            }
            foreach (KeyValuePair<string, string> entry in dictionary) {
                result += entry.Key + "=" + entry.Value + ";";
            }
            //keep the last semicolon, it's required
            return result;
        }

        public static void NewCustomEvent(string eventName, Dictionary<string, string> value) {
            NewCustomEventWithString(eventName, DictionaryToString(value));
        }

        [DllImport("__Internal")]
        public static extern void LoadRemoteConfigs();

        [DllImport("__Internal")]
        public static extern string RetreiveRemoteConfigValue(string key, string defaultValue);

        [DllImport("__Internal")]
        public static extern bool HasRemoteConfigsBeenSet();

        [DllImport("__Internal")]
        public static extern void RestartTracking();

        [DllImport("__Internal")]
        public static extern void StopTracking();

        [DllImport("__Internal")]
        public static extern string GetUserID();
#endif
    }
}
