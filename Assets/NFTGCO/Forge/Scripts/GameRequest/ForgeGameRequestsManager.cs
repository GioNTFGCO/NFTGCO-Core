using Forge.API;
using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forge
{
    public class ForgeGameRequestsManager : NFTGCO.Helpers.Singleton<ForgeGameRequestsManager>
    {
        public static System.Action OnSendGameState;
        public static System.Action OnGetGameState;

        [NFTGCO.Helpers.SearchableEnum] [SerializeField]
        private NFTGCO.Core.Global.NFTGCOGamesId _gameId;

        private long _catchedGameId => (long)_gameId;

        protected override void Awake()
        {
            base.Awake();
            //ForgeGlobalData.Instance.SetNFTRobotID(0);
        }

        private void OnEnable()
        {
            OnSendGameState += SendGameState;
            OnGetGameState += GetGameState;
        }

        private void OnDisable()
        {
            OnSendGameState -= SendGameState;
            OnGetGameState -= GetGameState;
        }

        public void GetGames()
        {
            GameAPI.GetGamesRequest(GetGamesCallback);
        }

        private void GetGamesCallback(RequestException exception, ResponseHelper response)
        {
            List<GameDTO> games = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameDTO>>(response.Text);

            games.ForEach(game => Debug.Log("[GameRequests] Get game name: " + game.name + " and id: " + game.id));
        }

        public void GetGameById()
        {
            GameAPI.GetGameByIdRequest(_catchedGameId, GetGameByIdCallback);
        }

        private void GetGameByIdCallback(RequestException exception, ResponseHelper repsonse)
        {
            GameDTO game = JsonUtility.FromJson<GameDTO>(repsonse.Text);
            Debug.Log("[GameRequests] Get game (by id) name: " + game.name);
        }

        public void GetGameState()
        {
            GameAPI.GetLatestGameStateRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id,
                GetGameStateCallback);
        }

        private void GetGameStateCallback(RequestException exception, ResponseHelper response)
        {
            if (response != null)
            {
                GameStateDTO gameStateDto = Newtonsoft.Json.JsonConvert.DeserializeObject<GameStateDTO>(response.Text);
                if (gameStateDto.state != null)
                {
                    Debug.Log("[GameRequests] Get game state: " +
                              Newtonsoft.Json.JsonConvert.SerializeObject(gameStateDto.state));

                    ForgeStoredSettings.Instance.SetGameStateDTO(new GameStateDTO()
                    {
                        gameId = gameStateDto.gameId,
                        id = gameStateDto.id,
                        userId = gameStateDto.userId,
                        state = new Dictionary<string, object>()
                    });

                    foreach (KeyValuePair<string, object> pair in gameStateDto.state)
                    {
                        ForgeStoredSettings.Instance.GameState.state.Add(pair.Key, pair.Value);
                    }
                }
            }
        }

        public void GetGameEvents()
        {
            GameAPI.GetGameEventsRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id,
                DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), GetGameEventsCallback);
        }

        private void GetGameEventsCallback(RequestException exception, ResponseHelper response)
        {
            List<GameEventDTO> gameEvents =
                Newtonsoft.Json.JsonConvert.DeserializeObject<List<GameEventDTO>>(response.Text);
            
            gameEvents.ForEach(gameEvent => Debug.Log("[GameRequests] Get game event: " +
                                                      Newtonsoft.Json.JsonConvert.SerializeObject(gameEvent.@event)));
        }

        public void SendGameState()
        {
            #region Custom Send Game State

            CreateGameStateDTO createGameState = new CreateGameStateDTO()
            {
                accountId = ForgeStoredSettings.Instance.AccountDTOResponse.id,
                gameId = _catchedGameId,
                state = new Dictionary<string, object>()
                {
                    //fill Game State DTO state dictionary
                }
            };

            #endregion

            GameAPI.CreateGameStateRequest(createGameState, SendGameStateCallback);
        }

        private void SendGameStateCallback(RequestException exception, ResponseHelper response)
        {
            Debug.Log($"[GameRequests] Sent game state, response: {response.StatusCode}");
        }

        public void SendGameEvent(string[] keys, object[] values)
        {
            CreateGameEventDTO gameEvent = new CreateGameEventDTO()
            {
                accountId = ForgeStoredSettings.Instance.AccountDTOResponse.id,
                gameId = _catchedGameId,
                @event = new Dictionary<string, object>()
            };

            for (int i = 0; i < keys.Length; i++)
            {
                gameEvent.@event[keys[i]] = values[i];
            }

            GameAPI.CreateGameEventRequest(gameEvent, SendGameEventCallback);
        }

        private void SendGameEventCallback(RequestException exception, ResponseHelper response)
        {
            Debug.Log($"[GameRequests] Sent game event, response: {response.StatusCode}");
        }
    }
}