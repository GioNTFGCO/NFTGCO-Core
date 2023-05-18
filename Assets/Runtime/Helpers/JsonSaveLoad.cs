using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace NFTGCO.Helpers
{
    public abstract class JsonSaveLoad<T> : MonoBehaviour where T : class
    {
        private static JsonSaveLoad<T> instance;
        protected string _filePath;

        [Header("Inspector Buttons")]
        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton LoadButton = new NFTGCO.Helpers.InspectorButton("LoadData");
        [SerializeField] private NFTGCO.Helpers.InspectorButton SaveButton = new NFTGCO.Helpers.InspectorButton("SaveData");
        [SerializeField] private NFTGCO.Helpers.InspectorButton ClearButton = new NFTGCO.Helpers.InspectorButton("ClearData");

        protected virtual void Awake()
        {
            _filePath = CreateJsonPath();
        }
        protected virtual string CreateJsonPath()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Path.Combine(Application.temporaryCachePath, $"{typeof(T).Name}_.json");
#else
            return Path.Combine(Application.persistentDataPath, $"{typeof(T).Name}_.json");
#endif
        }
        protected virtual void Save(T saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData);
            File.WriteAllText(_filePath, jsonData);
            Debug.Log($"Save file: {_filePath}");
        }
        protected virtual T Load()
        {
            if (File.Exists(_filePath))
            {
                string jsonData = File.ReadAllText(_filePath);
                int index = _filePath.LastIndexOf('\\');
                var jsonName = _filePath.Substring(index + 1);
                Debug.Log($"Load file: {jsonName}");
                return JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                Debug.LogWarning($"Save file not found: {_filePath}");
                return null;
            }
        }
        protected virtual void ClearFile()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
    }
}