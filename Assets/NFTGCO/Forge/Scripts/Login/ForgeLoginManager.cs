using System;
using System.Collections;
using System.Collections.Generic;
using Forge.API;
using NFTGCO;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using SceneField.Core;
using UnityEngine;

namespace Forge
{
    public class ForgeLoginManager : MonoBehaviour
    {
        [SerializeField] private ForgeLoginServer _forgeLoginServer;
        [SerializeField] private ForgeLoginUi _forgeLoginUi;
        [SerializeField] private ForgeManagerUi _forgeManagerUi;
        [SerializeField] private UpdateAccountManager _updateAccountManager;
        [SerializeField] private ForgeLoggedSessionManager _forgeLoggedSessionManager;

        [Space] [Header("Test only")] [SerializeField]
        private string _testUserName;

        [SerializeField] private string _testUserPassword;
        [SerializeField] private string _manualToken;

        [Space] [SerializeField] private NFTGCO.Helpers.InspectorButton LoginWithManualTokenButton =
            new NFTGCO.Helpers.InspectorButton("LoginWithManualToken");

        private void OnEnable()
        {
            _forgeLoginServer.OnAuthFinish += OnLoginSuccess;
        }

        private void OnDisable()
        {
            _forgeLoginServer.OnAuthFinish -= OnLoginSuccess;
        }

        void Start()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            _forgeLoginUi.TestDataFields(_testUserName, _testUserPassword);
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
            _forgeLoginServer.LoginWithToken("");
#endif

            _forgeLoginUi.Init(() => AuthWithUserData(), () => ForgetPassword());

            #region Auto Login

            if (!string.IsNullOrEmpty(Config.Instance.RefreshToken))
            {
                _forgeManagerUi.ShowHideBlockPanel(true);
                _forgeLoginServer.RefreshToken();
            }

            #endregion
        }

        public void AuthWithUserData()
        {
            AccountAPI.AuthRequest(_forgeLoginUi.GetUserName(), _forgeLoginUi.GetUserPassword(),
                _forgeLoginServer.AuthWithCredentialsCallback);
        }

        public void LoginWithToken(string token)
        {
            _forgeLoginServer.LoginWithToken(token);
        }

        private void ForgetPassword()
        {
            AccountAPI.ForgetPasswordRequest(_forgeLoginUi.GetForgetPasswordEmail(), ForgetPasswordCallback);
        }

        private void ForgetPasswordCallback(RequestException exception, string response)
        {
            if (exception == null) //success
            {
                Debug.Log("The mail has been sent. Please verify your inbox or Junk emails.");
                _forgeLoginUi.EnableEmailSentPanel();
            }

            if (exception != null)
            {
                if (exception.IsHttpError)
                {
                    UiMessage.OnMessageSent?.Invoke("Email doesn't exist!");
                }
            }
        }

        private void LoginWithSaveData()
        {
            _forgeLoginServer.RefreshToken();
        }

        private void OnLoginSuccess()
        {
            _forgeManagerUi.ShowHideBlockPanel(false);

            _forgeLoginUi.LoginButtonBehaviour(false);

            if (ForgeStoredSettings.Instance.AccountDTOResponse != null)
            {
                UiMessage.OnMessageSent?.Invoke("Auth success");
                //show the logged session panel
                //_forgeManagerUi.ShowPanel("LoggedSession");
                //_forgeLoggedSessionManager.StartGame();
                
                if (!_updateAccountManager.CheckFirstSocialLogin())
                {
                    _forgeManagerUi.ShowPanel("LoggedSession");
                    //start game automatically
                    //_forgeLoggedSessionManager.StartGame();
                }
                //show the nickname panel
                else
                {
                    _updateAccountManager.OpenUpdateNicknamePanel();
                }
            }
            else
            {
                UiMessage.OnMessageSent?.Invoke("Auth error, reload the game with correct token");
            }
        }

        #region Test only
        private void LoginWithManualToken()
        {
            _forgeLoginServer.LoginWithToken(_manualToken);
        }
        #endregion
    }
}