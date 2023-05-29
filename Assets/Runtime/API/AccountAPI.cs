using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Forge.API
{
    public static class AccountAPI
    {
        private const string GRANT_TYPE = "grant_type";
        private const string GRANTTYPE = "grantType";
        private const string NFTGCO_SERVICE = "nftgco-service";
        private const string CLIENT_ID = "client_id";
        private const string USERNAME = "username";
        private const string PASSWORD = "password";
        private const string REFRESH_TOKEN = "/token-exchange/refresh";

        private const string TOKEN_ENDPOINT = "/auth/realms/nftgco-service/protocol/openid-connect/token";
        private const string FORGET_PASSWORD_ENDPOINT = "/password/forgot";
        private const string TOKEN_EXCHANGE = "/token-exchange";

        public static void AuthRequest(string username, string password,
            Action<RequestException, AuthResponseDTO> callback)
        {
            string postData = "";
            Dictionary<string, string> postParameters = new Dictionary<string, string>()
            {
                { GRANT_TYPE, PASSWORD },
                { CLIENT_ID, NFTGCO_SERVICE },
                { USERNAME, username },
                { PASSWORD, password }
            };

            foreach (string key in postParameters.Keys)
                postData += UnityWebRequest.EscapeURL(key) + "=" + UnityWebRequest.EscapeURL(postParameters[key]) + "&";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_URLENCODED,
                Uri = NTFGCOAPI.BASE_URL + TOKEN_ENDPOINT,
                EnableDebug = true,
                BodyRaw = data
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: Auth");

            RestClient.Post(request, (err, res) => { callback(err, JsonUtility.FromJson<AuthResponseDTO>(res.Text)); });
        }

        /// <summary>
        /// Refresh token request
        /// </summary>
        /// <param name="body"></param>
        /// <param name="callback"></param>
        public static void RefreshTokenRequest(Dictionary<string, string> body,
            Action<RequestException, AuthResponseDTO> callback)
        {
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.BASE_URL + NTFGCOAPI.ACCOUNT_BASE_URL + REFRESH_TOKEN,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: RefreshToken");

            RestClient.Post(request, (err, res) => { callback(err, JsonUtility.FromJson<AuthResponseDTO>(res.Text)); });
        }

        /// <summary>
        /// Send a request to the server to get the account data
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="callback"></param>
        public static void GetAccountData(string accessToken, Action<RequestException, AccountDto> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountData");

            RestClient.Get(request, (err, res) => { callback(err, JsonUtility.FromJson<AccountDto>(res.Text)); });
        }

        /// <summary>
        ///  Send a request to the server to reset the password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="callback"></param>
        public static void ForgetPasswordRequest(string email, Action<RequestException, string> callback)
        {
            ForgetPasswordDTO body = new ForgetPasswordDTO(email);

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}{FORGET_PASSWORD_ENDPOINT}",
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            Debug.Log("Post request: ForgetPassword");

            RestClient.Post(request, (err, res) => { callback(err, res.Text); });
        }

        /// <summary>
        /// Send a request to the server to update the user nickname
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="accountNickcname"></param>
        /// <param name="callback"></param>
        public static void UpdateUserUsername(string accessToken, AccountUsernameDTO accountNickcname,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                EnableDebug = true,
                Headers = headers,
                BodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(accountNickcname))
            };

            Debug.Log("Put request: UpdateUserNickname");

            RestClient.Put(request, callback);
        }

        /// <summary>
        ///  Send a request to the server to update the user XP
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="amountXP"></param>
        /// <param name="callback"></param>
        public static void UpdateUserXP(string accessToken, Int64 amountXP,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            Dictionary<string, Int64> body = new Dictionary<string, Int64>()
            {
                { "amount", amountXP }
            };
            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}/xp",
                EnableDebug = true,
                Headers = headers,
                BodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))
            };

            RestClient.Put(request, callback);
        }

        /// <summary>
        /// Send a request to the server to get the user XP
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="callback"></param>
        public static void GetAvailableUserXP(string accessToken, Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}/xp/available",
                EnableDebug = true,
                Headers = headers
            };

            RestClient.Get(request, callback);
        }

        #region TOKEN_EXCHANGE

        /// <summary>
        /// Send a request to the server to exchange a google token for a server token
        /// </summary>
        /// <param name="googleTokenId"></param>
        /// <param name="callback"></param>
        public static void AuthGoogleRequest(string googleTokenId,
            Action<RequestException, AccountExchangeDTO> callback)
        {
            AccountTokenExchange body = new AccountTokenExchange(googleTokenId, "google");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}{TOKEN_EXCHANGE}",
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            Debug.Log("Post request: AuthGoogle");

            RestClient.Post(request,
                (err, res) => { callback(err, JsonUtility.FromJson<AccountExchangeDTO>(res.Text)); });
        }

        /// <summary>
        /// Send a request to the server to exchange an Apple token for a server token
        /// </summary>
        /// <param name="appleTokenId"></param>
        /// <param name="callback"></param>
        public static void AuthAppleRequest(string appleTokenId, Action<RequestException, AccountExchangeDTO> callback)
        {
            AccountTokenExchange body = new AccountTokenExchange(appleTokenId, "apple");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.BASE_URL}{NTFGCOAPI.ACCOUNT_BASE_URL}{TOKEN_EXCHANGE}",
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            Debug.Log("Post request: AuthApple");

            RestClient.Post(request,
                (err, res) => { callback(err, JsonUtility.FromJson<AccountExchangeDTO>(res.Text)); });
        }

        #endregion
    }
}