using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTGCOShop
{
    public class ShopModalItemUiController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _mainCanvasGroup;
        [SerializeField] private Button _modalCloseButton;
        [SerializeField] private TextMeshProUGUI _itemNameText;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemDescriptionText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _buyButtonText;
    }
}