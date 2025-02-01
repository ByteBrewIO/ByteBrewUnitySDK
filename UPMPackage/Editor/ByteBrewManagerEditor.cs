using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ByteBrewSDK
{
    [System.Serializable]
    [CustomEditor(typeof(ByteBrewSettings))]
    public class ByteBrewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            ByteBrewSettings manager = ByteBrewSettingsHandler.SettingsInstance;

            Texture bytebrewLogo = (Texture)Resources.Load("bytebrewfulllogo", typeof(Texture));
            if (bytebrewLogo != null) {
                GUI.DrawTexture(new Rect(15f, 30f, 275f, 75f), bytebrewLogo);
            }

            GUILayout.Space(100f); //2
            GUILayout.Label($"ByteBrew Unity SDK v{ByteBrew.SDK_VERSION}", EditorStyles.helpBox);
            GUILayout.Space(2f);
            EditorGUILayout.HelpBox("Go to your games setting in the ByteBrew Dashboard to get the right keys.", MessageType.Info);
            GUILayout.Space(10f);
            GUILayout.Label("ByteBrew App Settings", EditorStyles.boldLabel);

            GUILayout.Space(15f);

            bool originalAndroidEnabled = manager.androidEnabled;
            string originalAndroidGameID = manager.androidGameID;
            string originalAndroidSDKKey = manager.androidSDKKey;

            bool originalIOSEnabled = manager.iosEnabled;
            string originalIOSGameID = manager.iosGameID;
            string originalIOSSDKKey = manager.iosSDKKey;

            bool originalWebEnabled = manager.webEnabled;
            string originalWebGameID = manager.webGameID;
            string originalWebSDKKey = manager.webSDKKey;

            if (!manager.androidEnabled)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Enable Android Settings", GUILayout.Width(300f)))
                {
                    manager.androidEnabled = true;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Label("Android Settings", EditorStyles.centeredGreyMiniLabel);
                GUILayout.Space(5f);
                GUILayout.Label("Android Game ID");
                manager.androidGameID = GUILayout.TextField(manager.androidGameID, GUILayout.Width(250f));
                GUILayout.Space(10f);
                GUILayout.Label("Android Game SDK Key");
                manager.androidSDKKey = GUILayout.TextField(manager.androidSDKKey, GUILayout.Width(250f));

                GUILayout.Space(5f);
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Remove Android Settings", GUILayout.Width(300f)))
                {
                    manager.androidGameID = "";
                    manager.androidSDKKey = "";
                    manager.androidEnabled = false;
                    ByteBrewOnLoadPackageImportCredsHolder.RemoveAndroidPrefs();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }


            GUILayout.Space(15f);

            if (!manager.iosEnabled)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Enable iOS Settings", GUILayout.Width(300f)))
                {
                    manager.iosEnabled = true;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Label("iOS Settings", EditorStyles.centeredGreyMiniLabel);
                GUILayout.Space(5f);
                GUILayout.Label("iOS Game ID");
                manager.iosGameID = GUILayout.TextField(manager.iosGameID, GUILayout.Width(250f));
                GUILayout.Space(10f);
                GUILayout.Label("iOS Game SDK Key");
                manager.iosSDKKey = GUILayout.TextField(manager.iosSDKKey, GUILayout.Width(250f));

                GUILayout.Space(5f);
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Remove iOS Settings", GUILayout.Width(300f)))
                {
                    manager.iosGameID = "";
                    manager.iosSDKKey = "";
                    manager.iosEnabled = false;
                    ByteBrewOnLoadPackageImportCredsHolder.RemoveIOSPrefs();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }

            GUILayout.Space(15f);

            if (!manager.webEnabled) {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Enable Web Settings", GUILayout.Width(300f))) {
                    manager.webEnabled = true;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            } else {
                GUILayout.Label("Web Settings", EditorStyles.centeredGreyMiniLabel);
                GUILayout.Space(5f);
                GUILayout.Label("Web Game ID");
                manager.webGameID = GUILayout.TextField(manager.webGameID, GUILayout.Width(250f));
                GUILayout.Space(10f);
                GUILayout.Label("Web Game SDK Key");
                manager.webSDKKey = GUILayout.TextField(manager.webSDKKey, GUILayout.Width(250f));

                GUILayout.Space(5f);
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Remove Web Settings", GUILayout.Width(300f))) {
                    manager.webGameID = "";
                    manager.webSDKKey = "";
                    manager.webEnabled = false;
                    ByteBrewOnLoadPackageImportCredsHolder.RemoveWebPrefs();
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }


            GUILayout.Space(20f); //2
            GUILayout.Label("ByteBrew Extra Help", EditorStyles.boldLabel);
            GUILayout.Space(10f);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("ByteBrew Dashboard", GUILayout.Width(300f)))
            {
                Help.BrowseURL("https://dashboard.bytebrew.io");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(10f);
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("ByteBrew Docs", GUILayout.Width(300f)))
            {
                Help.BrowseURL("https://docs.bytebrew.io");
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            EditorUtility.SetDirty(manager);
            serializedObject.ApplyModifiedProperties();

            if (originalAndroidEnabled != manager.androidEnabled
                || originalAndroidGameID != manager.androidGameID
                || originalAndroidSDKKey != manager.androidSDKKey) {
                ByteBrewOnLoadPackageImportCredsHolder.SetAndroidKeysToPlayerPrefs();
            }

            if (originalIOSEnabled != manager.iosEnabled
                || originalIOSGameID != manager.iosGameID
                || originalIOSSDKKey != manager.iosSDKKey) {
                ByteBrewOnLoadPackageImportCredsHolder.SetIOSKeysToPlayerPrefs();
            }
            
            if (originalWebEnabled != manager.webEnabled
                || originalWebGameID != manager.webGameID
                || originalWebSDKKey != manager.webSDKKey) {
                ByteBrewOnLoadPackageImportCredsHolder.SetWebKeysToPlayerPrefs();
            }
        }
    }

}

