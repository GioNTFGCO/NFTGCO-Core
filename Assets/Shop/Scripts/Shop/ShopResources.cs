using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCOShop
{
    public class ShopResources : MonoBehaviour
    {
        private Sprite[] _itemsIcons;

        public Sprite[] ItemsIcons => _itemsIcons;

        private void Awake()
        {
            LoadIcons();
        }

        private void LoadIcons()
        {
            object[] loadedIcons = Resources.LoadAll("ShopItems", typeof(Sprite));
            _itemsIcons = new Sprite[loadedIcons.Length];

            loadedIcons.CopyTo(_itemsIcons, 0);
        }
    }
}