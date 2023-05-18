using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO;
using System.Linq;

namespace NFTGCOShop
{
    public class ShopBuyItemController : MonoBehaviour
    {
        public void BuyItem(System.Action OnRefreshShopMoney, ShopTypeEnum typeOfShop, ItemShop itemShop)
        {
            if (DataRecieverManager.Instance.PlayerData.ServerData.player_xp > itemShop.itemCost)
            {
                //string itemInInventoryID = $"{typeOfShop}_{itemShop.item_Id}";
                if (typeOfShop == ShopTypeEnum.General_games)
                {
                    GameMoneyController.OnAddMoney?.Invoke(itemShop.itemCost);
                    Debug.Log($"Player buy {itemShop.itemName}, with a cost of {itemShop.itemCost} gaxos coins and add {itemShop.itemCost} coins to player.");
                   
                    DataRecieverManager.Instance.PlayerData.SaveData();
                    // //add item to player inventory
                    // PlayerInventoryManager.OnAddItemToPlayerInventory?.Invoke(itemShop.item_Id, itemShop, delegate
                    // {
                    //     Debug.Log($"Player buy {itemShop.item_name}, with a cost of {itemShop.item_cost} gaxos coins.");
                    //     GameMoneyController.OnDiscountMoney?.Invoke(itemShop.item_cost);
                    //     DataRecieverManager.Instance.PlayerData.SaveData();
                    // });
                }
                else
                if (typeOfShop == ShopTypeEnum.Own_game)
                {
                    //add item to player game inventory
                    string itemId = "";
                    string[] splitItemId = itemShop.itemId.Split('-');

                    itemId = splitItemId.Length > 1 ? splitItemId.First() : itemShop.itemId;

                    PlayerInventoryManager.OnAddItemToGameInventory?.Invoke(itemId, itemShop, delegate
                    {
                        Debug.Log($"Player buy {itemShop.itemName}, with a cost of {itemShop.itemCost} gaxos coins.");
                        
                        GameMoneyController.OnDiscountMoney?.Invoke(itemShop.itemCost);
                        
                        DataRecieverManager.Instance.GameInventoryData.SaveData();
                    });
                }

                OnRefreshShopMoney?.Invoke();
            }
        }
    }
}