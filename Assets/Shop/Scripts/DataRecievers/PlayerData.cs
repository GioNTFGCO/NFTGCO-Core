using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCOShop;
using NFTGCO.Helpers;

namespace NFTGCO
{
    public class PlayerData : JsonSaveLoad<NFTGCOShop.PlayerData>, IDataLoader
    {
        [Space]
        [SerializeField] private NFTGCOShop.PlayerData _playerData;
        public NFTGCOShop.PlayerData ServerData => _playerData;

        protected override void Awake()
        {
            base.Awake();

            int upperCaseIndex = _filePath.LastIndexOf('_');
            _filePath = _filePath.Remove(upperCaseIndex, 1);
        }

        public void LoadData()
        {
            _playerData = Load();
            if (_playerData != null)
            {
                Debug.Log($"Data from {this.GetType().Name} was loaded successfully.");
            }
            else
            {
                _playerData = new NFTGCOShop.PlayerData();
            }
        }
        public void SaveData()
        {
            Save(_playerData);
        }
        public void ClearData()
        {
            base.ClearFile();
        }
    }
}