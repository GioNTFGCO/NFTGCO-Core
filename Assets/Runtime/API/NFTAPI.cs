using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Forge.API
{
    public static class NFTAPI
    {
        public static void GetAccountNftsRequest(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.NFT_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsRequest");
            RestClient.Get(request, callback);
        }

        public static void GetAccountNftsByIdRequest(string id,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/user/{id}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByIdRequest");

            RestClient.Get(request, callback);
        }

        public static void GetAccountNftsByWalletAddressRequest(string walletAddress,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{walletAddress}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByAddressRequest");

            RestClient.Get(request, callback);
        }

        public static void GetNftAvailableXp(long id, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{id}/available-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetNftAvailableXp");

            RestClient.Get(request, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }

        public static void GetTotalXpOfOwnerByUserId(string id,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{id}/user/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByUserId");

            RestClient.Get(request, callback);
        }

        public static void GetTotalXpOfOwnerByAddress(string address,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{address}/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByAddress");

            RestClient.Get(request, callback);
        }

        public static void IncreaseNftXpRequest(IncreaseNftXpRequest requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/increase/xp",
                EnableDebug = true,
                Headers = headers,
                Body = requestData,
            };

            Debug.Log("Post request: IncreaseNftXp");

            RestClient.Post(request, callback);
        }

        public static void CreateInitialAvatarRequest(string id,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            Dictionary<string, string> userAccount = new Dictionary<string, string>();
            userAccount.Add("accountId", id);

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.NFT_BASE_URL,
                EnableDebug = true,
                Headers = headers,
                Body = JsonConvert.SerializeObject(userAccount)
            };

            Debug.Log("Post request: CreateInitialAvatar");
            RestClient.Post(request, callback);
        }

        public static void GetLastAvatar(string id,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.NFT_BASE_URL + $"/avatar/{id}",
                EnableDebug = true,
                Headers = headers
            };

            Debug.Log("Get request: GetLastAvatar");
            RestClient.Get(request, callback);
        }
    }
}