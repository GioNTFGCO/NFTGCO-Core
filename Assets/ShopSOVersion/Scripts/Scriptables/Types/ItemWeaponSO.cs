using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCOSHOP
{
    [CreateAssetMenu(fileName = "ItemWeapon", menuName = "NFTGCGAME/ItemWeapon", order = 0)]
    public class ItemWeaponSO : ItemGameSO
    {
        private ItemTypeEnum _itemType = ItemTypeEnum.Unique;
        private ItemUniqueTypeEnum _itemUniqueTypeEnum = NFTGCOSHOP.ItemUniqueTypeEnum.Weapon;

        public ItemTypeEnum ItemType => _itemType;
        public ItemUniqueTypeEnum ItemUniqueTypeEnum => _itemUniqueTypeEnum;
    }
}