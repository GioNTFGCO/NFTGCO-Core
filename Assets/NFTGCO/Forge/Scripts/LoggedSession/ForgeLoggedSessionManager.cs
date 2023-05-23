using System.Collections;
using System.Collections.Generic;
using Forge.API;
using SceneField.Core;
using UnityEngine;
using NFTGCO;

namespace Forge
{
    public class ForgeLoggedSessionManager : MonoBehaviour
    {
        [SerializeField] private ForgeLoggedSessionUi _forgeLoggedSessionUi;
        [SerializeField] private ForgeManagerUi _forgeManagerUi;
        [SerializeField] private ForgeLoginUi _forgeLoginUi;
        [SerializeField] private SceneLoaderController _sceneLoader;

        private void Start()
        {
            _forgeLoggedSessionUi.Init(() => StartGame(), () => LogOut());
        }

        public void StartGame()
        {
            if (_sceneLoader == null)
                return;

            _sceneLoader.StartLevel();
        }
        public void LogOut()
        {
            PlayerPrefs.DeleteKey(Config.ACCESS_TOKEN);
            PlayerPrefs.DeleteKey(Config.REFRESH_TOKEN);

            ForgeStoredSettings.Instance.ClearData();

            _forgeManagerUi.ShowPanel("Initial");

            _forgeLoginUi.LoginButtonBehaviour(true);

            //Logout Google
#if UNITY_ANDROID && !UNITY_EDITOR
            Debug.Log("Calling SignOut");
            Google.GoogleSignIn.DefaultInstance.SignOut();
#endif
        }
    }
}