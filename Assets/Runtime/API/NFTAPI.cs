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
        private static List<TokenDetailsDTO> parseTokenResponse(ResponseHelper response)
        {
            AccountNFTsDTO accountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            return accountNFTs.nftDetails;
        }

        public static void GetAccountNftsRequest(string accessToken,
            Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.NFT_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsRequest");

            RestClient.Get(request, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }

        public static void GetAccountNftsByIdRequest(string accessToken, string id,
            Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/user/{id}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByIdRequest");

            RestClient.Get(request, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }

        public static void GetAccountNftsByWalletAddressRequest(string accessToken, string walletAddress,
            Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{walletAddress}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByAddressRequest");

            RestClient.Get(request, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }

        public static void GetNftAvailableXp(string accessToken, long id, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
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

        public static void GetTotalXpOfOwnerByUserId(string accessToken, string id,
            Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{id}/user/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByUserId");

            RestClient.Get(request, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }

        public static void GetTotalXpOfOwnerByAddress(string accessToken, string address,
            Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/{address}/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByAddress");

            RestClient.Get(request, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }

        public static void IncreaseNftXpRequest(string accessToken, IncreaseNftXpRequest requestData,
            Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.NFT_BASE_URL}/increase/xp",
                EnableDebug = true,
                Headers = headers,
                Body = requestData,
            };

            Debug.Log("Post request: IncreaseNftXp");

            RestClient.Post(request, (err, res) => { callback(err, Int64.Parse(res.Text)); });
        }

        public static void CreateInitialAvatarRequest(string accessToken, string id,
            Action<RequestException, AvatarDataDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

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
            RestClient.Post(request, (err, res) => { callback(err, JsonUtility.FromJson<AvatarDataDTO>(res.Text)); });
        }

        public static void GetLastAvatar(string accessToken, string id,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {accessToken}");

            RequestHelper request = new RequestHelper()
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.NFT_BASE_URL + $"/avatar/{id}",
                EnableDebug = true,
                Headers = headers
            };

            Debug.Log("Get request: GetLastAvatar");
            RestClient.Get(request, (err, res) => { callback(err, res); });
        }
    }
}