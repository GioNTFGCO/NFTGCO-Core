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

        [Space]
        [Header("Test only")]
        [SerializeField] private string _testUserName;
        [SerializeField] private string _testUserPassword;
        [SerializeField] private string _manualToken;

        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton LoginWithManualTokenButton = new NFTGCO.Helpers.InspectorButton("LoginWithManualToken");

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
            AuthApi.AuthRequest(_forgeLoginUi.GetUserName(), _forgeLoginUi.GetUserPassword(), _forgeLoginServer.AuthWithCredentialsCallback);
        }
        public void LoginWithToken(string token)
        {
            _forgeLoginServer.LoginWithToken(token);
        }

        private void ForgetPassword()
        {
            AuthApi.ForgetPasswordRequest(_forgeLoginUi.GetForgetPasswordEmail(), ForgetPasswordCallback);
        }
        private void ForgetPasswordCallback(RequestException exception, string response)
        {
            if (exception == null)//success
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
                _forgeManagerUi.ShowPanel("LoggedSession");

                //check if first time (check the nickname), if the nickname is empty, then it is the first time
                //then show the nickname panel
                _updateAccountManager.CheckFirstSocialLogin();
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