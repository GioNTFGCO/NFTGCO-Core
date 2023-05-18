using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forge.API
{
    public static class NFTApi
    {
        private const string BASE_URL = "https://dev.gaxos99.com";
        private const string CONTENT_TYPE = "application/json";
        private const string NFT_BASE_URL = "/api/nft/v1";

        private static List<TokenDetailsDTO> parseTokenResponse(ResponseHelper response)
        {
            AccountNFTsDTO accountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            return accountNFTs.nftDetails;
        }
        public static void GetAccountNftsRequest(string accessToken, Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + NFT_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsRequest");

            RestClient.Get(currentRequest, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }
        public static void GetAccountNftsByIdRequest(string accessToken, string id, Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/user/{id}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByIdRequest");

            RestClient.Get(currentRequest, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }
        public static void GetAccountNftsByWalletAddressRequest(string accessToken, string walletAddress, Action<RequestException, List<TokenDetailsDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/{walletAddress}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetAccountNftsByAddressRequest");

            RestClient.Get(currentRequest, (err, res) =>
            {
                List<TokenDetailsDTO> accountNFTsList = parseTokenResponse(res);
                callback(err, accountNFTsList);
            });
        }
        public static void GetNftAvailableXp(string accessToken, long id, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/{id}/available-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetNftAvailableXp");

            RestClient.Get(currentRequest, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }
        public static void GetTotalXpOfOwnerByUserId(string accessToken, string id, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/{id}/user/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByUserId");

            RestClient.Get(currentRequest, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }
        public static void GetTotalXpOfOwnerByAddress(string accessToken, string address, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/{address}/total-xp",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetTotalXpOfOwnerByAddress");

            RestClient.Get(currentRequest, (err, res) =>
            {
                long callBackValue;
                if (Int64.TryParse(res.Text, out callBackValue))
                {
                    callback(err, callBackValue);
                }
            });
        }
        public static void IncreaseNftXpRequest(string accessToken, IncreaseNftXpRequest requestData, Action<RequestException, long> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{NFT_BASE_URL}/increase/xp",
                EnableDebug = true,
                Headers = headers,
                Body = requestData,
            };

            Debug.Log("Post request: IncreaseNftXp");

            RestClient.Post(currentRequest, (err, res) =>
            {
                callback(err, Int64.Parse(res.Text));
            });
        }
        public static void CreateInitialAvatarRequest(string accessToken, Action<RequestException, AvatarDataDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + accessToken);

            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + NFT_BASE_URL,
                EnableDebug = true,
                Headers = headers
            };

            Debug.Log("Post request: CreateInitialAvatar");
            RestClient.Post(currentRequest, (err, res) =>
            {
                callback(err, JsonUtility.FromJson<AvatarDataDTO>(res.Text));
            });
        }
        [System.Serializable]
        public class AvatarDataDTO
        {
            public long id;
            public string ownerAddress;
            public string mintType;
            public long tokenId;
            public long avatarTypeId;
            public string mintStatus;
            public List<long> tokenAttributes;
        }
    }
}