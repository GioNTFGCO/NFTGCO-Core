using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCOSHOP
{
    [CreateAssetMenu(fileName = "ItemConsumable", menuName = "NFTGCGAME/ItemConsumable", order = 0)]
    public class ItemConsumableSO : ItemGameSO
    {
        private ItemTypeEnum _itemType = ItemTypeEnum.Non_Unique;

        public ItemTypeEnum ItemType => _itemType;
    }
}