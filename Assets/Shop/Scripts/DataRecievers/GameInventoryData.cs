using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using NFTGCOShop;
using UnityEngine;
namespace NFTGCO.Inventory
{
    public class GameInventoryData : JsonSaveLoad<GameInventoryDTO>, IDataLoader
    {
        [Space]
        [SerializeField] private GameInventoryDTO _gameInventory;
        public GameInventoryDTO ServerData => _gameInventory;
        protected override void Awake()
        {
            base.Awake();

            int upperCaseIndex = _filePath.LastIndexOf('_');
            //_filePath = _filePath.Insert(upperCaseIndex + 1, $"{_gameShopData.game_Id}");
            _filePath = _filePath.Insert(upperCaseIndex + 1, $"{NFTGCO.DataRecieverManager.Instance.GameID}");
        }

        public void LoadData()
        {
            _gameInventory = Load();
            if (_gameInventory != null)
            {
                Debug.Log($"Data from {this.GetType().Name} was loaded successfully.");
            }
            else
            {
                _gameInventory = new GameInventoryDTO(NFTGCO.DataRecieverManager.Instance.GameID);
                SaveData();
            }
        }
        public void SaveData()
        {
            Save(_gameInventory);
        }
        public void ClearData()
        {
            base.ClearFile();
        }
    }
}