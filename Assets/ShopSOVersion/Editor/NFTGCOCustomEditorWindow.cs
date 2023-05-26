using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using NFTGCOSHOP;

public class NFTGCOCustomEditorWindow<T> : EditorWindow where T : new()
{
    private const float MinColumnWidth = 100f;

    private List<T> _itemObjectList = new List<T>();
    private List<T> _filteredList = new List<T>();
    private string _savePath = "Assets/Data/data.json";
    private Vector2 _scrollPosition;
    private List<PropertyInfo> _itemProperties;
    private string _filterID;
    private List<float> _columnWidths;
    private bool _resizingColumn;
    private int _resizingColumnIndex;
    private float _resizingColumnStartWidth;

    //[MenuItem("NFTGCO/Generic Custom Editor Window")]
    public static void ShowWindow()
    {
        // NFTGCOCustomEditorWindow<T> window = EditorWindow.GetWindow<NFTGCOCustomEditorWindow<T>>();
        // window.titleContent = new GUIContent("Shop Window");
        
        Rect rect = new Rect(100, 100, 800, 600); // Define las dimensiones y la posición de la ventana
        NFTGCOCustomEditorWindow<T> window = EditorWindow.GetWindowWithRect<NFTGCOCustomEditorWindow<T>>(rect);
        window.titleContent = new GUIContent("Shop Window");
    }

    private void OnEnable()
    {
        // Get the properties of the item object using reflection
        _itemProperties = new List<PropertyInfo>(typeof(T).GetProperties());

        _filteredList = new List<T>();
        // Initialize column widths
        _columnWidths = new List<float>(_itemProperties.Count);
        for (int i = 0; i < _itemProperties.Count; i++)
        {
            _columnWidths.Add(150f); // Set initial width for each column
        }
    }

    private Rect _windowRect;
    private void OnGUI()
    {
        _windowRect = new Rect(100, 100, 800, 600); // Define las dimensiones y la posición de la ventana
        
        EditorGUILayout.LabelField("Filter by ID:");
        _filterID = EditorGUILayout.TextField(_filterID);

        // Filter the list based on the ID value
        if (_itemObjectList != null)
        {
            if (_filterID != "")
            {
                _filteredList = _itemObjectList.FindAll(item => GetPropertyValue<string>(item, "ItemId").Contains(_filterID.ToString()));
            }
            else
            {
                _filteredList = _itemObjectList;
            }
        }

        EditorGUILayout.LabelField("List of Items to display in Shop:");

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition); // Start the vertical scroll view

        // Table header
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("#", GUILayout.Width(20)); // Number column

        for (int i = 0; i < _itemProperties.Count; i++)
        {
            GUILayout.BeginVertical(GUILayout.Width(_columnWidths[i]));
            GUILayout.Label(_itemProperties[i].Name);
            GUILayout.EndVertical();

            Rect separatorRect = GUILayoutUtility.GetLastRect();
            separatorRect.width = 3;
            separatorRect.x += separatorRect.width - 1;
            EditorGUIUtility.AddCursorRect(separatorRect, MouseCursor.ResizeHorizontal);

            // Handle column width resizing
            if (Event.current.type == EventType.MouseDown && separatorRect.Contains(Event.current.mousePosition))
            {
                _resizingColumn = true;
                _resizingColumnIndex = i;
                _resizingColumnStartWidth = _columnWidths[i];
                Event.current.Use();
            }
            else if (_resizingColumn && Event.current.type == EventType.MouseDrag)
            {
                float delta = Event.current.delta.x;
                _columnWidths[_resizingColumnIndex] = Mathf.Max(_resizingColumnStartWidth + delta, MinColumnWidth);
                Repaint();
            }
            else if (Event.current.type == EventType.MouseUp && _resizingColumn)
            {
                _resizingColumn = false;
                Event.current.Use();
            }
        }

        EditorGUILayout.EndHorizontal();

        // Show the list of objects
        for (int i = 0; i < _filteredList.Count; i++)
        {
            T myObject = _filteredList[i];
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label((i + 1).ToString(), GUILayout.Width(20)); // Number column

            for (int j = 0; j < _itemProperties.Count; j++)
            {
                PropertyInfo property = _itemProperties[j];
                Type propertyType = property.PropertyType;

                if (propertyType == typeof(string))
                {
                    // Handle string type
                    property.SetValue(myObject, EditorGUILayout.TextField((string)property.GetValue(myObject), GUILayout.Width(_columnWidths[j])));
                }
                else if (propertyType == typeof(int))
                {
                    // Handle int type
                    property.SetValue(myObject, EditorGUILayout.IntField((int)property.GetValue(myObject), GUILayout.Width(_columnWidths[j])));
                }
                else if (propertyType.IsEnum)
                {
                    // Handle enum type
                    property.SetValue(myObject, EditorGUILayout.EnumPopup((Enum)property.GetValue(myObject), GUILayout.Width(_columnWidths[j])));
                }
                else if (propertyType == typeof(bool))
                {
                    // Handle bool type
                    property.SetValue(myObject, EditorGUILayout.Toggle((bool)property.GetValue(myObject), GUILayout.Width(_columnWidths[j])));
                }
                else if (propertyType == typeof(float))
                {
                    // Handle float type
                    property.SetValue(myObject, EditorGUILayout.FloatField((float)property.GetValue(myObject), GUILayout.Width(_columnWidths[j])));
                }
                else if (propertyType == typeof(Dictionary<string, string>))
                {
                    // Handle Dictionary<string, string> type
                    Dictionary<string, string> dictionary = (Dictionary<string, string>)property.GetValue(myObject);

                    EditorGUILayout.BeginVertical();

                    foreach (KeyValuePair<string, string> pair in dictionary)
                    {
                        EditorGUILayout.BeginHorizontal();

                        GUILayout.Label(pair.Key, GUILayout.Width(100));
                        dictionary[pair.Key] = EditorGUILayout.TextField(pair.Value, GUILayout.Width(_columnWidths[j] - 100 - 30));

                        if (GUILayout.Button("-", GUILayout.Width(20)))
                        {
                            dictionary.Remove(pair.Key);
                            break;
                        }

                        EditorGUILayout.EndHorizontal();
                    }

                    if (GUILayout.Button("Add", GUILayout.Width(_columnWidths[j])))
                    {
                        dictionary.Add(string.Empty, string.Empty);
                    }

                    EditorGUILayout.EndVertical();
                }
                else if (propertyType == typeof(List<string>))
                {
                    // Handle List<string> type
                    List<string> list = (List<string>)property.GetValue(myObject);

                    EditorGUILayout.BeginVertical();

                    for (int k = 0; k < list.Count; k++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        list[k] = EditorGUILayout.TextField(list[k], GUILayout.Width(_columnWidths[j]));

                        if (GUILayout.Button("-", GUILayout.Width(20)))
                        {
                            list.RemoveAt(k);
                            break;
                        }

                        EditorGUILayout.EndHorizontal();
                    }

                    if (GUILayout.Button("Add", GUILayout.Width(_columnWidths[j])))
                    {
                        list.Add(string.Empty);
                    }

                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView(); // End the vertical scroll view

        GUILayout.FlexibleSpace();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Item"))
        {
            _itemObjectList.Add(new T());
        }

        if (GUILayout.Button("Remove All Items"))
        {
            _itemObjectList.Clear();
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save"))
        {
            SaveItems();
        }

        if (GUILayout.Button("Load"))
        {
            LoadItems();
        }
    }

    private TValue GetPropertyValue<TValue>(object obj, string propertyName)
    {
        PropertyInfo property = typeof(T).GetProperty(propertyName);
        if (property != null)
        {
            return (TValue)property.GetValue(obj);
        }
        return default(TValue);
    }

    private void SaveItems()
    {
        string jsonData = JsonConvert.SerializeObject(_itemObjectList, Formatting.Indented);
        File.WriteAllText(_savePath, jsonData);
        AssetDatabase.Refresh();
    }

    private void LoadItems()
    {
        if (File.Exists(_savePath))
        {
            string jsonData = File.ReadAllText(_savePath);
            _itemObjectList = JsonConvert.DeserializeObject<List<T>>(jsonData);
        }
        else
        {
            _itemObjectList = new List<T>();
        }
    }
}
