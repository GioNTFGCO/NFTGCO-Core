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
        public static void RegisterUserRequest(RegisterUserInfo userInfo, System.Action<RequestException, ResponseHelper> callback)
        {
            RequestHelper request = new RequestHelper
            {
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.ACCOUNT_BASE_URL,
                Body = new RegisterUserInfo
                {
                    name = userInfo.name,
                    username = userInfo.username,
                    email = userInfo.email,
                    password = userInfo.password
                },
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: Register user");

            RestClient.Post(request, callback);
        }
    }
}