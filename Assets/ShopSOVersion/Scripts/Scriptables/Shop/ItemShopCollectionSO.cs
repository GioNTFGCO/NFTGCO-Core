using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCOSHOP
{
    [CreateAssetMenu(fileName = "ItemShopCollection", menuName = "NFTGCOSHOP/ItemShopCollection", order = 0)]
    public class ItemShopCollectionSO : ScriptableObject
    {
        [SerializeField] private List<ItemShopSO> _itemShopScriptableObjects = new List<ItemShopSO>();

        public List<ItemShopSO> ItemShopScriptableObjects => _itemShopScriptableObjects;
    }
}