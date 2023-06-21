using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using TMPro;
using UnityEngine;

namespace NFTGCO
{
    public class GameVersion : Singleton<GameVersion>
    {
        [SerializeField] private TextMeshProUGUI _versionText;

        private void Start()
        {
            if (_versionText)
            {
                _versionText.text = $"Version {Application.version}";
            }
            else
            {
                _versionText = GetComponentInChildren<TextMeshProUGUI>();
                _versionText.text = $"Version {Application.version}";
            }
        }
    }
}