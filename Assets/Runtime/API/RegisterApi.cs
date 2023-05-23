using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFTGCO.Core.RestClient;
using NFTGCO.Models;
using Forge;

namespace Forge.API
{
    public static class RegisterApi
    {
        public static void RegisterUserRequest(string user_name, string user_username, string user_email, string user_password, System.Action<RequestException, string> callback)
        {
            RequestHelper request = new RequestHelper
            {
                Uri = NTFGCOApi.BASE_URL + NTFGCOApi.ACCOUNT_BASE_URL,
                Body = new RegisterUserInfo
                {
                    name = user_name,
                    username = user_username,
                    email = user_email,
                    password = user_password
                },
                EnableDebug = true
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: Register user");

            RestClient.Post(request, (err, res) =>
            {
                callback(err, res.Text);
            });

            // RestClient.Post<RegisterUserInfo>(request)
            // .Then(res =>
            // {
            //     // And later we can clear the default query string params for all requests
            //     RestClient.ClearDefaultParams();

            //     Debug.Log($"Success {JsonUtility.ToJson(res, true)}");
            // })
            // .Catch(err => Debug.Log($"Error {err.Message}"));
        }
    }
}