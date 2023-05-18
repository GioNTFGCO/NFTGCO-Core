using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Helpers;

namespace NFTGCOShop
{
    [System.Serializable]
    public class ItemDTO
    {
        public string itemId;
        public string itemName;
        public string itemDescription;
        public int itemCost;
        public ItemTypeEnum itemType;
        [EnumCondition("itemType", (int)ItemTypeEnum.Unique)]
        public ItemUniqueTypeEnum itemUniqueType;
        [EnumCondition("itemUniqueType", (int)ItemUniqueTypeEnum.Weapon)]
        public UDictionary<string, string> otherParameters = new UDictionary<string, string>();
        [EnumCondition("itemUniqueType", (int)ItemUniqueTypeEnum.Weapon)]
        public float firstPriceUpgrade;
        //at the moment this field is not gonna used in any part of the code, but it can be used in the future, after the MVP!
        public ItemTimeResetEnum itemTimeReset;

        public ItemDTO(string id, string name, string description, int cost, ItemTypeEnum itemType, ItemUniqueTypeEnum itemUniqueType, UDictionary<string, string> otherParameters, float firstPriceUpgrade,ItemTimeResetEnum itemTimeReset)
        {
            itemId = id;
            itemName = name;
            itemDescription = description;
            itemCost = cost;

            this.itemType = itemType;
            this.itemUniqueType = itemUniqueType;
            this.otherParameters = otherParameters;
            this.firstPriceUpgrade = firstPriceUpgrade;
            this.itemTimeReset = itemTimeReset;
        }
    }
}