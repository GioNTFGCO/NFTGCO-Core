using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTGCOShop
{
    public class ItemControllerUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Button _itemModalButton;
        [SerializeField] private Button _itemBuyButton;
        [SerializeField] private TextMeshProUGUI _itemPrice;

        public Button ItemBuyButton => _itemBuyButton;

        public void Init(string itemName, Sprite itemSprite, System.Action OnOpenModal, System.Action OnBuy, string itemPrice, bool canBuy)
        {
            _itemName.text = itemName;
            _itemImage.sprite = itemSprite;
            _itemModalButton.onClick.AddListener(() => OnOpenModal?.Invoke());
            _itemBuyButton.onClick.AddListener(() => OnBuy?.Invoke());
            _itemPrice.text = itemPrice;

            if (!canBuy)
            {
                _itemBuyButton.interactable = false;
            }
        }
    }
}