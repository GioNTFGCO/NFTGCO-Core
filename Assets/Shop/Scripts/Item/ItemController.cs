using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCOShop
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] private ItemControllerUi _itemControllerUi;

        private ItemShop _itemShop;

        public void Init(ItemShop itemShop, Sprite itemSprite, System.Action OnOpenModal, System.Action OnBuy, bool canBuy)
        {
            _itemShop = itemShop;

            OnBuy += () => OnLockItem();

            _itemControllerUi.Init(itemShop.itemName, itemSprite, OnOpenModal, OnBuy, $"${itemShop.itemCost}", canBuy);
        }
        private void OnLockItem()
        {
            if (_itemShop.itemType == ItemTypeEnum.Unique)
            {
                _itemControllerUi.ItemBuyButton.interactable = false;
            }
        }
    }
}