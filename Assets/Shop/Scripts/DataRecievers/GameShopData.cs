using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;
namespace NFTGCOShop
{
    public class GameShopData : JsonSaveLoad<GameShop>, IDataLoader
    {
        [Space]
        [SerializeField] private GameShop _gameShopData;
        public GameShop ServerData => _gameShopData;

        protected override void Awake()
        {
            base.Awake();

            int upperCaseIndex = _filePath.LastIndexOf('_');
            //_filePath = _filePath.Insert(upperCaseIndex + 1, $"{_gameShopData.game_Id}");
            _filePath = _filePath.Insert(upperCaseIndex + 1, $"{NFTGCO.DataRecieverManager.Instance.GameID}");
        }
        public void LoadData()
        {
            _gameShopData = Load();
            if (_gameShopData != null)
            {
                Debug.Log($"Data from {this.GetType().Name} was loaded successfully.");
            }
            else
            {
                _gameShopData = new GameShop(NFTGCO.DataRecieverManager.Instance.GameID);
                SaveData();
            }
        }
        public void SaveData()
        {
            Save(_gameShopData);
        }
        public void ClearData()
        {
            base.ClearFile();
        }
    }
}