using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using Forge.API;
using System;

namespace Forge
{
    public class ForgeLoginServer : MonoBehaviour
    {
        [SerializeField] private ForgeLoginNFT _forgeLoginNFT;

        private string _webURL;

        public System.Action OnAuthFinish;

        [SerializeField] private NFTGCO.Helpers.InspectorButton CreateAvatarButton = new NFTGCO.Helpers.InspectorButton("CreateAvatar");

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
        private void CreateAvatar()
        {
            NFTApi.CreateInitialAvatarRequest(Config.Instance.AccessToken, CreateAvatarCallback);
        }

        public NFTApi.AvatarDataDTO avatarData;
        private void CreateAvatarCallback(RequestException exception, NFTApi.AvatarDataDTO response)
        {
            Debug.Log(response.id);
            if (exception == null)
            {
                Debug.Log($"Create avatar: {response}");
                ForgeStoredSettings.Instance.ClearData();
                _forgeLoginNFT.GetNFTS();
                avatarData = response;
            }
            else
            {
                Debug.Log(exception);
            }

        }

        public void GetAccountData()
        {
            AuthApi.GetAccountData(Config.Instance.AccessToken, GetAccountDataCallback);
        }
        private void GetAccountDataCallback(RequestException exception, AccountDto response)
        {
            if (response != null)
            {
                Debug.Log("Get Account Data Callback.");
                ForgeStoredSettings.Instance.SetAccountDTOResponse(response);
                OnAuthFinish?.Invoke();

                //check if first time (check the nickname), if the nickname is empty, then it is the first time
                //then show the nickname panel
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
        #region Methods that are never used
        public void RefreshToken()
        {
            AuthApi.RefreshTokenData body = new AuthApi.RefreshTokenData
            {
                refreshToken = Config.Instance.RefreshToken,
                loginType = Config.Instance.LoginType
            };
            AuthApi.RefreshTokenRequest(body, RefreshTokenCallback);
        }

        private void RefreshTokenCallback(RequestException exception, AuthResponseDTO response)
        {
            AuthByToken(response.access_token, response.refresh_token);
        }
        public void GetUserTotalXpById(string userId)
        {
            // 1st parameter is user id
            NFTApi.GetTotalXpOfOwnerByUserId(Config.Instance.AccessToken, userId, GetUserTotalXpByIdCallback);
        }
        private void GetUserTotalXpByIdCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log($"Total User Xp: {response}");
        }
        #endregion
    }
}