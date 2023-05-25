using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class CustomWindowEditor : EditorWindow
{
    private List<Dictionary<string, object>> items;
    private List<ParameterInfo> parameters;

    [MenuItem("Window/Custom Window Editor")]
    public static void OpenWindow()
    {
        CustomWindowEditor window = GetWindow<CustomWindowEditor>();
        window.titleContent = new GUIContent("Custom Editor");
        window.Show();
    }

    private void OnEnable()
    {
        items = new List<Dictionary<string, object>>();
        parameters = new List<ParameterInfo>();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        // Display the parameters
        DisplayParameters();

        // Display the table form
        DisplayTableForm();

        // Add new item button
        if (GUILayout.Button("Add Item"))
        {
            AddItem();
        }

        // Remove last item button
        if (GUILayout.Button("Remove Last Item"))
        {
            RemoveLastItem();
        }

        EditorGUILayout.EndVertical();

        // Save and Load buttons
        if (GUILayout.Button("Save"))
        {
            SaveData();
        }

        if (GUILayout.Button("Load"))
        {
            LoadData();
        }
    }

    private void DisplayParameters()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Parameters:");

        foreach (ParameterInfo parameter in parameters)
        {
            EditorGUILayout.LabelField(parameter.Name);
        }

        EditorGUILayout.Space();
    }

    private void DisplayTableForm()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Dictionary<string, object> item = items[i];

            EditorGUILayout.BeginVertical(GUI.skin.box);

            // Display the properties of the item
            foreach (KeyValuePair<string, object> property in item)
            {
                string propertyName = property.Key;
                object propertyValue = property.Value;

                // Display the property name and input field
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(propertyName);
                item[propertyName] = DisplayPropertyValue(propertyValue);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }

    private object DisplayPropertyValue(object value)
    {
        // Display the property value based on its type
        if (value is int)
        {
            return EditorGUILayout.IntField((int)value);
        }
        else if (value is float)
        {
            return EditorGUILayout.FloatField((float)value);
        }
        else if (value is string)
        {
            return EditorGUILayout.TextField((string)value);
        }
        else if (value is bool)
        {
            return EditorGUILayout.Toggle((bool)value);
        }
        else if (value is Vector3)
        {
            return EditorGUILayout.Vector3Field("", (Vector3)value);
        }
        // Add additional cases for other supported property types

        return value;
    }

    private void AddItem()
    {
        Dictionary<string, object> newItem = new Dictionary<string, object>();
        items.Add(newItem);
    }

    private void RemoveLastItem()
    {
        if (items.Count > 0)
        {
            items.RemoveAt(items.Count - 1);
        }
    }

    private void SaveData()
    {
        // Implement the logic to save your data here
        // Example using PlayerPrefs:

        string serializedData = JsonUtility.ToJson(items);
        PlayerPrefs.SetString("CustomWindowEditorData", serializedData);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        // Implement the logic to load your data here
        // Example using PlayerPrefs:

        string serializedData = PlayerPrefs.GetString("CustomWindowEditorData");
        items = JsonUtility.FromJson<List<Dictionary<string, object>>>(serializedData);
    }

    private void OnSelectionChange()
    {
        parameters.Clear();

        if (Selection.activeGameObject != null)
        {
            Component[] components = Selection.activeGameObject.GetComponents<Component>();

            foreach (Component component in components)
            {
                MethodInfo[] methods = component.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "MyMethod") // Replace "MyMethod" with the actual method name you want to inspect
                    {
                        parameters.AddRange(method.GetParameters());
                    }
                }
            }
        }

        Repaint();
    }
}


[System.Serializable]
public class MyClass
{
    public string property1;
    public int property2;
}

public class MyClassTable
{
    private List<MyClass> items;

    public MyClassTable()
    {
        items = new List<MyClass>();
    }

    public List<MyClass> GetItems()
    {
        return items;
    }

    public void SetItems(List<MyClass> newItems)
    {
        items = newItems;
    }

    public void AddItem()
    {
        MyClass newItem = new MyClass();
        items.Add(newItem);
    }

    public void RemoveLastItem()
    {
        if (items.Count > 0)
        {
            items.RemoveAt(items.Count - 1);
        }
    }
}
