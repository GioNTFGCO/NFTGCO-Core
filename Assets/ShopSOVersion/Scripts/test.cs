using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCOSHOP;

public class test : MonoBehaviour
{
    public ItemsFromServer _itemsDTO;
    public InventoryDTO _userInventory;
    void Start()
    {
        string jsonData = JsonUtility.ToJson(_userInventory);
        Debug.Log(jsonData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
