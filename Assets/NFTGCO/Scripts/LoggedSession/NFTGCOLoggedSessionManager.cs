using System.Collections;
using System.Collections.Generic;
using NFTGCO.API;
using SceneField.Core;
using UnityEngine;
using NFTGCO;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class NFTGCOLoggedSessionManager : MonoBehaviour
    {
        [SerializeField] private NFTGCOLoggedSessionUi nftgcoLoggedSessionUi;
        [SerializeField] private NFTGCOManagerUi nftgcoManagerUi;
        [SerializeField] private NFTGCOLoginUi nftgcoLoginUi;
        [SerializeField] private SceneLoaderController _sceneLoader;

        private void Start()
        {
            nftgcoLoggedSessionUi.Init(() => StartGame(), () => LogOut());
        }

        public void StartGame()
        {
            if (_sceneLoader == null)
                return;

            _sceneLoader.StartLevel();
        }

        public void LogOut()
        {
            PlayerPrefs.DeleteKey(NFTGCOConfig.ConfigAccessToken);
            PlayerPrefs.DeleteKey(NFTGCOConfig.ConfigRefreshToken);

            NFTGCOStoredManager.Instance.ClearData();

            nftgcoManagerUi.ShowPanel("Initial");

            nftgcoLoginUi.LoginButtonBehaviour(true);

            //Logout Google
#if UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("Calling SignOut");
            Google.GoogleSignIn.DefaultInstance.SignOut();
#endif
        }
    }
}