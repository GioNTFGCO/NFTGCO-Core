using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTGCO
{
    public class NFTGCOLoggedSessionUi : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _logOutButton;

        public void Init(System.Action OnStartGame, System.Action OnLogOut)
        {
            _startGameButton.onClick.AddListener(() =>
            {
                OnStartGame?.Invoke();
            });

            _logOutButton.onClick.AddListener(() =>
            {
                OnLogOut?.Invoke();
            });
        }
    }
}