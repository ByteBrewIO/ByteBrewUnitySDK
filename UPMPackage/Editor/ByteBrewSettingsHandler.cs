using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class ByteBrewSettingsHandler
{
    private static ByteBrewSettings _settingsInstance = null;
    public static ByteBrewSettings SettingsInstance 
    {
        get {
            if (_settingsInstance == null) {
                Debug.LogError("ByteBrewSettings.asset SettingsInstance was accessed before initialization.");
            }
            return _settingsInstance;
        }
    }

    // Static constructor runs on load but we delay the actual initialization.
    static ByteBrewSettingsHandler() 
    {
        // Delay initialization until after the AssetDatabase is ready.
        EditorApplication.delayCall += Initialize;
    }

    private static void Initialize()
    {
        _settingsInstance = GetOrCreateByteBrewSettings();
        // We call this here to ensure that the settings are loaded and available.
        ByteBrewOnLoadPackageImportCredsHolder.SetSDKKeysToPlayerPrefs();
    }

    private static ByteBrewSettings GetOrCreateByteBrewSettings() 
    {
        string bytebrewSettingsDirPath = Path.Combine("Assets", "ByteBrewSDK", "Resources");
        string bytebrewSettingsPath = Path.Combine(bytebrewSettingsDirPath, "ByteBrewSettings.asset");

        AssetDatabase.Refresh();

        ByteBrewSettings settings = AssetDatabase.LoadAssetAtPath<ByteBrewSettings>(bytebrewSettingsPath);
        if (settings != null) {
            Debug.Log("ByteBrewSettings.asset loaded successfully");
            return settings;
        }

        if (!Directory.Exists(bytebrewSettingsDirPath)) {
            Directory.CreateDirectory(bytebrewSettingsDirPath);
            AssetDatabase.Refresh();
        }

        try {
            settings = ScriptableObject.CreateInstance<ByteBrewSettings>();

            AssetDatabase.CreateAsset(settings, bytebrewSettingsPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("ByteBrewSettings.asset created successfully");
        } catch (System.Exception e) {
            Debug.LogError("Error creating ByteBrewSettings.asset: " + e.Message);
        }

        return settings;
    }
}
