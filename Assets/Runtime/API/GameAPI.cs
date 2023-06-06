using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Forge.API
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
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + NTFGCOAPI.GAME_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

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
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/{gameId.ToString()}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetGameById");

            RestClient.Get(request, callback);
        }

        public static void GetLatestGameStateRequest(long gameId, string accountId,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/state/{gameId.ToString()}/{accountId}",
                EnableDebug = true,
            };

            Debug.Log("Get request: GetLatestGameState");

            RestClient.Get(request, callback);
        }

        public static void GetGameEventsRequest(long gameId, string userId, System.DateTime from, System.DateTime to,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.GetBASEURL() + $"{NTFGCOAPI.GAME_BASE_URL}/event/{gameId.ToString()}/{userId}?from=" +
                      from.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "&to=" +
                      to.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                EnableDebug = true,
            };

            Debug.Log("Get request: GetGameEvents");

            RestClient.Get(request, callback);
        }

        public static void CreateGameEventRequest(CreateGameEventDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() +
                      $"{NTFGCOAPI.GAME_BASE_URL}/event/{requestData.gameId}/{requestData.accountId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameEvent");

            RestClient.Post(request, callback);
        }

        public static void CreateGameStateRequest(CreateGameStateDTO requestData,
            Action<RequestException, ResponseHelper> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {Config.Instance.AccessToken}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.GetBASEURL() +
                      $"{NTFGCOAPI.GAME_BASE_URL}/state/{requestData.gameId}/{requestData.accountId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameState");

            RestClient.Post(request, callback);
        }
    }
}