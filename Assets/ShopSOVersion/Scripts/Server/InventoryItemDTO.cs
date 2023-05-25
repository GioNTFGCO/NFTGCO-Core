using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;

namespace NFTGCOSHOP
{
    [System.Serializable]
    public class InventoryItemDTO: ItemDTO
    {
        public int stock;
        public UDictionary<string, string> paramenters = new UDictionary<string, string>();
    }
}