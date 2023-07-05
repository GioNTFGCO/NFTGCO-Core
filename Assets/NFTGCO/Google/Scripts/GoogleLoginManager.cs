using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NFTGCO.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Serialization;

#if !UNITY_EDITOR && UNITY_ANDROID
using Google;
#endif

namespace NFTGCO
{
    public class GoogleLoginManager : MonoBehaviour
    {
        [SerializeField] private NFTGCOLoginManager nftgcoLoginManager;
        [SerializeField] private NFTGCOManagerUi nftgcoManagerUi;
        [SerializeField] private string _webClientId = "<your client id here>";
#if !UNITY_EDITOR && UNITY_ANDROID
        private GoogleSignInConfiguration _configuration;
#endif
        [Space] [Header("Test")] [SerializeField]
        private string _manualGoogleToken;

        [SerializeField] private NFTGCO.Helpers.InspectorButton LoginWithManualTokenButton =
            new NFTGCO.Helpers.InspectorButton("LoginWithManualToken");

        private void Awake()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            _configuration = new GoogleSignInConfiguration
            {
                WebClientId = _webClientId, RequestEmail =
                    true,
                RequestIdToken = true
            };
#endif
        }

        private void Start()
        {
            nftgcoManagerUi.DelegateButtonLoginCallback("GOOGLE", () =>
            {
                if (string.IsNullOrEmpty(_webClientId))
                    return;

#if UNITY_ANDROID && !UNITY_EDITOR
                OnSignIn();
                GameServerLoadingScreen.OnShowLoadingScreen?.Invoke();
#endif
            });


#if UNITY_ANDROID && !UNITY_EDITOR
            if (string.IsNullOrEmpty(NFTGCOConfig.Instance.AccessToken))
            {
                OnSignIn();
                GameServerLoadingScreen.OnShowLoadingScreen?.Invoke();
            }

#endif
        }
#if !UNITY_EDITOR && UNITY_ANDROID
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
                        GameServerLoadingScreen.OnHideLoadingScreen?.Invoke();
                    }
                    else
                    {
                        Debug.Log($"Got Unexpected Exception?!? {task.Exception}");
                        GameServerLoadingScreen.OnHideLoadingScreen?.Invoke();
                    }
                }
            }
            else if (task.IsCanceled)
            {
                Debug.Log("Canceled");
                GameServerLoadingScreen.OnHideLoadingScreen?.Invoke();
            }
            else
            {
                Debug.Log("Welcome: " + task.Result.DisplayName + "!");
                //set social name in forge stored settings, to use in the future
                NFTGCOStoredManager.Instance.SetSocialName(task.Result.DisplayName);
                //start login with google token exchange
                LoginWithGoogle(task.Result.IdToken);
            }
        }
#endif

        #region NFTGCO

        private void LoginWithGoogle(string googleUserToken)
        {
            AccountAPI.AuthGoogleRequest(googleUserToken, AuthGoogleCallback);
        }

        private void AuthGoogleCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                if (!NFTGCOConfig.Instance.EnabledRegistration)
                {
                    //registration unavailable
                    nftgcoManagerUi.ShowRegistrationUnablePanel();
                }

                return;
            }

            if (string.IsNullOrEmpty(response.Text)) 
                return;
            
            Debug.Log("Success! - Exchange Google API");
            //set login type to social
            NFTGCOConfig.Instance.SetLoginType("social");

            AccountExchangeDTO accountExchange = JsonUtility.FromJson<AccountExchangeDTO>(response.Text);

            nftgcoLoginManager.LoginWithToken(accountExchange.access_token, accountExchange.refresh_token);
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