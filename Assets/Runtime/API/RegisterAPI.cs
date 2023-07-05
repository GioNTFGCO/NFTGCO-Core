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
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
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
                Uri =$"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}",
                Headers = headers,
                EnableDebug = true,
                Body = new RegisterUserInfo
                {
                    name = userInfo.name,
                    username = userInfo.username,
                    email = userInfo.email,
                    password = userInfo.password
                }
            };

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: Register user");

            RestClient.Post(request, callback);
        }

        public static void RegistrationStatus(System.Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
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
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.ACCOUNT_BASE_URL}/registration",
                Headers = headers,
                EnableDebug = true,
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }
            
            Debug.Log("Get request: Registration Status");

            RestClient.Get(request, callback);
        }
    }
}