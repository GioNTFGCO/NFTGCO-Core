using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace NFTGCO.API
{
    public static class GameAPI
    {
        /// <summary>
        /// Get all games
        /// </summary>
        /// <param name="callback"></param>
        public static void GetGamesRequest(Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.GAME_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetGames");

            RestClient.Get(request, callback);
        }

        /// <summary>
        /// Get game by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="callback"></param>
        public static void GetGameByIdRequest(long gameId, Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/{gameId.ToString()}",
                EnableDebug = true,
                Headers = headers,
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetGameById");

            RestClient.Get(request, callback);
        }

        public static void GetLatestGameStateRequest(long gameId, long accountId,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/state/{gameId.ToString()}/{accountId}",
                EnableDebug = true,
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetLatestGameState");

            RestClient.Get(request, callback);
        }

        public static void GetGameEventsRequest(long gameId, long userId, System.DateTime from, System.DateTime to,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/event/{gameId.ToString()}/{userId}?from=" +
                      from.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "&to=" +
                      to.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                EnableDebug = true,
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Get request: GetGameEvents");

            RestClient.Get(request, callback);
        }

        public static void CreateGameEventRequest(CreateGameEventDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() +
                      $"{NTFGCOAPI.GAME_BASE_URL}/event/{requestData.gameId}/{requestData.accountId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: CreateGameEvent");

            RestClient.Post(request, callback);
        }

        public static void CreateGameStateRequest(CreateGameStateDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{NTFGCOAPI.GetBASEURL()}{NTFGCOAPI.GAME_BASE_URL}/state",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            Debug.Log("Post request: CreateGameState");

            RestClient.Post(request, callback);
        }
    }
}