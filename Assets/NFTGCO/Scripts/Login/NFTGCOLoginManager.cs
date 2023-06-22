using System;
using System.Collections;
using System.Collections.Generic;
using NFTGCO.API;
using NFTGCO;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using SceneField.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class NFTGCOLoginManager : MonoBehaviour
    {
        [SerializeField] private NFTGCOLoginServer nftgcoLoginServer;
        [SerializeField] private NFTGCOLoginNFT nftgcoLoginNft;
        [SerializeField] private NFTGCOLoginUi nftgcoLoginUi;
        [SerializeField] private NFTGCOManagerUi nftgcoManagerUi;
        [SerializeField] private NFTGCOLoggedSessionManager nftgcoLoggedSessionManager;

        [Space] [Header("Test only")] [SerializeField]
        private string _testUserName;

        [SerializeField] private string _testUserPassword;

        private void OnEnable()
        {
            nftgcoLoginNft.OnNFTGCODataReceived += OnLoginSuccess;
        }

        private void OnDisable()
        {
            nftgcoLoginNft.OnNFTGCODataReceived -= OnLoginSuccess;
        }

        void Start()
        {
#if UNITY_EDITOR || UNITY_ANDROID
            nftgcoLoginUi.TestDataFields(_testUserName, _testUserPassword);
#endif
#if !UNITY_EDITOR && UNITY_WEBGL
            _forgeLoginServer.LoginWithToken("");
#endif

            nftgcoLoginUi.Init(() => AuthWithUserData(), () => ForgetPassword());

            #region Auto Login

            if (!string.IsNullOrEmpty(NFTGCOConfig.Instance.RefreshToken))
            {
                nftgcoManagerUi.ShowHideBlockPanel(true);
                nftgcoLoginServer.RefreshToken();
            }

            #endregion
        }

        public void AuthWithUserData()
        {
            AccountAPI.AuthRequest(nftgcoLoginUi.GetUserName(), nftgcoLoginUi.GetUserPassword(),
                nftgcoLoginServer.AuthWithCredentialsCallback);

            // Set login type to user_pass
            NFTGCOConfig.Instance.SetLoginType("user_pass");
        }

        public void LoginWithToken(string token, string refreshToken)
        {
            nftgcoLoginServer.LoginWithToken(token, refreshToken);
        }

        private void ForgetPassword()
        {
            AccountAPI.ForgetPasswordRequest(nftgcoLoginUi.GetForgetPasswordEmail(), ForgetPasswordCallback);
        }

        private void ForgetPasswordCallback(RequestException exception, ResponseHelper response)
        {
            if (exception == null) //success
            {
                Debug.Log("The mail has been sent. Please verify your inbox or Junk emails.");
                nftgcoLoginUi.EnableEmailSentPanel();
            }

            if (exception != null)
            {
                if (exception.IsHttpError)
                {
                    UiMessage.OnMessageSent?.Invoke("Email doesn't exist!");
                }
            }
        }

        private void OnLoginSuccess()
        {
            //_forgeManagerUi.ShowHideBlockPanel(false);
            nftgcoLoginUi.LoginButtonBehaviour(false);

            if (NFTGCOStoredManager.Instance.AccountDTOResponse != null)
            {
                UiMessage.OnMessageSent?.Invoke("NFTGCO data success!");

                nftgcoManagerUi.ShowHideBlockPanel(false);
                //_forgeManagerUi.ShowPanel("LoggedSession");
                //start game automatically
                nftgcoLoggedSessionManager.StartGame();
            }
            else
            {
                UiMessage.OnMessageSent?.Invoke("Auth error, reload the game with correct token");
            }
        }
    }
}