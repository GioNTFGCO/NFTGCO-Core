using System.Collections;
using System.Collections.Generic;
using NFTGCOShop;
using UnityEngine;
namespace NFTGCO
{
    public class GameMoneyController : MonoBehaviour
    {
        public static System.Action<int> OnAddMoney;
        public static System.Action<int> OnDiscountMoney;

        private void OnEnable()
        {
            OnAddMoney += AddMoney;
            OnDiscountMoney += DiscountMoney;
        }
        private void OnDisable()
        {
            OnAddMoney -= AddMoney;
            OnDiscountMoney -= DiscountMoney;
        }

        //Create another script to handle money
        private void AddMoney(int amount)
        {
            DataRecieverManager.Instance.PlayerData.ServerData.player_xp += amount;
        }
        private void DiscountMoney(int amount)
        {
            DataRecieverManager.Instance.PlayerData.ServerData.player_xp -= amount;
        }
    }
}