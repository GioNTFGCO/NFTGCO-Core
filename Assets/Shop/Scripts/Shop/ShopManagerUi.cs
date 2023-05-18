using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NFTGCO.Helpers;

namespace NFTGCOShop
{
    [System.Serializable]
    public class ShopTabButtons
    {
        public Button tabButton;
        public CanvasGroup tabCanvasGroup;
        public Transform tabContent;
    }
    public class ShopManagerUi : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UDictionary<ShopTypeEnum, ShopTabButtons> _shopTabs;
        [SerializeField] private TextMeshProUGUI _moneyText;

        public UDictionary<ShopTypeEnum, ShopTabButtons> ShopTabs => _shopTabs;

        private void Start()
        {
            RegisterCallbacks();
        }

        public void EnableShop(ShopTypeEnum initialPanelKey)
        {
            DisableAllPanels();
            EnableShopPanel(initialPanelKey);
        }
        private void RegisterCallbacks()
        {
            foreach (var item in _shopTabs)
            {
                item.Value.tabButton.onClick.AddListener(delegate
                {
                    EnableShop(item.Key);
                });
            }
        }
        private void DisableAllPanels()
        {
            foreach (var item in _shopTabs)
            {
                CanvasGroupState(item.Value.tabCanvasGroup, false);
            }
        }
        private void EnableShopPanel(ShopTypeEnum panelKey)
        {
            CanvasGroupState(_shopTabs[panelKey].tabCanvasGroup, true);
        }
        private void CanvasGroupState(CanvasGroup canvasGroup, bool newState)
        {
            canvasGroup.alpha = newState ? 1 : 0;
            canvasGroup.interactable = newState;
            canvasGroup.blocksRaycasts = newState;
        }
        public void RefreshMoneyUi(string newValue)
        {
            _moneyText.text = newValue;
        }
    }
}