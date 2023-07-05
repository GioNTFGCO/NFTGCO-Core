using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace NFTGCO.API
{
    public static class NFTAPI
    {
        public static void GetNftAvailableXp(long nftId, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSSN}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + $"{NTFGCOAPI.NFT_BASE_URL}/{nftId}/total-xp",
                Headers = headers,
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetNftAvailableXp");

            RestClient.Get(request, (err, res) =>
            {
                if (Int64.TryParse(res.Text, out var callBackValue))
                    callback(err, callBackValue);
            });
        }

        public static void IncreaseNftXpRequest(IncreaseNftXpRequest requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSSN}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + $"{NTFGCOAPI.NFT_BASE_URL}/increase/xp",
                Headers = headers,
                EnableDebug = true,
                Body = requestData
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: IncreaseNftXp");

            RestClient.Post(request, callback);
        }

        public static void CreateInitialAvatarRequest(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSSN}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID }
            };

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + NTFGCOAPI.NFT_BASE_URL,
                Headers = headers,
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: CreateInitialAvatar");

            RestClient.Post(request, callback);
        }

        public static void GetLastAvatar(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSSN}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID }
            };

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + NTFGCOAPI.NFT_BASE_URL + $"/avatar",
                Headers = headers,
                EnableDebug = true,
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetLastAvatar");

            RestClient.Get(request, callback);
        }

        public static void GetLeaderboard(int amount, Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
                { "browser_info", "" },
                { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
                { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
                { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
                { "ssn", $"{NFTGCOConfig.Instance.GetSSN}" },
                { "ts", $"{NTFGCOAPI.GetTime()}" },
                { "client_version", GameSettingsSO.Instance.CoreVersionID }
            };

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("topSize", amount.ToString());

            if (amount == 10 || amount == 20)
            {
                RequestHelper request = new RequestHelper()
                {
                    ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                    Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL}/top",
                    Headers = headers,
                    EnableDebug = true,
                    Params = parameters
                };

                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogError("Error. Check internet connection!");
                    return;
                }

                Debug.Log("Get request: Leaderboard");

                RestClient.Get(request, callback);
            }
            else
                Debug.LogWarning("Amount must be 10 or 20");
        }
    }
}