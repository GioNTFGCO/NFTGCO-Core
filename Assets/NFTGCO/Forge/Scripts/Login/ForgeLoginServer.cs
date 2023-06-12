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
        [SerializeField] private ForgeLoginNFT _forgeLoginNFT;

        private string _webURL;

        public void LoginWithToken(string userToken, string refreshToken)
        {
            _webURL = Application.absoluteURL;
#if !UNITY_EDITOR && UNITY_WEBGL
            int index = _webURL.IndexOf("?_auth=");
            userToken = _webURL.Substring(index + 7);
            Debug.Log($"new URL {userToken}");
#endif
            AuthByToken(userToken, refreshToken);
        }

        private void AuthByToken(string accessToken, string refreshToken)
        {
            if (!string.IsNullOrEmpty(accessToken))
                Config.Instance.SetAccessToken(accessToken);
            if (!string.IsNullOrEmpty(refreshToken))
                Config.Instance.SetRefreshToken(refreshToken);

            GetAccountData();
            _forgeLoginNFT.GetNFTS();
        }

        public void GetAccountData()
        {
            AccountAPI.GetAccountData(GetAccountDataCallback);
        }

        private void GetAccountDataCallback(RequestException exception, ResponseHelper response)
        {
            if (response != null)
            {
                AccountDto accountDto = JsonConvert.DeserializeObject<AccountDto>(response.Text);
                Debug.Log("Get Account Data Callback.");
                ForgeStoredSettings.Instance.SetAccountDTOResponse(accountDto);
            }
        }


        public void AuthWithCredentialsCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                if (exception.IsHttpError || exception.StatusCode == 400)
                {
                    UiMessage.OnMessageSent?.Invoke("User not found, or need to confirm email.");
                    return;
                }
            }

            AuthResponseDTO authResponse = JsonConvert.DeserializeObject<AuthResponseDTO>(response.Text);

            Config.Instance.SetAccessToken(authResponse.access_token);
            Config.Instance.SetRefreshToken(authResponse.refresh_token);

            Debug.Log($"Response server token: {authResponse.access_token}");

            if (authResponse.access_token == null)
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

        private void RefreshTokenCallback(RequestException exception, ResponseHelper response)
        {
            AuthResponseDTO authResponse = JsonConvert.DeserializeObject<AuthResponseDTO>(response.Text);
            AuthByToken(authResponse.access_token, authResponse.refresh_token);
        }
    }
}