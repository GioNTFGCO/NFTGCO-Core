using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCOSHOP
{
    [System.Serializable]
    public class InventoryDTO
    {
        public List<InventoryItemDTO> items = new List<InventoryItemDTO>();
    }
}