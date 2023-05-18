using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCOShop;
using NFTGCO.Inventory;

namespace NFTGCO
{
    public class DataRecieverManager : NFTGCO.Helpers.Singleton<DataRecieverManager>
    {
        [Header("Data")]
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameInventoryData _gameInventoryData;
        [SerializeField] private GamesShopData _gamesShopData;
        [SerializeField] private GameShopData _gameShopData;

        [Space]
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTGCO.Core.Global.NFTGCOGamesId _gameId;
        public NFTGCO.Core.Global.NFTGCOGamesId GameID => _gameId;

        public PlayerData PlayerData => _playerData;
        public GameInventoryData GameInventoryData => _gameInventoryData;
        public GamesShopData GamesShopData => _gamesShopData;
        public GameShopData GameShopData => _gameShopData;

        private void Start()
        {
            _playerData.LoadData();
            _gameInventoryData.LoadData();
            _gamesShopData.LoadData();
            _gameShopData.LoadData();
        }
    }
}