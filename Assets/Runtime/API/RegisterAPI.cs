using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Core.RestClient;
using NFTGCO.Models;
using NFTGCO;

namespace NFTGCO.API
{
    public static class RegisterAPI
    {
        public static void RegisterUserRequest(RegisterUserInfo userInfo,
            System.Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithoutAuth();
            
            RequestHelper request = new RequestHelper
            {
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                Headers = headers,
                EnableDebug = NTFGCOAPI.IsEditor(),
                Body = new RegisterUserInfo
                {
                    name = userInfo.name,
                    username = userInfo.username,
                    email = userInfo.email,
                    password = userInfo.password
                }
            };

            RestClient.Post(request, callback);
        }

        public static void RegistrationStatus(System.Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithoutAuth();

            RequestHelper request = new RequestHelper
            {
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}registration",
                Headers = headers,
                EnableDebug = NTFGCOAPI.IsEditor(),
            };

            RestClient.Get(request, callback);
        }
    }
}