using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using Forge.API;
using System;
using Newtonsoft.Json;

namespace Forge
{
    public class ForgeLoginServer : MonoBehaviour
    {
        public System.Action OnAuthFinish;

        [SerializeField] private ForgeLoginNFT _forgeLoginNFT;

        private string _webURL;

        [Space] 
        [SerializeField] private NFTGCO.Helpers.InspectorButton GetAvailableXPButton =
            new NFTGCO.Helpers.InspectorButton("GetAvailableXP");   
        [SerializeField] private NFTGCO.Helpers.InspectorButton TestUpdateUserXPButton =
            new NFTGCO.Helpers.InspectorButton("TestUpdateUserXP");

        public void LoginWithToken(string userToken)
        {
            _webURL = Application.absoluteURL;

            Debug.Log($"Absolute URL: {_webURL}");
            Debug.Log($"Token from URL: {userToken}");

#if !UNITY_EDITOR && UNITY_WEBGL
            int index = _webURL.IndexOf("?_auth=");
            userToken = _webURL.Substring(index + 7);
            Debug.Log($"new URL {userToken}");
#endif
            AuthByToken(userToken, "");
        }

        private void AuthByToken(string accessToken, string refreshToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                Config.Instance.SetAccessToken(accessToken);
            if (!string.IsNullOrEmpty(refreshToken))
                Config.Instance.SetRefreshToken(refreshToken);

            if (accessToken == null)
            {
                UiMessage.OnMessageSent?.Invoke("Auth error, invalid credentials");
            }
            else
            {
                GetAccountData();
                _forgeLoginNFT.GetNFTS();
            }
        }

        public void GetAccountData()
        {
            AccountAPI.GetAccountData(Config.Instance.AccessToken, GetAccountDataCallback);
        }

        private void GetAccountDataCallback(RequestException exception, AccountDto response)
        {
            if (response != null)
            {
                Debug.Log("Get Account Data Callback.");
                ForgeStoredSettings.Instance.SetAccountDTOResponse(response);
                OnAuthFinish?.Invoke();
            }
        }


        public void AuthWithCredentialsCallback(RequestException exception, AuthResponseDTO response)
        {
            if (exception != null)
            {
                if (exception.IsHttpError || exception.StatusCode == 400)
                {
                    UiMessage.OnMessageSent?.Invoke("User not found, or need to confirm email.");
                    return;
                }
            }

            Config.Instance.SetAccessToken(response.access_token);
            Config.Instance.SetRefreshToken(response.refresh_token);

            // Set login type to user_pass
            Config.Instance.SetLoginType("user_pass");

            Debug.Log($"Response server token: {response.access_token}");

            if (response.access_token == null)
            {
                UiMessage.OnMessageSent?.Invoke("Auth error, token is invalid.");
            }
            else
            {
                GetAccountData();
                _forgeLoginNFT.GetNFTS();
                Debug.Log("(SDK) Get NFTs calling");
            }
        }

        public void RefreshToken()
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                { "refreshToken", Config.Instance.RefreshToken },
                { "loginType", Config.Instance.LoginType }
            };

            AccountAPI.RefreshTokenRequest(body, RefreshTokenCallback);
        }

        private void RefreshTokenCallback(RequestException exception, AuthResponseDTO response)
        {
            AuthByToken(response.access_token, response.refresh_token);
        }

        public Int64 amountForTest;
        private void TestUpdateUserXP()
        {
            UpdateUserXP(amountForTest);
        }
        public void UpdateUserXP(Int64 amountXP)
        {
            AccountAPI.UpdateUserXP(Config.Instance.AccessToken, amountXP, UpdateUserXPCallback);
        }

        private void UpdateUserXPCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                Debug.Log($"The game has a error with the server, server retrieve code {exception.StatusCode}");
            }

            if (response != null)
            {
                var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(response.Text);

                ForgeStoredSettings.Instance.AccountDTOResponse.totalXp = values["availableXp"];
                Debug.Log($"User new XP: {values["availableXp"]}");
            }
        }

        public void GetAvailableXP()
        {
            AccountAPI.GetAvailableUserXP(Config.Instance.AccessToken, GetAvailableXPCallback);
        }

        private void GetAvailableXPCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                Debug.Log($"The game has a error with the server, server retrieve code {exception.StatusCode}");
            }

            if (response != null)
            {
                int result = Int32.Parse(response.Text);
                ForgeStoredSettings.Instance.AccountDTOResponse.totalXp = result;
                Debug.Log($"User XP: {response.Text}");
            }
        }
    }
}