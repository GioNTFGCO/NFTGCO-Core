using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NFTGCOSHOP;

public class CustomShopItemsWindow : EditorWindow
{
    private const float MinColumnWidth = 100f;

    private List<ItemShop> _itemshopObjectList = new List<ItemShop>();
    private List<ItemShop> _filteredList = new List<ItemShop>();
    private string _savePath = "Assets/Data/data.json";
    private Vector2 _scrollPosition;
    private List<PropertyInfo> _itemProperties;
    private string _filterID;
    private List<float> _columnWidths;

    //[MenuItem("NFTGCO/Shop Window")]
    public static void ShowWindow()
    {
        CustomShopItemsWindow window = EditorWindow.GetWindow<CustomShopItemsWindow>();
        window.titleContent = new GUIContent("Shop Window");
    }

    private void OnEnable()
    {
        // Get the properties of ItemShopObject using reflection
        _itemProperties = new List<PropertyInfo>(typeof(ItemShop).GetProperties());

        _filteredList = _itemshopObjectList;
        // Initialize column widths
        _columnWidths = new List<float>(_itemProperties.Count);
        for (int i = 0; i < _itemProperties.Count; i++)
        {
            _columnWidths.Add(150f); // Set initial width for each column
        }

        LoadDataFromJson();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Filter by ID:");
        _filterID = EditorGUILayout.TextField(_filterID);

        // Filter the list based on the ID value
        if (_itemshopObjectList != null)
        {
            if (string.IsNullOrEmpty(_filterID))
            {
                _filteredList = _itemshopObjectList;
            }
            else
            {
                _filteredList = _itemshopObjectList.FindAll(item => item.ItemId.Contains(_filterID.ToString()));
            }
        }

        EditorGUILayout.LabelField("List of Items to display in Shop:");

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition); // Start the vertical scroll view

        // Table header
        EditorGUILayout.BeginHorizontal();
        try
        {
            GUILayout.Label("#", GUILayout.Width(20)); // Number column

            for (int i = 0; i < _itemProperties.Count; i++)
            {
                GUILayout.BeginVertical(GUILayout.Width(_columnWidths[i]));
                GUILayout.Label(_itemProperties[i].Name);
                GUILayout.EndVertical();
            }

            GUILayout.Label("Actions", GUILayout.Width(50)); // Actions column
        }
        finally
        {
            EditorGUILayout.EndHorizontal();
        }

        // Show the list of objects
        for (int i = 0; i < _filteredList.Count; i++)
        {
            ItemShop myObject = _filteredList[i];
            EditorGUILayout.BeginHorizontal();
            try
            {
                GUILayout.Label((i + 1).ToString(), GUILayout.Width(20)); // Number column

                for (int j = 0; j < _itemProperties.Count; j++)
                {
                    PropertyInfo property = _itemProperties[j];
                    Type propertyType = property.PropertyType;

                    if (propertyType == typeof(string))
                    {
                        property.SetValue(myObject,
                            EditorGUILayout.TextField((string)property.GetValue(myObject),
                                GUILayout.Width(_columnWidths[j])));
                    }
                    else if (propertyType == typeof(int))
                    {
                        property.SetValue(myObject,
                            EditorGUILayout.IntField((int)property.GetValue(myObject),
                                GUILayout.Width(_columnWidths[j])));
                    }
                    else if (propertyType.IsEnum)
                    {
                        property.SetValue(myObject,
                            EditorGUILayout.EnumPopup((Enum)property.GetValue(myObject),
                                GUILayout.Width(_columnWidths[j])));
                    }
                    else if (propertyType == typeof(bool))
                    {
                        property.SetValue(myObject,
                            EditorGUILayout.Toggle((bool)property.GetValue(myObject),
                                GUILayout.Width(_columnWidths[j])));
                    }
                    else if (propertyType == typeof(float))
                    {
                        property.SetValue(myObject,
                            EditorGUILayout.FloatField((float)property.GetValue(myObject),
                                GUILayout.Width(_columnWidths[j])));
                    }
                    else if (propertyType == typeof(Dictionary<string, string>))
                    {
                        Dictionary<string, string> dictionary = (Dictionary<string, string>)property.GetValue(myObject);

                        EditorGUILayout.BeginVertical();

                        // Create a copy of the keys
                        List<string> keys = new List<string>(dictionary.Keys);

                        foreach (var key in keys)
                        {
                            EditorGUILayout.BeginHorizontal();
                            try
                            {
                                string value = dictionary[key];

                                string newKey =
                                    EditorGUILayout.TextField(key, GUILayout.Width(_columnWidths[j] * 0.4f));
                                string newValue =
                                    EditorGUILayout.TextField(value, GUILayout.Width(_columnWidths[j] * 0.4f));

                                // Update the dictionary with the new key-value pair
                                if (newKey != key)
                                {
                                    dictionary.Remove(key);
                                    dictionary[newKey] = newValue;
                                }
                                else
                                {
                                    dictionary[key] = newValue;
                                }

                                if (GUILayout.Button("x", GUILayout.Width(_columnWidths[j] * 0.2f)))
                                {
                                    dictionary.Remove(key);
                                }
                            }
                            finally
                            {
                                EditorGUILayout.EndHorizontal();
                            }
                        }

                        if (GUILayout.Button("Add", GUILayout.Width(_columnWidths[j])))
                        {
                            dictionary.Add(string.Empty, string.Empty);
                        }

                        EditorGUILayout.EndVertical();
                    }
                    else if (propertyType == typeof(List<string>))
                    {
                        List<string> list = (List<string>)property.GetValue(myObject);

                        EditorGUILayout.BeginVertical();

                        for (int k = 0; k < list.Count; k++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            try
                            {
                                list[k] = EditorGUILayout.TextField(list[k], GUILayout.Width(_columnWidths[j]));

                                if (GUILayout.Button("-", GUILayout.Width(20)))
                                {
                                    list.RemoveAt(k);
                                    break;
                                }
                            }
                            finally
                            {
                                EditorGUILayout.EndHorizontal();
                            }
                        }

                        if (GUILayout.Button("Add", GUILayout.Width(_columnWidths[j])))
                        {
                            list.Add(string.Empty);
                        }

                        EditorGUILayout.EndVertical();
                    }
                }

                // Button to delete the current object
                if (GUILayout.Button("Delete", GUILayout.Width(50)))
                {
                    _itemshopObjectList.Remove(myObject);
                    break;
                }
            }
            finally
            {
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView(); // End the vertical scroll view

        EditorGUILayout.Space();

        // Button to add a new object to the list
        if (GUILayout.Button("Add Object"))
        {
            _itemshopObjectList.Add(new ItemShop());
        }

        EditorGUILayout.Space();

        // Button to save the data to a JSON file
        if (GUILayout.Button("Save Data"))
        {
            SaveDataToJson();
        }

        // Button to load the data from a JSON file
        if (GUILayout.Button("Load Data"))
        {
            LoadDataFromJson();
        }
    }

    // Save the data to a JSON file
    private void SaveDataToJson()
    {
        string jsonData = JsonConvert.SerializeObject(_itemshopObjectList, Formatting.Indented);
        File.WriteAllText(_savePath, jsonData);
        AssetDatabase.Refresh();
        Debug.Log("Data saved to " + _savePath);
    }

    // Load the data from a JSON file
    private void LoadDataFromJson()
    {
        if (File.Exists(_savePath))
        {
            string jsonData = File.ReadAllText(_savePath);
            _itemshopObjectList = JsonConvert.DeserializeObject<List<ItemShop>>(jsonData);
            Debug.Log("Data loaded from " + _savePath);
        }
        else
        {
            Debug.LogWarning("JSON file not found at " + _savePath);
        }
    }
}