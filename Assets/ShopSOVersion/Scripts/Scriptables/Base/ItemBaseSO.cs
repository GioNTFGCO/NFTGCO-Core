using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCOSHOP
{
    public class ItemBaseSO : ScriptableObject
    {
        [Header("Base data")] [SerializeField] private string _itemId;
        [SerializeField] private string _itemName;
        [TextArea(3, 10)] [SerializeField] private string _itemDescription;

        public string ItemId => _itemId;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
    }
}