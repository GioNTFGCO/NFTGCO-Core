using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCO
{
    [System.Serializable]
    public class GameInventoryDTO
    {
        public NFTGCO.Core.Global.NFTGCOGamesId game_Id;
        public NFTGCO.Helpers.UDictionary<string, NFTGCO.Inventory.ItemInInventory> itemsInInventory = new NFTGCO.Helpers.UDictionary<string, NFTGCO.Inventory.ItemInInventory>();

        public GameInventoryDTO(NFTGCO.Core.Global.NFTGCOGamesId gameId)
        {
            game_Id = gameId;
        }
    }
}