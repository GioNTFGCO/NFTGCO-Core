using System;
using UnityEngine;
using NFTGCO.Helpers;

namespace NFTGCO
{
    public class GameServerLoadingScreen: MonoBehaviour
    {
        public static Action OnShowLoadingScreen;
        public static Action OnHideLoadingScreen;

        [SerializeField] private CanvasGroup _canvasGroup;
        private Coroutine _coroutine;
        string _baseText = "Loading";
        private int _maxDots = 3;
        private float _delay = 0.1f;

        private void OnEnable()
        {
            OnShowLoadingScreen += ShowLoadingScreen;
            OnHideLoadingScreen += HideLoadingScreen;
        }

        private void OnDisable()
        {
            OnShowLoadingScreen -= ShowLoadingScreen;
            OnHideLoadingScreen -= HideLoadingScreen;
        }

        // Start is called before the first frame update
        void Start()
        {
            HideLoadingScreen();
        }

        private void ShowLoadingScreen()
        {
            NFTGCOHelpers.CanvasGroupBehaviour(_canvasGroup, true);
        }

        private void HideLoadingScreen()
        {
            NFTGCOHelpers.CanvasGroupBehaviour(_canvasGroup, false);
        }
    }
}