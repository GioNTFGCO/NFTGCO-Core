using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCOShop
{
    [System.Serializable]
    public class GameShop
    {
        public NFTGCO.Core.Global.NFTGCOGamesId gameId;
        public List<ItemShop> itemsGameShop = new List<ItemShop>();
        public GameShop(NFTGCO.Core.Global.NFTGCOGamesId gameId)
        {
            this.gameId = gameId;
        }
    }
}