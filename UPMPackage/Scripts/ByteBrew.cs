using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ByteBrewSDK
{
    public class ByteBrew : MonoBehaviour
    {
        public static readonly string SDK_VERSION = "0.1.9";

        private static ByteBrew _instance;

        public static ByteBrew Instance { get { return _instance; } }

        ///<summary> Do not use this variable to tell if the SDK is initialized, use IsByteBrewInitialized()</summary>
        public static bool IsInitilized = false;

        private static bool libraryFinishedInitialization = false;

        public static bool FirstTimeOpening = false;

        public static event Action<int> OnRequestTrackTransparencyFinished;

        public static event Action remoteConfigUpdated;

        public static event Action<ByteBrewPurchaseData> purchaseResult;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.Log("ByteBrew can only have one running instance");
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public static void InitializeByteBrew()
        {
#if UNITY_EDITOR

            Debug.Log("ByteBrew is not going to initialize in a non mobile environment. SDK events will not be sent.");

#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                return;
            ByteBrewSettings settings = Resources.Load<ByteBrewSettings>("ByteBrewSettings");

            if (settings == null)
            {
                Debug.Log("ByteBrew Settings have not been created");
                return;
            }

            IsInitilized = ByteBrew_Helper.SetGameSettings(settings);

#endif

        }

        /// <summary>
        /// Returns whether the ByteBrew Library is fully initialized
        /// </summary>
        /// <returns>Boolean, true if the library for ByteBrew is initialized.</returns>
        public static bool IsByteBrewInitialized()
        {
#if UNITY_WEBGL
            return IsInitilized && ByteBrewWebHandler.IsByteBrewInitialized();
#else
            return libraryFinishedInitialization;
#endif
        }

        public void ByteBrewSDKInitialized()
        {
            libraryFinishedInitialization = true;
        }

#if !UNITY_WEBGL
        public static void StartPushNotifications()
        {
#if UNITY_EDITOR

            Debug.Log("ByteBrew is not going to start push notificaiton in a non mobile environment.");

#elif (UNITY_ANDROID) || (UNITY_IOS)

			ByteBrew_Helper.SetupPushNotifications();

#endif

        }
#endif

		/// <summary>
        /// Add a custom Data Attribute to user
        /// </summary>
        /// <param name="key">Key name of the custom data</param>
		/// <param name="value">String value of the custom data</param>
		public static void SetCustomUserDataAttribute(string key, string value) 
		{
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not setting custom data... But you are calling it so it will work on a mobile environment.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddCustomDataPair(key, value);

#endif
		}

		/// <summary>
        /// Add a custom Data Attribute to user
        /// </summary>
        /// <param name="key">Key name of the custom data</param>
		/// <param name="value">Double value of the custom data</param>
		public static void SetCustomUserDataAttribute(string key, double value) 
		{
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not setting custom data... But you are calling it so it will work on a mobile environment.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddCustomDataPair(key, value);

#endif
		}

		/// <summary>
        /// Add a custom Data Attribute to user
        /// </summary>
        /// <param name="key">Key name of the custom data</param>
		/// <param name="value">Int value of the custom data</param>
		public static void SetCustomUserDataAttribute(string key, int value) 
		{
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not setting custom data... But you are calling it so it will work on a mobile environment.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddCustomDataPair(key, value);

#endif
		}

		/// <summary>
        /// Add a custom Data Attribute to user
        /// </summary>
        /// <param name="key">Key name of the custom data</param>
		/// <param name="value">Boolean value of the custom data</param>
		public static void SetCustomUserDataAttribute(string key, bool value) 
		{
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not setting custom data... But you are calling it so it will work on a mobile environment.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddCustomDataPair(key, value);

#endif
		}

        /// <summary>
        /// Add a custom event to be tracked in ByteBrew
        /// </summary>
        /// <param name="eventName">Name of the event (ex. shopOpened)</param>
        public static void NewCustomEvent(string eventName)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddNewCustomEvent(eventName);

#endif
        }

        /// <summary>
        /// Add a custom event to be tracked in ByteBrew
        /// </summary>
        /// <param name="eventName">Name of the event (ex. WeaponUnloced)</param>
        /// <param name="parameters">parameters that corresponds to the event (ex. Weapon: SledgeHammer, etc..)</param>
        public static void NewCustomEvent(string eventName, Dictionary<string, string> parameters)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.AddNewCustomEvent(eventName, parameters);
#endif

        }

        /// <summary>
        /// Add a custom event to be tracked in ByteBrew
        /// </summary>
        /// <param name="eventName">Name of the event (ex. WeaponUnloced)</param>
        /// <param name="value">Particular value that corresponds to the event (ex. SledgeHammer)</param>
        public static void NewCustomEvent(string eventName, string value)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.AddNewCustomEvent(eventName, value);
#endif

        }

        /// <summary>
        /// Add a progression event
        /// </summary>
        /// <param name="progressionStatus">Type of progression (ex. Start, Fail...)</param>
        /// <param name="environment">The environment that the event is happening in (ex. Tutorial, Level)</param>
        /// <param name="stage">Stage or progression that the player is in (ex. GoldLevelArena, Level_1, tutorial_menu_purchase)</param>
        [Obsolete("Progression events are legacy events and will be deprecated from the platform in upcoming releases. For enhanced analytical capabilities on the platform, make sure to implement custom events with sub-parameters.")]
        public static void NewProgressionEvent(ByteBrewProgressionTypes progressionStatus, string environment, string stage)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddProgressionEvent(progressionStatus, environment, stage);
#endif

        }

        /// <summary>
        /// Add a progression event
        /// </summary>
        /// <param name="progressionStatus">Type of progression (ex. Start, Fail...)</param>
        /// <param name="environment">The environment that the event is happening in (ex. Tutorial, Level)</param>
        /// <param name="stage">Stage or progression that the player is in (ex. GoldLevelArena, Level_1, tutorial_menu_purchase)</param>
        /// <param name="value">Value that ties to an event (ex. 500, -300, Chainsaw) </param>
        [Obsolete("Progression events are legacy events and will be deprecated from the platform in upcoming releases. For enhanced analytical capabilities on the platform, make sure to implement custom events with sub-parameters.")]
        public static void NewProgressionEvent(ByteBrewProgressionTypes progressionStatus, string environment, string stage, string value)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddProgressionEvent(progressionStatus, environment, stage, value);
#endif
        }

        /// <summary>
        /// Add a progression event
        /// </summary>
        /// <param name="progressionStatus">Type of progression (ex. Start, Fail...)</param>
        /// <param name="environment">The environment that the event is happening in (ex. Tutorial, Level)</param>
        /// <param name="stage">Stage or progression that the player is in (ex. GoldLevelArena, Level_1, tutorial_menu_purchase)</param>
        /// <param name="value">Value that ties to an event could be postive for a reward or negative for a fail (ex. 500, -300) </param>
        [Obsolete("Progression events are legacy events and will be deprecated from the platform in upcoming releases. For enhanced analytical capabilities on the platform, make sure to implement custom events with sub-parameters.")]
        public static void NewProgressionEvent(ByteBrewProgressionTypes progressionStatus, string environment, string stage, float value)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddProgressionEvent(progressionStatus, environment, stage, value);
#endif

        }

        /// <summary>
        /// Add a custom event to be tracked in ByteBrew
        /// </summary>
        /// <param name="eventName">Name of the event (ex. levelFailed)</param>
        /// <param name="value">Particular value that corresponds to the event (ex. 35)</param>
        public static void NewCustomEvent(string eventName, float value)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.AddNewCustomEvent(eventName, value);
#endif

        }

        /// <summary>
        /// Track when a Ad is shown to the user
        /// </summary>
        /// <param name="adType">Placement type of the Ad. (ex. Interstitial, Reward)</param>
        /// <param name="adLocation">The location of the shown ad. (ex. shopOpen, levelFail...)</param>
        [Obsolete("Upgrade to the newest version of the TrackAdEvent method in newer SDKs to send ad impressions with revenue parameters. This method version of the event will be deprecated in upcoming releases.")]
        public static void TrackAdEvent(ByteBrewAdTypes adType, string adLocation)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.NewTrackedAdEvent(adType.ToString(), adLocation);
#endif

        }

        /// <summary>
        /// Track when a Ad is shown to the user
        /// </summary>
        /// <param name="adType">Placement type of the Ad. (ex. Interstitial, Reward)</param>
        /// <param name="adLocation">The location of the shown ad. (ex. shopOpen, levelFail...)</param>
        /// <param name="AdID">The Ad ID or Unit ID of the ad just shown</param>
        [Obsolete("Upgrade to the newest version of the TrackAdEvent method in newer SDKs to send ad impressions with revenue parameters. This method version of the event will be deprecated in upcoming releases.")]
        public static void TrackAdEvent(ByteBrewAdTypes adType, string adLocation, string AdID)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.NewTrackedAdEvent(adType.ToString(), adLocation, AdID);
#endif

        }

        /// <summary>
        /// Track when a Ad is shown to the user
        /// </summary>
        /// <param name="adType">Placement type of the Ad. (ex. Interstitial, Reward)</param>
        /// <param name="adLocation">The location of the shown ad. (ex. shopOpen, levelFail...)</param>
        /// <param name="AdID">The Ad ID or Unit ID of the ad just shown</param>
        /// <param name="adProvider">The provider of the Ad. (ex. AdMob, IronSource)</param>
        [Obsolete("Upgrade to the newest version of the TrackAdEvent method in newer SDKs to send ad impressions with revenue parameters. This method version of the event will be deprecated in upcoming releases.")]
        public static void TrackAdEvent(ByteBrewAdTypes adType, string adLocation, string AdID, string adProvider)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.NewTrackedAdEvent(adType.ToString(), adLocation, AdID, adProvider);
#endif

        }

        /// <summary>
        /// Track when a Ad is shown to the user
        /// </summary>
        /// <param name="adType">Placement type of the Ad. (ex. Interstitial, Reward)</param>
        /// <param name="adProvider">The provider of the Ad. (ex. AdMob, IronSource)</param>
        /// <param name="adUnitName">The Ad Unit Name or ID that was used to show the impression</param>
        /// <param name="revenue">Revenue earned from the impression shown</param>
        public static void TrackAdEvent(ByteBrewAdTypes adType, string adProvider, string adUnitName, double revenue)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.NewTrackedAdEvent(adType.ToString(), adProvider, adUnitName, revenue);
#endif

        }

        /// <summary>
        /// Track when a Ad is shown to the user
        /// </summary>
        /// <param name="adType">Placement type of the Ad. (ex. Interstitial, Reward)</param>
        /// <param name="adProvider">The provider of the Ad. (ex. AdMob, IronSource)</param>
        /// <param name="adUnitName">The Ad Unit Name or ID that was used to show the impression</param>
        /// <param name="adLocation">The location of the shown ad. (ex. shopOpen, levelFail, game open...)</param>
        /// <param name="revenue">Revenue earned from the impression shown</param>
        public static void TrackAdEvent(ByteBrewAdTypes adType, string adProvider, string adUnitName, string adLocation, double revenue)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.NewTrackedAdEvent(adType.ToString(), adProvider, adUnitName, adLocation, revenue);
#endif

        }

        /// <summary>
        /// Track a purchase Event
        /// </summary>
        /// <param name="store">Store the purchase occured in (ex. Apple, Google)</param>
        /// <param name="currency">The currency used for the purchase</param>
        /// <param name="amount">The amount spent on the purchase</param>
        /// <param name="itemID">The ID or name of the item purchased</param>
        /// <param name="category">The name of the category item was purchased (ex. currency)</param>
        public static void TrackInAppPurchaseEvent(string store, string currency, float amount, string itemID, string category)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if(IsInitilized)
                ByteBrew_Helper.AddTrackedInAppPurchaseEvent(store, currency, amount, itemID, category);
#endif

        }

        /// <summary>
        /// Track a iOS purchase and validate with receipt
        /// </summary>
        /// <param name="store">Store the purchase occured in (ex. Apple, Google)</param>
        /// <param name="currency">The currency used for the purchase</param>
        /// <param name="amount">The amount spent on the purchase</param>
        /// <param name="itemID">The ID or name of the item purchased</param>
        /// <param name="category">The name of the category item was purchased (ex. currency)</param>
        /// <param name="receipt">The iOS receipt given by apple</param>
        public static void TrackiOSInAppPurchaseEvent(string store, string currency, float amount, string itemID, string category, string receipt)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddTrackediOSInAppPurchaseEvent(store, currency, amount, itemID, category, receipt);
#endif

        }

        /// <summary>
        /// Track a Google purchase and validate with receipt and signature
        /// </summary>
        /// <param name="store">Store the purchase occured in (ex. Apple, Google)</param>
        /// <param name="currency">The currency used for the purchase</param>
        /// <param name="amount">The amount spent on the purchase</param>
        /// <param name="itemID">The ID or name of the item purchased</param>
        /// <param name="category">The name of the category item was purchased (ex. currency)</param>
        /// <param name="receipt">The Google receipt given by the play store</param>
        /// <param name="signature">The Google signature provided by the play store on purchase</param>
        public static void TrackGoogleInAppPurchaseEvent(string store, string currency, float amount, string itemID, string category, string receipt, string signature)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not sending events");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.AddTrackedGoogleInAppPurchaseEvent(store, currency, amount, itemID, category, receipt, signature);
#endif

        }

        /// <summary>
        /// Validate a iOS Purchase and get callback of purchase result to tell if its a valid purchase
        /// </summary>
        /// <param name="store">Store the purchase occured in (ex. Apple, Google)</param>
        /// <param name="currency">The currency used for the purchase</param>
        /// <param name="amount">The amount spent on the purchase</param>
        /// <param name="itemID">The ID or name of the item purchased</param>
        /// <param name="category">The name of the category item was purchased (ex. currency)</param>
        /// <param name="receipt">The iOS receipt given by apple</param>
        public static void ValidateiOSInAppPurchaseEvent(string store, string currency, float amount, string itemID, string category, string receipt, Action<ByteBrewPurchaseData> purchaseResultData)
        {
            purchaseResult = null;
			purchaseResult += purchaseResultData;
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not validating purchase, giving empty results.");
			purchaseResult.Invoke(new ByteBrewPurchaseData());
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized) {
                ByteBrew_Helper.ValidateTrackediOSInAppPurchaseEvent(store, currency, amount, itemID, category, receipt);
            }
#endif

        }

        /// <summary>
        /// Validate a Google Purchase and get callback of the purchase result to tell if its a valid purchase
        /// </summary>
        /// <param name="store">Store the purchase occured in (ex. Apple, Google)</param>
        /// <param name="currency">The currency used for the purchase</param>
        /// <param name="amount">The amount spent on the purchase</param>
        /// <param name="itemID">The ID or name of the item purchased</param>
        /// <param name="category">The name of the category item was purchased (ex. currency)</param>
        /// <param name="receipt">The Google receipt given by the play store</param>
        /// <param name="signature">The Google signature provided by the play store on purchase</param>
        public static void ValidateGoogleInAppPurchaseEvent(string store, string currency, float amount, string itemID, string category, string receipt, string signature, Action<ByteBrewPurchaseData> purchaseResultData)
        {
            purchaseResult = null;
			purchaseResult += purchaseResultData;
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not validating purchase, giving empty results.");
            purchaseResult.Invoke(new ByteBrewPurchaseData());
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            if (IsInitilized)
                ByteBrew_Helper.ValidateTrackedGoogleInAppPurchaseEvent(store, currency, amount, itemID, category, receipt, signature);
#endif

        }

        /// <summary>
        /// This is used to reset and start tracking the user again.
        /// Never call this to initialize bytebrew.
        /// If you want to restart tracking if the user allows it after denying to be tracked at first
        /// call this method(RestartTracking()) followed by InitializeByteBrew().
        /// </summary>
        public static void RestartTracking()
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not tacking.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            ByteBrew_Helper.RestartTracking();
#endif

        }

        /// <summary>
        /// This will Stop Tracking in ByteBrew. This can be called anytime, specifically
        /// if you call this before calling InitializeByteBrew it will set to never track
        /// and if you do call it after it will stop sending and tracking events.
        /// Use this if the user doesnt want to be tracked at all or anymore.
        /// </summary>
        public static void StopTracking()
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, not tacking.");
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            ByteBrew_Helper.StopTracking();
#endif

        }

#if UNITY_IPHONE
        public static void requestForAppTrackingTransparency(Action<int> RequestCallBack)
        {
            ByteBrew_Helper.RequestAppTrackingTrasparency();
            OnRequestTrackTransparencyFinished += RequestCallBack;
        }

        public void ATTStatusCallback(string statusParam)
        {
            int stat = int.Parse(statusParam);
            OnRequestTrackTransparencyFinished.Invoke(stat);
        }
#endif



        public static void RemoteConfigsUpdated(Action configUpdateCallback)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, auto invoking update.");
            remoteConfigUpdated += configUpdateCallback;
            remoteConfigUpdated.Invoke();
            return;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            ByteBrew_Helper.LoadRemoteConfigs();
            remoteConfigUpdated += configUpdateCallback;
#endif

        }

        public void RemoteConfigsUpdatedCallback(string status)
        {
            remoteConfigUpdated.Invoke();
        }

		public void IAPPurchaseResultCallback(string data)
        {
			var purchaseData = JsonUtility.FromJson<ByteBrewPurchaseData>(data);
            purchaseResult.Invoke(purchaseData);
        }

        public static string GetRemoteConfigForKey(string key, string defaultValue)
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, returning default value.");
            return defaultValue;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            return ByteBrew_Helper.RetrieveRemoteConfigValue(key, defaultValue);
#else
            return defaultValue;
#endif

        }

        /// <summary>
        /// Check ByteBrew to see if the configs are already locally set, so you dont need to kep calling Load
        /// </summary>
        /// <returns>Boolean, true if the configs are set, false otherwise.</returns>
        public static bool HasRemoteConfigsBeenSet()
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, returning default true.");
            return true;
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            return ByteBrew_Helper.AreRemoteConfigsSet();
#else
            return true;
#endif

        }

        /// <summary>
        /// Get the user ID given by ByteBrew, use for reference in push notifications or debugging
        /// </summary>
        /// <returns>String, users userID.</returns>
        public static string GetUserID()
        {
#if UNITY_EDITOR
            Debug.Log("ByteBrew is in Editor Mode, returning empty string.");
            return "";
#elif (UNITY_ANDROID) || (UNITY_IOS) || (UNITY_WEBGL)

            return ByteBrew_Helper.GetCurrentUserID();
#else
            return "";
#endif

        }

    }
}