using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCOSHOP
{
    public class InventoryItemDTO: ItemDTO
    {
        public int stock;
        public Dictionary<string, string> paramenters = new Dictionary<string, string>();
    }
}