using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NFTGCOSHOP;

public class CustomEditorWindow : EditorWindow
{
    private List<ItemShopClass> myClassList = new List<ItemShopClass>(); // List of objects
    private List<ItemShopClass> filteredList = new List<ItemShopClass>(); // Filtered list of objects
    private string savePath = "Assets/Data/data.json"; // JSON file save path
    private Vector2 scrollPosition; // Scroll position of the window
    private List<PropertyInfo> itemProperties; // List of properties in ItemShopClass
    private float[] columnWidths; // Array of column widths
    private float separatorWidth = 5f; // Width of the separator
    private string idFilter = ""; // ID filter

    [MenuItem("NFTGCO/Windows/Shop Window")]
    public static void ShowWindow()
    {
        CustomEditorWindow window = EditorWindow.GetWindow<CustomEditorWindow>();
        window.titleContent = new GUIContent("Shop Window");
    }

    private void OnEnable()
    {
        // Get the properties of ItemShopClass using reflection
        itemProperties = new List<PropertyInfo>(typeof(ItemShopClass).GetProperties());

        // Initialize column widths
        columnWidths = new float[itemProperties.Count + 1];
        for (int i = 0; i < columnWidths.Length; i++)
        {
            columnWidths[i] = 100f;
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("List of Items to display in Shop:");

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition); // Start the vertical scroll view

        // Table header
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("#", GUILayout.Width(columnWidths[0])); // Row number column

        // Show column headers and separators
        for (int i = 0; i < itemProperties.Count; i++)
        {
            GUILayout.BeginVertical(GUILayout.Width(columnWidths[i + 1]));

            GUILayout.Label(itemProperties[i].Name);

            Rect separatorRect = GUILayoutUtility.GetLastRect();
            separatorRect.x += separatorRect.width - separatorWidth / 2;
            separatorRect.width = separatorWidth;
            separatorRect.height = EditorGUIUtility.currentViewWidth;
            DragSeparator(separatorRect, i + 1);

            GUILayout.EndVertical();
        }

        GUILayout.Label("Actions", GUILayout.Width(50)); // Actions column

        EditorGUILayout.EndHorizontal();

        // Apply filter if ID filter is not empty
        if (!string.IsNullOrEmpty(idFilter))
        {
            filteredList = myClassList.FindAll(obj => obj.ItemId.Contains(idFilter));
        }
        else
        {
            filteredList = myClassList;
        }

        // Show the list of objects
        for (int i = 0; i < filteredList.Count; i++)
        {
            ItemShopClass myObject = filteredList[i];
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label((i + 1).ToString(), GUILayout.Width(columnWidths[0])); // Row number

            for (int j = 0; j < itemProperties.Count; j++)
            {
                PropertyInfo property = itemProperties[j];
                Type propertyType = property.PropertyType;

                // Set column width using GUILayoutOption
                GUILayoutOption[] options = { GUILayout.Width(columnWidths[j + 1]) };

                if (propertyType == typeof(string))
                {
                    property.SetValue(myObject, EditorGUILayout.TextField((string)property.GetValue(myObject), options));
                }
                else if (propertyType == typeof(int))
                {
                    property.SetValue(myObject, EditorGUILayout.IntField((int)property.GetValue(myObject), options));
                }
                else if (propertyType.IsEnum)
                {
                    property.SetValue(myObject, EditorGUILayout.EnumPopup((Enum)property.GetValue(myObject), options));
                }
            }

            // Button to delete the current object
            if (GUILayout.Button("Delete", GUILayout.Width(50)))
            {
                myClassList.Remove(myObject);
                break;
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView(); // End the vertical scroll view

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("ID Filter:", GUILayout.Width(70));
        idFilter = EditorGUILayout.TextField(idFilter);

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        // Button to add a new object to the list
        if (GUILayout.Button("Add Object"))
        {
            myClassList.Add(new ItemShopClass());
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
        string jsonData = JsonConvert.SerializeObject(myClassList, Formatting.Indented);
        File.WriteAllText(savePath, jsonData);
        AssetDatabase.Refresh();
        Debug.Log("Data saved to " + savePath);
    }

    // Load the data from a JSON file
    private void LoadDataFromJson()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            myClassList = JsonConvert.DeserializeObject<List<ItemShopClass>>(jsonData);
            Debug.Log("Data loaded from " + savePath);
        }
        else
        {
            Debug.LogWarning("JSON file not found at " + savePath);
        }
    }

    // Handle dragging of column separators
    private void DragSeparator(Rect separatorRect, int columnIndex)
    {
        EditorGUIUtility.AddCursorRect(separatorRect, MouseCursor.ResizeHorizontal);

        if (Event.current.type == EventType.MouseDown && separatorRect.Contains(Event.current.mousePosition))
        {
            // Start dragging separator
            EditorGUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
            Event.current.Use();
        }
        else if (Event.current.type == EventType.MouseDrag && EditorGUIUtility.hotControl == GUIUtility.GetControlID(FocusType.Passive))
        {
            // Drag separator
            columnWidths[columnIndex] += Event.current.delta.x;
            columnWidths[columnIndex] = Mathf.Max(columnWidths[columnIndex], 50f); // Minimum width
            Repaint();
        }
        else if (Event.current.type == EventType.MouseUp && EditorGUIUtility.hotControl == GUIUtility.GetControlID(FocusType.Passive))
        {
            // Stop dragging separator
            EditorGUIUtility.hotControl = 0;
            Event.current.Use();
        }
    }
}
