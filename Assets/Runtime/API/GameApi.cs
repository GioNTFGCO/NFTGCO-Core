using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Forge.API
{
    public static class GameApi
    {
        private const string BASE_URL = "https://dev.gaxos99.com";
        private const string CONTENT_TYPE = "application/json";
        private const string GAME_BASE_URL = "/api/game/v1/games";

        public static void GetGamesRequest(Action<RequestException, List<GameDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + BASE_URL);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + GAME_BASE_URL,
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetGames");

            RestClient.Get(currentRequest, (err, res) =>
            {
                List<GameDTO> games = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameDTO>>(res.Text);
                callback(err, games);
            });
        }

        public static void GetGameByIdRequest(long gameId, Action<RequestException, GameDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + BASE_URL);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{GAME_BASE_URL}/{gameId.ToString()}",
                EnableDebug = true,
                Headers = headers,
            };

            Debug.Log("Get request: GetGameById");

            RestClient.Get(currentRequest, (err, res) =>
            {
                callback(err, JsonUtility.FromJson<GameDTO>(res.Text));
            });
        }

        public static void GetLatestGameStateRequest(long gameId, string userId, Action<RequestException, GameStateDTO> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", $"Bearer {BASE_URL}");

            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Headers = headers,
                Uri = BASE_URL + $"{GAME_BASE_URL}/state/{gameId.ToString()}/{userId}",
                EnableDebug = true,
            };

            Debug.Log("Get request: GetLatestGameState");

            RestClient.Get(currentRequest, (err, res) =>
            {
                callback(err, Newtonsoft.Json.JsonConvert.DeserializeObject<GameStateDTO>(res.Text));
            });
        }

        public static void GetGameEventsRequest(long gameId, string userId, System.DateTime from, System.DateTime to, Action<RequestException, List<GameEventDTO>> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + BASE_URL);
            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Headers = headers,
                Uri = BASE_URL + $"{GAME_BASE_URL}/event/{gameId.ToString()}/{userId}?from=" + from.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture) + "&to=" + to.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture),
                EnableDebug = true,
            };

            Debug.Log("Get request: GetGameEvents");

            RestClient.Get(currentRequest, (err, res) =>
            {
                callback(err, Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameEventDTO>>(res.Text));
            });
        }

        public static void CreateGameEventRequest(long gameId, string userId, CreateGameEventDTO requestData, Action<RequestException, string> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + BASE_URL);

            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{GAME_BASE_URL}/event/{gameId.ToString()}/{userId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameEvent");

            RestClient.Post(currentRequest, (err, res) =>
            {
                callback(err, "Success!");
            });
        }

        public static void CreateGameStateRequest(long gameId, string userId, CreateGameStateDTO requestData, Action<RequestException, string> callback)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer " + BASE_URL);

            RequestHelper currentRequest = new RequestHelper
            {
                ContentType = CONTENT_TYPE,
                Uri = BASE_URL + $"{GAME_BASE_URL}/state/{gameId.ToString()}/{userId}",
                EnableDebug = true,
                Headers = headers,
                BodyString = Newtonsoft.Json.JsonConvert.SerializeObject(requestData)
            };

            Debug.Log("Post request: CreateGameState");

            RestClient.Post(currentRequest, (err, res) =>
            {
                callback(err, "Success!");
            });
        }
    }
}