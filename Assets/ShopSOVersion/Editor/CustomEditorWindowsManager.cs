using System.Collections;
using System.Collections.Generic;
using NFTGCOSHOP;
using UnityEditor;
using UnityEngine;

public static class CustomEditorWindowsManager
{
    [MenuItem("NFTGCO/Generic Item Shop Editor Window")]
    public static void ShowWindow()
    {
        NFTGCOCustomEditorWindow<Item>.ShowWindow();
    }

    static CustomEditorWindowsManager()
    {
        EditorApplication.delayCall += ShowWindow;
    }
}
public class Item
{
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }

    public Item()
    {
        Name = "New Item";
        Price = 0;
        Description = "New Description";
    }
}