using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace NFTGCO.API
{
    public static class AccountAPI
    {
        private const string GrantType = "grant_type";
        private const string NFTGCOService = "nftgco-service";
        private const string ClientID = "client_id";
        private const string Username = "username";
        private const string Password = "password";
        private const string RefreshToken = "/token-exchange/refresh";

        private const string TokenEndpoint = "/auth/realms/nftgco-service/protocol/openid-connect/token";
        private const string ForgetPasswordEndpoint = "/password/forgot";
        private const string TokenExchange = "/token-exchange";

        /// <summary>
        /// Send a request to the server to get the account data
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="callback"></param>
        public static void AuthRequest(string username, string password,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            string postData = "";
            Dictionary<string, string> postParameters = new Dictionary<string, string>()
            {
                { GrantType, Password },
                { ClientID, NFTGCOService },
                { Username, username },
                { Password, password }
            };

            foreach (string key in postParameters.Keys)
                postData += UnityWebRequest.EscapeURL(key) + "=" + UnityWebRequest.EscapeURL(postParameters[key]) + "&";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_URLENCODED,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + TokenEndpoint,
                Headers = headers,
                EnableDebug = true,
                BodyRaw = data
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: Auth");
            Debug.Log($"{request.Uri} {request.ContentType} {request.BodyRaw}");

            RestClient.Post(request, callback);
        }

        /// <summary>
        /// Refresh token request
        /// </summary>
        /// <param name="body"></param>
        /// <param name="callback"></param>
        public static void RefreshTokenRequest(Dictionary<string, string> body,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + NTFGCOAPI.ACCOUNT_BASE_URL + RefreshToken,
                //Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: RefreshToken");

            RestClient.Post(request, callback);
        }

        /// <summary>
        /// Send a request to the server to get the account data
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAccountData(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                Headers = headers,
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetAccountData");

            RestClient.Get(request, callback);
        }

        /// <summary>
        ///  Send a request to the server to reset the password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="callback"></param>
        public static void ForgetPasswordRequest(string email, Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            ForgetPasswordDTO body = new ForgetPasswordDTO(email);

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri =
                    $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}{ForgetPasswordEndpoint}",
                //Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: ForgetPassword");

            RestClient.Post(request, callback);
        }

        /// <summary>
        /// Send a request to the server to update the user nickname
        /// </summary>
        /// <param name="accountNickcname"></param>
        /// <param name="callback"></param>
        public static void UpdateUserUsername(AccountUsernameDTO accountNickcname,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                Headers = headers,
                EnableDebug = true,
                BodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(accountNickcname))
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Put request: UpdateUserNickname");

            RestClient.Put(request, callback);
        }

        /// <summary>
        ///  Send a request to the server to update the user XP
        /// </summary>
        /// <param name="amountXP"></param>
        /// <param name="callback"></param>
        public static void UpdateUserXp(Int64 amountXP,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            Dictionary<string, Int64> body = new Dictionary<string, Int64>()
            {
                { "amount", amountXP }
            };
            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}/xp",
                Headers = headers,
                EnableDebug = true,
                BodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Put request: UpdateUserXp");

            RestClient.Put(request, callback);
        }

        /// <summary>
        /// Send a request to the server to get the user XP
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAvailableUserXp(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" }
            };

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}/xp/available",
                Headers = headers,
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetAvailableUserXp");

            RestClient.Get(request, callback);
        }

        #region TOKEN_EXCHANGE

        /// <summary>
        /// Send a request to the server to exchange a google token for a server token
        /// </summary>
        /// <param name="googleTokenId"></param>
        /// <param name="callback"></param>
        public static void AuthGoogleRequest(string googleTokenId,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" },
                { "tc_version", GameSettingsSO.Instance.TermsAndConditionsVersion },
                { "tc_accepted", "true" }
            };

            AccountTokenExchange body = new AccountTokenExchange(googleTokenId, "google");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}{TokenExchange}",
                Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            Debug.Log("Post request: AuthGoogle");

            RestClient.Post(request, callback);
        }

        /// <summary>
        /// Send a request to the server to exchange an Apple token for a server token
        /// </summary>
        /// <param name="appleTokenId"></param>
        /// <param name="callback"></param>
        public static void AuthAppleRequest(string appleTokenId, Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID },
                { "session_uuid ", $"{NTFGCOAPI.GetSessionUUID()}" },
                { "tc_version", GameSettingsSO.Instance.TermsAndConditionsVersion },
                { "tc_accepted", "true" }
            };

            AccountTokenExchange body = new AccountTokenExchange(appleTokenId, "apple");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}{TokenExchange}",
                Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(body)
            };

            Debug.Log("Post request: AuthApple");

            RestClient.Post(request, callback);
        }

        #endregion
    }
}