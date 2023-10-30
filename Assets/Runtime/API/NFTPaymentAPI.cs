using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using UnityEngine;

namespace NFTGCO.API
{
    public static class NFTPaymentAPI
    {
        public static void MintAvatar(AvatarMintServer mintServer, Action<RequestException, ResponseHelper> callback)
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
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.PAYMENT_BASE_URL}{GameSettingsSO.Instance.GamePlatformEnum.ToString().ToLower()}/iap",
                Headers = headers,
                EnableDebug = NTFGCOAPI.IsEditor(),
                Body = mintServer
            };

            RestClient.Post(request, callback);
        }

        public static void MintAvatarNFT2(MintData mintData, Action<RequestException, ResponseHelper> callback)
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
                EnableDebug = NTFGCOAPI.IsEditor(),
                Body = mintData
            };

            RestClient.Post(request, callback);
        }
    }
}