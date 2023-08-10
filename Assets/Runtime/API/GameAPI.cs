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
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + NTFGCOAPI.GAME_BASE_URL,
                Headers = headers,
                EnableDebug = true
            };

            RestClient.Get(request, callback);
        }

        /// <summary>
        /// Get game by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="callback"></param>
        public static void GetGameByIdRequest(long gameId, Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment + $"{NTFGCOAPI.GAME_BASE_URL}/{gameId.ToString()}",
                Headers = headers,
                EnableDebug = true
            };

            RestClient.Get(request, callback);
        }

        public static void GetLatestGameStateRequest(long gameId, long accountId,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment +
                      $"{NTFGCOAPI.GAME_BASE_URL}/state/{gameId.ToString()}/{accountId}",
                Headers = headers,
                EnableDebug = true,
            };

            RestClient.Get(request, callback);
        }

        public static void GetGameEventsRequest(long gameId, long userId, System.DateTime from, System.DateTime to,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment +
                      $"{NTFGCOAPI.GAME_BASE_URL}/event/{gameId.ToString()}/{userId}?from=" +
                      from.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "&to=" +
                      to.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                Headers = headers,
                EnableDebug = true,
            };

            RestClient.Get(request, callback);
        }

        public static void CreateGameEventRequest(CreateGameEventDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = GameSettingsSO.Instance.GetGameEnvironment +
                      $"{NTFGCOAPI.GAME_BASE_URL}/event/{requestData.gameId}/{requestData.accountId}",
                Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            RestClient.Post(request, callback);
        }

        public static void CreateGameStateRequest(CreateGameStateDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("Error. Check internet connection!");
                return;
            }

            var headers = NTFGCOAPI.GetModifiedHeadersWithUserData();

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = $"{GameSettingsSO.Instance.GetGameEnvironment}{NTFGCOAPI.GAME_BASE_URL}/state",
                Headers = headers,
                EnableDebug = true,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            RestClient.Post(request, callback);
        }
    }
}