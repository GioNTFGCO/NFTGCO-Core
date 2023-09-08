using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace NFTGCO.API
{
    public static class NFTAPI
    {
        public static void GetNftAvailableXp(long nftId, Action<RequestException, long> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetRequestHeaders();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + $"{NTFGCOAPI.NFT_BASE_URL}{nftId}/total-xp",
                Headers = headers,
                EnableDebug = true
            };

            RestClient.Get(request, (err, res) =>
            {
                if (Int64.TryParse(res.Text, out var callBackValue))
                    callback(err, callBackValue);
            });
        }

        public static void IncreaseNftXpRequest(IncreaseNftXpRequest requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetRequestHeaders();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + $"{NTFGCOAPI.NFT_BASE_URL}increase/xp",
                Headers = headers,
                EnableDebug = true,
                Body = requestData
            };

            RestClient.Post(request, callback);
        }

        public static void CreateInitialAvatarRequest(Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetRequestHeaders();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + NTFGCOAPI.NFT_BASE_URL,
                Headers = headers,
                EnableDebug = true
            };

            RestClient.Post(request, callback);
        }

        public static void GetLastAvatar(Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL}avatar",
                Headers = headers,
                EnableDebug = true,
            };

            RestClient.Get(request, callback);
        }

        public static void GetLeaderboard(int amount, Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("topSize", amount.ToString());

            if (amount == 10 || amount == 20)
            {
                RequestHelper request = new RequestHelper()
                {
                    ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                    Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL}top",
                    Headers = headers,
                    EnableDebug = true,
                    Params = parameters
                };

                RestClient.Get(request, callback);
            }
            else
                Debug.LogWarning("Amount must be 10 or 20");
        }

        public static void UploadImageAsset(string imgURL,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("asset", imgURL);

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL_V2}store",
                Headers = headers,
                EnableDebug = true,
                BodyRaw = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body))
            };

            RestClient.Post(request, callback);
        }

        public static void MintAvatar(MintData mintProperties, Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL_V2}",
                Headers = headers,
                EnableDebug = true,
                Body = mintProperties
            };

            RestClient.Post(request, callback);
        }

        public static void GetAvatarFromWallet(string walletAddress, Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.NFT_BASE_URL_V2}tokens-by-owner/{walletAddress}",
                Headers = headers,
                EnableDebug = true,
            };

            RestClient.Get(request, callback);
        }
    }
}