using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;
namespace NFTGCOShop
{
    public class GamesShopData : JsonSaveLoad<GamesShop>, IDataLoader
    {
        [Space]
        [SerializeField] private GamesShop _gamesShopData;
        public GamesShop ServerData => _gamesShopData;

        protected override void Awake()
        {
            base.Awake();

            int upperCaseIndex = _filePath.LastIndexOf('_');
            _filePath = _filePath.Remove(upperCaseIndex, 1);
        }

        public void LoadData()
        {
            _gamesShopData = Load();
            if (_gamesShopData != null)
            {
                Debug.Log($"Data from {this.GetType().Name} was loaded successfully.");
            }
            else
            {
                _gamesShopData = new GamesShop();
            }
        }
        public void SaveData()
        {
            Save(_gamesShopData);
        }
        public void ClearData()
        {
            base.ClearFile();
        }
    }
}