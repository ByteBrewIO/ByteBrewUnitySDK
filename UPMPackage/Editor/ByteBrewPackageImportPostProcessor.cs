using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class ByteBrewOnLoadPackageImportCredsHolder {
    public static ByteBrewSettings BBSettings = null;

    public static string AndroidEnabledPlayerPrefsKey = "BYTEBREW_ANDROID_ENABLED";
    public static string IOSEnabledPlayerPrefsKey = "BYTEBREW_IOS_ENABLED";
    public static string WebEnabledPlayerPrefsKey = "BYTEBREW_WEB_ENABLED";

    public static string AndroidGameIDPlayerPrefsKey = "BYTEBREW_ANDROID_GAME_ID";
    public static string AndroidSDKKeyPlayerPrefsKey = "BYTEBREW_ANDROID_SDK_KEY";

    public static string IOSGameIDPlayerPrefsKey = "BYTEBREW_IOS_GAME_ID";
    public static string IOSSDKKeyPlayerPrefsKey = "BYTEBREW_IOS_SDK_KEY";

    public static string WebGameIDPlayerPrefsKey = "BYTEBREW_WEB_GAME_ID";
    public static string WebSDKKeyPlayerPrefsKey = "BYTEBREW_WEB_SDK_KEY";

    public static bool AndroidEnabled => PlayerPrefs.GetInt(AndroidEnabledPlayerPrefsKey, 0) == 1;
    public static bool IOSEnabled => PlayerPrefs.GetInt(IOSEnabledPlayerPrefsKey, 0) == 1;
    public static bool WebEnabled => PlayerPrefs.GetInt(WebEnabledPlayerPrefsKey, 0) == 1;

    public static string AndroidGameID => PlayerPrefs.GetString(AndroidGameIDPlayerPrefsKey, "");
    public static string AndroidGameKey => PlayerPrefs.GetString(AndroidSDKKeyPlayerPrefsKey, "");

    public static string IOSGameID => PlayerPrefs.GetString(IOSGameIDPlayerPrefsKey, "");
    public static string IOSGameKey => PlayerPrefs.GetString(IOSSDKKeyPlayerPrefsKey, "");

    public static string WebGameID => PlayerPrefs.GetString(WebGameIDPlayerPrefsKey, "");
    public static string WebGameKey => PlayerPrefs.GetString(WebSDKKeyPlayerPrefsKey, "");

    static ByteBrewOnLoadPackageImportCredsHolder() {
        SetSDKKeysToPlayerPrefs();
    }

    public static void SetSDKKeysToPlayerPrefs() {
        if (BBSettings == null) {
            BBSettings = ByteBrewSettingsHandler.SettingsInstance;
        }

        if (BBSettings == null) {
            return;
        }

        if (BBSettings.androidEnabled) {
            PlayerPrefs.SetInt(AndroidEnabledPlayerPrefsKey, 1);
            PlayerPrefs.SetString(AndroidGameIDPlayerPrefsKey, BBSettings.androidGameID);
            PlayerPrefs.SetString(AndroidSDKKeyPlayerPrefsKey, BBSettings.androidSDKKey);
        }

        if (BBSettings.iosEnabled) {
            PlayerPrefs.SetInt(IOSEnabledPlayerPrefsKey, 1);
            PlayerPrefs.SetString(IOSGameIDPlayerPrefsKey, BBSettings.iosGameID);
            PlayerPrefs.SetString(IOSSDKKeyPlayerPrefsKey, BBSettings.iosSDKKey);
        }

        if (BBSettings.webEnabled) {
            PlayerPrefs.SetInt(WebEnabledPlayerPrefsKey, 1);
            PlayerPrefs.SetString(WebGameIDPlayerPrefsKey, BBSettings.webGameID);
            PlayerPrefs.SetString(WebSDKKeyPlayerPrefsKey, BBSettings.webSDKKey);
        }
    }

    public static void RemoveAndroidPrefs() {
        PlayerPrefs.SetInt(AndroidEnabledPlayerPrefsKey, 0);
        PlayerPrefs.DeleteKey(AndroidGameIDPlayerPrefsKey);
        PlayerPrefs.DeleteKey(AndroidSDKKeyPlayerPrefsKey);
    }

    public static void RemoveIOSPrefs() {
        PlayerPrefs.SetInt(IOSEnabledPlayerPrefsKey, 0);
        PlayerPrefs.DeleteKey(IOSGameIDPlayerPrefsKey);
        PlayerPrefs.DeleteKey(IOSSDKKeyPlayerPrefsKey);
    }

    public static void RemoveWebPrefs() {
        PlayerPrefs.SetInt(WebEnabledPlayerPrefsKey, 0);
        PlayerPrefs.DeleteKey(WebGameIDPlayerPrefsKey);
        PlayerPrefs.DeleteKey(WebSDKKeyPlayerPrefsKey);
    }
}

public class ByteBrewPackageImportPostProcessor : AssetPostprocessor {
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
        EditorApplication.delayCall += DelayedMethod;
    }

    private static void DelayedMethod() {
        EditorApplication.delayCall -= DelayedMethod;

        ByteBrewSettings bbSettings = ByteBrewSettingsHandler.SettingsInstance;

        if (bbSettings == null) {
            Debug.LogError("ByteBrewSettings not found in project.");
            return;
        }

        if (ByteBrewOnLoadPackageImportCredsHolder.AndroidEnabled) {
            bbSettings.androidEnabled = true;
            bbSettings.androidGameID = ByteBrewOnLoadPackageImportCredsHolder.AndroidGameID;
            bbSettings.androidSDKKey = ByteBrewOnLoadPackageImportCredsHolder.AndroidGameKey;
        }

        if (ByteBrewOnLoadPackageImportCredsHolder.IOSEnabled) {
            bbSettings.iosEnabled = true;
            bbSettings.iosGameID = ByteBrewOnLoadPackageImportCredsHolder.IOSGameID;
            bbSettings.iosSDKKey = ByteBrewOnLoadPackageImportCredsHolder.IOSGameKey;
        }

        if (ByteBrewOnLoadPackageImportCredsHolder.WebEnabled) {
            bbSettings.webEnabled = true;
            bbSettings.webGameID = ByteBrewOnLoadPackageImportCredsHolder.WebGameID;
            bbSettings.webSDKKey = ByteBrewOnLoadPackageImportCredsHolder.WebGameKey;
        }
    }
}
