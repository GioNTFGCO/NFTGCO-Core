using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCOShop;
using NFTGCO.Helpers;
using NFTGCO.Inventory;
using System;

namespace NFTGCO
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        //public static System.Action<string, ItemShop, System.Action> OnAddItemToPlayerInventory;
        public static System.Action<string, ItemShop, System.Action> OnAddItemToGameInventory;
        public static System.Action OnRemoveItemFromInventory;
        private void OnEnable()
        {
            //OnAddItemToPlayerInventory += AddItemToInventory;
            OnAddItemToGameInventory += AddItemToGameInventory;
            OnRemoveItemFromInventory += RemoveItemFromInventory;
        }
        private void OnDisable()
        {
            //OnAddItemToPlayerInventory -= AddItemToInventory;
            OnAddItemToGameInventory -= AddItemToGameInventory;
            OnRemoveItemFromInventory -= RemoveItemFromInventory;
        }

        // private void AddItemToInventory(string itemID, ItemShop itemShop, System.Action OnAdd)
        // {
        //     AddItem(itemID, itemShop, OnAdd, DataRecieverManager.Instance.PlayerData.ServerData.itemsInInventory);
        // }
        private void AddItemToGameInventory(string itemID, ItemShop itemShop, System.Action OnAdd)
        {
            AddItem(itemID, itemShop, OnAdd, DataRecieverManager.Instance.GameInventoryData.ServerData.itemsInInventory);
        }

        private void AddItem(string itemID, ItemShop itemShop, System.Action OnAdd, UDictionary<string, ItemInInventory> whereToAdd)
        {
            ItemInInventory itemInInventory = new ItemInInventory(itemShop.itemId, itemShop.itemName, itemShop.itemDescription, itemShop.itemCost, itemShop.itemType, itemShop.itemUniqueType, itemShop.otherParameters, itemShop.firstPriceUpgrade, itemShop.itemTimeReset);

            if (itemShop.itemType == ItemTypeEnum.Non_Unique)
            {
                if (!whereToAdd.ContainsKey(itemID))
                {
                    whereToAdd.Add(itemID, itemInInventory);
                    OnAdd?.Invoke();
                }
                else
                {
                    whereToAdd[itemID].stock++;
                    OnAdd?.Invoke();
                }
            }
            else
            {
                if (!whereToAdd.ContainsKey(itemID))
                {
                    whereToAdd.Add(itemID, itemInInventory);
                    OnAdd?.Invoke();
                }
                else
                    Debug.LogWarning($"{itemID} already exists in data base.");
            }
        }
        private void RemoveItemFromInventory()
        {
            throw new NotImplementedException();
        }
    }
}