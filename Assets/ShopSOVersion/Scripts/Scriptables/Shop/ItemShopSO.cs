using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;

namespace NFTGCOSHOP
{
    [CreateAssetMenu(fileName = "ItemShop", menuName = "NFTGCOSHOP/ItemShop", order = 0)]
    public class ItemShopSO : ItemBaseSO
    {
        [Header("Values")]
        [SerializeField] private int _cost;
        [SerializeField] private int _quantity;
        [Header("Types")]
        [SerializeField] private ItemRarityEnum _itemRarityEnum;
        [SerializeField] private ShopTypeEnum _shopTypeEnum;
        [SerializeField] private ItemTimeResetEnum _itemTimeResetEnum;
        
        public int Cost => _cost;
        public int Quantity => _quantity;
        
        public ItemRarityEnum ItemRarityEnum => _itemRarityEnum;
        public ShopTypeEnum ShopTypeEnum => _shopTypeEnum;
        public ItemTimeResetEnum ItemTimeResetEnum => _itemTimeResetEnum;
    }
}