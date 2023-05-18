using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Helpers;

namespace NFTGCOShop
{
    [System.Serializable]
    public class ItemShop : ItemDTO
    {
        public ItemShop(string id, string name, string description, int cost, ItemTypeEnum itemType, ItemUniqueTypeEnum itemUniqueType, UDictionary<string, string> otherParameters, float firstPriceUpgrade, ItemTimeResetEnum itemTimeReset) : base(id, name, description, cost, itemType, itemUniqueType, otherParameters, firstPriceUpgrade, itemTimeReset)
        {

        }
    }
}