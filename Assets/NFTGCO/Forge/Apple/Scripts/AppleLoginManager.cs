using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Extensions;
using AppleAuth.Interfaces;
using AppleAuth.Native;
using Forge;
using Forge.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;

namespace NFTGCO
{
    public class AppleLoginManager : MonoBehaviour
    {
        [SerializeField] private ForgeLoginManager _forgeLoginManager;
        [SerializeField] private ForgeManagerUi _forgeManagerUi;

        private IAppleAuthManager _appleAuthManager;
        private IAppleIDCredential _appleIdCredential;
        private string _appleUserToken;

        [Space] [Header("Test")] [SerializeField]
        private string _manualAppleToken;

        [SerializeField] private NFTGCO.Helpers.InspectorButton LoginWithManualTokenButton =
            new NFTGCO.Helpers.InspectorButton("LoginWithManualToken");

        void Start()
        {
            // If the current platform is supported
            if (AppleAuthManager.IsCurrentPlatformSupported)
            {
                // Creates a default JSON deserializer, to transform JSON Native responses to C# instances
                var deserializer = new PayloadDeserializer();
                // Creates an Apple Authentication manager with the deserializer
                _appleAuthManager = new AppleAuthManager(deserializer);
            }

            _forgeManagerUi.DelegateButtonLoginCallback("IOS", () =>
            {
                //lock panel
                _forgeManagerUi.ShowHideBlockPanel(true);
                //start login with Apple
                SignInWithApple();
            });


#if UNITY_IOS && !UNITY_EDITOR
            if (string.IsNullOrEmpty(Config.Instance.AccessToken))
                SignInWithApple();
#endif
        }

        // Update is called once per frame
        void Update()
        {
            // Updates the AppleAuthManager instance to execute
            // pending callbacks inside Unity's execution loop
            if (_appleAuthManager != null)
            {
                _appleAuthManager.Update();
            }
        }

        private void SignInWithApple()
        {
            if (_appleAuthManager == null)
            {
                Debug.LogError("Unsupported platform");
                return;
            }

            var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail | LoginOptions.IncludeFullName);

            this._appleAuthManager.LoginWithAppleId(
                loginArgs,
                credential =>
                {
                    // If a sign in with apple succeeds, we should have obtained the credential with the user id, name, and email, save it
                    _appleIdCredential = credential as IAppleIDCredential;
                    _appleUserToken = Encoding.UTF8.GetString(_appleIdCredential.IdentityToken, 0,
                        _appleIdCredential.IdentityToken.Length);

                    LoginWithApple(_appleUserToken);
                },
                error =>
                {
                    var authorizationErrorCode = error.GetAuthorizationErrorCode();
                    Debug.LogWarning("Sign in with Apple failed " + authorizationErrorCode.ToString() + " " +
                                     error.ToString());
                    _forgeManagerUi.ShowHideBlockPanel(false);
                });
        }

        #region NFTGCO

        private void LoginWithApple(string appleUserToken)
        {
            Debug.Log($"Sign in with Apple succeeded {appleUserToken}");
            AccountAPI.AuthAppleRequest(appleUserToken, AuthAppleCallback);
        }

        private void AuthAppleCallback(RequestException exception, ResponseHelper response)
        {
            Debug.Log("Success! - Exchange Apple API");
            // set login type to social
            Config.Instance.SetLoginType("social");

            AccountExchangeDTO accountExchangeDTO = JsonUtility.FromJson<AccountExchangeDTO>(response.Text);

            _forgeLoginManager.LoginWithToken(accountExchangeDTO.access_token, accountExchangeDTO.refresh_token);
        }

        private void LoginWithManualToken()
        {
            if (!string.IsNullOrEmpty(_manualAppleToken))
            {
                LoginWithApple(_manualAppleToken);
            }
        }

        #endregion
    }
}