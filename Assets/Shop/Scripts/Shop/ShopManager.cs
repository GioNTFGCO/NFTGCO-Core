using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Helpers;
using NFTGCO;
using System;
using System.Linq;

namespace NFTGCOShop
{
    public class ShopManager : MonoBehaviour
    {
        public System.Action OnRefreshShopMoney;

        [Header("References")]
        [SerializeField] private ShopManagerUi _shopManagerUi;
        [SerializeField] private ShopBuyItemController _buyItemController;
        [SerializeField] private ShopResources _shopResources;
        [Header("Runtime")]
        [SerializeField] private ShopTypeEnum _initialShop;
        [SerializeField] private ItemController _itemControllerPrefab;

        private UDictionary<ShopTypeEnum, List<ItemShop>> _copiesShopList;

        private void OnEnable()
        {
            OnRefreshShopMoney += RefreshShopMoney;
        }
        private void OnDisable()
        {
            OnRefreshShopMoney -= RefreshShopMoney;
        }
        private void Start()
        {
            this.DoAfter(() => DataRecieverManager.Instance.PlayerData != null && DataRecieverManager.Instance.GamesShopData != null && DataRecieverManager.Instance.GameShopData != null,
            delegate
            {
                _shopManagerUi.EnableShop(_initialShop);

                CreateItemsCopyFromServer();

                CreateItems();
            });

            Debug.Log(NFTGCOHelpers.ParseStringToDateTime("Item4-5/10/2023"));

            Debug.Log(NFTGCOHelpers.IsSameWeek(DateTime.Now, NFTGCOHelpers.ParseStringToDateTime("Item4-5/10/2023")));
            Debug.Log(NFTGCOHelpers.IsSameWeek(DateTime.Now, DateTime.Now.AddDays(7)));
        }



        private void CreateItemsCopyFromServer()
        {
            _copiesShopList = new UDictionary<ShopTypeEnum, List<ItemShop>>();

            List<ItemShop> CopyGamesShopList = new List<ItemShop>();
            foreach (var item in DataRecieverManager.Instance.GamesShopData.ServerData.items_global_shop)
            {
                ItemShop copy = item.DeepClone<ItemShop>();

                #region Logic for time reset items
                // switch (copy.item_time_reset)
                // {
                //     case ItemTimeResetEnum.None:
                //         break;
                //     case ItemTimeResetEnum.Daily:
                //         copy.item_Id = $"{copy.item_Id}-{System.DateTime.Now.ToShortDateString()}";
                //         break;
                //     case ItemTimeResetEnum.Weekly:
                //         copy.item_Id = $"{copy.item_Id}-{System.DateTime.Now.ToShortDateString()}";
                //         break;
                //     case ItemTimeResetEnum.Monthly:
                //         copy.item_Id = $"{copy.item_Id}-{System.DateTime.Now.ToShortDateString()}";
                //         break;
                // }

                // if (copy.item_time_reset != ItemTimeResetEnum.None)
                // {
                //     copy.item_Id = $"{copy.item_Id}-{System.DateTime.Now.ToShortDateString()}";
                // }
                #endregion

                CopyGamesShopList.Add(copy);
                #region Types to copy classes
                // ItemShop copy = new ItemShop(item.item_Id, item.item_name, item.item_description, item.item_gameId, item.item_cost, item.is_stackeable, item.buy_once_a_day);
                // CopyGamesShopList.Add(new ItemShop
                // {
                //     item_Id = item.item_Id,
                //     item_name = item.item_name,
                //     item_description = item.item_description,
                //     item_gameId = item.item_gameId,
                //     item_cost = item.item_cost,
                //     is_stackeable = item.is_stackeable,
                //     buy_once_a_day = item.buy_once_a_day
                // });
                #endregion
            }

            _copiesShopList.TryAdd(ShopTypeEnum.General_games, CopyGamesShopList);

            List<ItemShop> CopyGameShopList = new List<ItemShop>();
            foreach (var item in DataRecieverManager.Instance.GameShopData.ServerData.itemsGameShop)
            {
                ItemShop copy = item.DeepClone<ItemShop>();

                // if (copy.item_time_reset != ItemTimeResetEnum.None)
                // {
                //     copy.item_Id = $"{copy.item_Id}-{System.DateTime.Now.ToShortDateString()}";
                // }
                CopyGameShopList.Add(copy);
            }

            _copiesShopList.TryAdd(ShopTypeEnum.Own_game, CopyGameShopList);
        }
        private void CreateItems()
        {
            for (int i = 0; i < _copiesShopList.Keys.Count; i++)
            {
                ShopTypeEnum typeOfShop = (ShopTypeEnum)i;

                for (int x = 0; x < _copiesShopList[typeOfShop].Count; x++)
                {
                    bool canBuyItem = true;

                    ItemShop itemShop = _copiesShopList[typeOfShop][x];
                    Debug.Log($"Creating Item {itemShop.itemId} from {typeOfShop} Data.");

                    ItemController itemFromServer = Instantiate(_itemControllerPrefab);
                    itemFromServer.transform.SetParent(_shopManagerUi.ShopTabs[typeOfShop].tabContent, false);

                    if (itemShop.itemType == ItemTypeEnum.Unique)
                    {
                        if (DataRecieverManager.Instance.GameInventoryData.ServerData.itemsInInventory.ContainsKey(itemShop.itemId)) /*|| DataRecieverManager.Instance.PlayerData.ServerData.itemsInInventory.ContainsKey(itemShop.item_Id)*/
                        {
                            Debug.Log($"{itemShop.itemId} Already exist in Player Inventory.");
                            canBuyItem = false;
                        }
                    }

                    #region Logic for time reset items
                    // if (itemShop.item_time_reset != ItemTimeResetEnum.None)
                    // {
                    //     if (DataRecieverManager.Instance.GameInventoryData.ServerData.itemsInInventory.ContainsKey(itemShop.item_Id)) /*|| DataRecieverManager.Instance.PlayerData.ServerData.itemsInInventory.ContainsKey(itemShop.item_Id)*/
                    //     {
                    //         Debug.Log($"{itemShop.item_Id} Already exist in Player Inventory.");
                    //         canBuyItem = false;
                    //     }
                    // }
                    // switch (itemShop.item_time_reset)
                    // {
                    //     case ItemTimeResetEnum.None:
                    //         break;
                    //     case ItemTimeResetEnum.Daily:
                    //         if (DataRecieverManager.Instance.GameInventoryData.ServerData.itemsInInventory.ContainsKey(itemShop.item_Id))
                    //         {
                    //             Debug.Log($"{itemShop.item_Id} Already exist in Player Inventory.");
                    //             canBuyItem = false;
                    //         }
                    //         break;
                    //     case ItemTimeResetEnum.Weekly:

                    //         break;
                    //     case ItemTimeResetEnum.Monthly:
                    //         break;
                    // }
                    #endregion

                    itemFromServer.Init(itemShop, _shopResources.ItemsIcons[0], null,
                    delegate
                    {
                        _buyItemController.BuyItem(() => RefreshShopMoney(), typeOfShop, itemShop);
                    }, canBuyItem);
                }
            }
        }
        private void RefreshShopMoney()
        {
            _shopManagerUi.RefreshMoneyUi($"{DataRecieverManager.Instance.PlayerData.ServerData.player_xp}");
        }
    }
}