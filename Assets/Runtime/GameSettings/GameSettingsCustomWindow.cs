using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NFTGCO
{
#if UNITY_EDITOR
    public class GameSettingsCustomWindow : EditorWindow
    {
        private GameSettingsSO _gameSettings;
        
        private SerializedObject _serializedObject;
        private SerializedProperty[] _serializedProperties;

        [MenuItem("NFTGCO/GameSettingsCustomWindow")]
        public static void ShowWindow()
        {
            GetWindow<GameSettingsCustomWindow>("Game Settings Window");
        }

        // private void OnEnable()
        // {
        //     _gameSettings = GameSettingsSO.Instance;
        // }

        private void OnEnable()
        {
            _gameSettings = Resources.Load<GameSettingsSO>("GameSettings");
            _serializedObject = new SerializedObject(_gameSettings);

            var fieldInfoArray = typeof(GameSettingsSO).GetFields();
            _serializedProperties = new SerializedProperty[fieldInfoArray.Length];

            for (int i = 0; i < fieldInfoArray.Length; i++)
            {
                var fieldInfo = fieldInfoArray[i];
                _serializedProperties[i] = _serializedObject.FindProperty(fieldInfo.Name);
            }
        }
        
        private void OnGUI()
        {
            EditorGUILayout.LabelField("Game Settings", EditorStyles.boldLabel);

            foreach (var property in _serializedProperties)
            {
                EditorGUILayout.PropertyField(property);
            }

            _serializedObject.ApplyModifiedProperties();
            // GUILayout.Label("Game Settings", EditorStyles.boldLabel);
            //
            // System.Type customSettingsType = typeof(GameSettingsSO);
            // var fields = customSettingsType.GetFields();
            //
            // foreach (var field in fields)
            // {
            //     if (field.FieldType == typeof(string))
            //     {
            //         string value = (string)field.GetValue(_gameSettings);
            //         string newValue = EditorGUILayout.TextField(field.Name, value);
            //         field.SetValue(_gameSettings, newValue);
            //     }
            //     else if (field.FieldType == typeof(int))
            //     {
            //         int value = (int)field.GetValue(_gameSettings);
            //         int newValue = EditorGUILayout.IntField(field.Name, value);
            //         field.SetValue(_gameSettings, newValue);
            //     }
            //     else if (field.FieldType == typeof(bool))
            //     {
            //         bool value = (bool)field.GetValue(_gameSettings);
            //         bool newValue = EditorGUILayout.Toggle(field.Name, value);
            //         field.SetValue(_gameSettings, newValue);
            //     }
            //     else if (field.FieldType.IsEnum)
            //     {
            //         System.Enum value = (System.Enum)field.GetValue(_gameSettings);
            //         System.Enum newValue = EditorGUILayout.EnumPopup(field.Name, value);
            //         field.SetValue(_gameSettings, newValue);
            //     }
            // }

            GUILayout.Space(10);
            
            if (GUILayout.Button("Save Settings"))
            {
                SaveCustomSettings();
            }

            GUILayout.Space(10);
            
            GUILayout.Label("Current Settings:", EditorStyles.boldLabel);
            
            foreach (var field in typeof(GameSettingsSO).GetFields())
            {
                if (field.FieldType == typeof(string) || field.FieldType == typeof(int) ||
                    field.FieldType == typeof(bool) || field.FieldType.IsEnum)
                {
                    GUILayout.Label(field.Name + ": " + field.GetValue(_gameSettings).ToString());
                }
            }
        }

        private void SaveCustomSettings()
        {
            EditorUtility.SetDirty(_gameSettings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
#endif
}