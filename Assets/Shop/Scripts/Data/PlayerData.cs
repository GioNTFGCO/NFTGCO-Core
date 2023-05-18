using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace NFTGCOShop
{
    [System.Serializable]
    public class PlayerData
    {
        public string player_Id;
        public int player_xp;
        //public NFTGCO.Helpers.UDictionary<string, NFTGCO.Inventory.ItemInInventory> itemsInInventory = new NFTGCO.Helpers.UDictionary<string, NFTGCO.Inventory.ItemInInventory>();
    }
}