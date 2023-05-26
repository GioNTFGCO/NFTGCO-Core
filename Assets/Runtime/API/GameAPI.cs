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
        public static void GetGamesRequest(Action<RequestException, List<GameDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.BASE_URL + NTFGCOAPI.GAME_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetGames");

            RestClient.Get(request, (err, res) =>
            {
                List<GameDTO> games = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameDTO>>(res.Text);
                callback(err, games);
            });
        }

        public static void GetGameByIdRequest(long gameId, Action<RequestException, GameDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.BASE_URL + $"{NTFGCOAPI.GAME_BASE_URL}/{gameId.ToString()}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetGameById");

            RestClient.Get(request, (err, res) =>
            {
                callback(err, JsonUtility.FromJson<GameDTO>(res.Text));
            });
        }

        public static void GetLatestGameStateRequest(long gameId, string accountId, Action<RequestException, GameStateDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.BASE_URL + $"{NTFGCOAPI.GAME_BASE_URL}/state/{gameId.ToString()}/{accountId}",
                EnableDebug = true,
            };

            Debug.Log("Get request: GetLatestGameState");

            RestClient.Get(request, (err, res) =>
            {
                callback(err, Newtonsoft.Json.JsonConvert.DeserializeObject<GameStateDTO>(res.Text));
            });
        }

        public static void GetGameEventsRequest(long gameId, string userId, System.DateTime from, System.DateTime to, Action<RequestException, List<GameEventDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");
            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Headers = headers,
                Uri = NTFGCOAPI.BASE_URL + $"{NTFGCOAPI.GAME_BASE_URL}/event/{gameId.ToString()}/{userId}?from=" + from.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "&to=" + to.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                EnableDebug = true,
            };

            Debug.Log("Get request: GetGameEvents");

            RestClient.Get(request, (err, res) =>
            {
                callback(err, Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameEventDTO>>(res.Text));
            });
        }

        public static void CreateGameEventRequest(CreateGameEventDTO requestData, Action<RequestException, string> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.BASE_URL + $"{NTFGCOAPI.GAME_BASE_URL}/event/{requestData.gameId}/{requestData.accountId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameEvent");

            RestClient.Post(request, (err, res) =>
            {
                callback(err, "Success!");
            });
        }

        public static void CreateGameStateRequest(CreateGameStateDTO requestData, Action<RequestException, string> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {NTFGCOAPI.BASE_URL}");

            RequestHelper request = new RequestHelper
            {
                ContentType = NTFGCOAPI.CONTENT_TYPE_JSON,
                Uri = NTFGCOAPI.BASE_URL + $"{NTFGCOAPI.GAME_BASE_URL}/state/{requestData.gameId}/{requestData.accountId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameState");

            RestClient.Post(request, (err, res) =>
            {
                callback(err, "Success!");
            });
        }
    }
}