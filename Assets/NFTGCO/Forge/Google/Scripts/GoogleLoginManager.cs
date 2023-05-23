using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forge;
using Forge.API;
using Google;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using UnityEngine;

namespace NFTGCO
{
    public class GoogleLoginManager : MonoBehaviour
    {
        [SerializeField] private ForgeLoginManager _forgeLoginManager;
        [SerializeField] private ForgeManagerUi _forgeManagerUi;
        [SerializeField] private string _webClientId = "<your client id here>";

        private GoogleSignInConfiguration _configuration;

        [Header("Test")]
        [SerializeField] private string _manualGoogleToken;
        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton LoginWithManualTokenButton = new NFTGCO.Helpers.InspectorButton("LoginWithManualToken");

        private void Awake()
        {
            _configuration = new GoogleSignInConfiguration { WebClientId = _webClientId, RequestEmail = true, RequestIdToken = true };
        }
        private void Start()
        {
            _forgeManagerUi.DelegateButtonLoginCallback("GOOGLE", () =>
            {
                if (string.IsNullOrEmpty(_webClientId))
                    return;

                OnSignIn();
                _forgeManagerUi.ShowHideBlockPanel(true);
            });


#if UNITY_ANDROID && !UNITY_EDITOR
            if (string.IsNullOrEmpty(Config.Instance.AccessToken))
                OnSignIn();
            //OnSignInSilently();
#endif
        }

        private void OnSignIn()
        {
            GoogleSignIn.Configuration = _configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            Debug.Log("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        private void OnSignInSilently()
        {
            GoogleSignIn.Configuration = _configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;

            GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
        }

        private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted)
            {
                using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        Debug.Log($"Got Error: {error.Status} {error.Message}");
                        _forgeManagerUi.ShowHideBlockPanel(false);
                    }
                    else
                    {
                        Debug.Log($"Got Unexpected Exception?!? {task.Exception}");
                        _forgeManagerUi.ShowHideBlockPanel(false);
                    }
                }
            }
            else if (task.IsCanceled)
            {
                Debug.Log("Canceled");
                _forgeManagerUi.ShowHideBlockPanel(false);
            }
            else
            {
                Debug.Log("Welcome: " + task.Result.DisplayName + "!");
                //set social name in forge stored settings, to use in the future
                ForgeStoredSettings.Instance.SetSocialName(task.Result.DisplayName);
                //start login with google token exchange
                LoginWithGoogle(task.Result.IdToken);
            }
        }
        #region NFTGCO
        private void LoginWithGoogle(string googleUserToken)
        {
            AuthApi.AuthGoogleRequest(googleUserToken, AuthGoogleCallback);
        }

        private void AuthGoogleCallback(RequestException message, AccountExchangeDTO accountExchange)
        {
            Debug.Log("Success! - Exchange Google API");
            //set login type to social
            Config.Instance.SetLoginType("social");

            _forgeLoginManager.LoginWithToken(accountExchange.access_token);
        }
        private void LoginWithManualToken()
        {
            if (!string.IsNullOrEmpty(_manualGoogleToken))
            {
                LoginWithGoogle(_manualGoogleToken);
            }
        }
        #endregion
    }
}