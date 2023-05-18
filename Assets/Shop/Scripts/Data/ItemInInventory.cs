using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using NFTGCOShop;
using UnityEngine;

namespace NFTGCO.Inventory
{
    [System.Serializable]
    public class ItemInInventory : ItemDTO
    {
        public int stock;

        public ItemInInventory(string id, string name, string description, int cost, ItemTypeEnum itemType, ItemUniqueTypeEnum itemUniqueType, UDictionary<string, string> otherParameters, float firstPriceUpgrade, ItemTimeResetEnum itemTimeReset) : base(id, name, description, cost, itemType, itemUniqueType, otherParameters, firstPriceUpgrade, itemTimeReset)
        {

        }
    }
}