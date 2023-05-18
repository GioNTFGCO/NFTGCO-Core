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

        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTGCO.Core.Global.NFTGCOGamesId _gameId;

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
            GameApi.GetGamesRequest(GetGamesCallback);
        }
        private void GetGamesCallback(RequestException exception, List<GameDTO> games)
        {
            games.ForEach(game => Debug.Log("[GameRequests] Get game name: " + game.name + " and id: " + game.id));
        }
        public void GetGameById()
        {
            GameApi.GetGameByIdRequest(_catchedGameId, GetGameByIdCallback);
        }
        private void GetGameByIdCallback(RequestException exception, GameDTO game)
        {
            Debug.Log("[GameRequests] Get game (by id) name: " + game.name);
        }
        public void GetGameState()
        {
            GameApi.GetLatestGameStateRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id, GetGameStateCallback);
        }
        private void GetGameStateCallback(RequestException exception, GameStateDTO gameState)
        {
            if (gameState != null && gameState.state != null)
            {
                Debug.Log("[GameRequests] Get game state: " + Newtonsoft.Json.JsonConvert.SerializeObject(gameState.state));

                ForgeStoredSettings.Instance.SetGameStateDTO(new GameStateDTO()
                {
                    gameId = gameState.gameId,
                    id = gameState.id,
                    userId = gameState.userId,
                    state = new Dictionary<string, object>()
                });
                
                foreach (KeyValuePair<string, object> pair in gameState.state)
                {
                    ForgeStoredSettings.Instance.GameState.state.Add(pair.Key, pair.Value);
                }
            }
        }
        public void GetGameEvents()
        {
            GameApi.GetGameEventsRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), GetGameEventsCallback);
        }
        private void GetGameEventsCallback(RequestException exception, List<GameEventDTO> gameEvents)
        {
            gameEvents.ForEach(gameEvent => Debug.Log("[GameRequests] Get game event: " + Newtonsoft.Json.JsonConvert.SerializeObject(gameEvent.@event)));
        }
        public void SendGameState()
        {
            CreateGameStateDTO createGameState = new CreateGameStateDTO();
            createGameState.accountId = ForgeStoredSettings.Instance.AccountDTOResponse.id;
            createGameState.gameId = _catchedGameId;

            #region Custom Send Game State
            //create a new dictionary state
            createGameState.state = new Dictionary<string, object>();
            //fill Game State DTO state dictionary

            #endregion

            GameApi.CreateGameStateRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id, createGameState, SendGameStateCallback);
        }
        private void SendGameStateCallback(RequestException exception, string response)
        {
            Debug.Log($"[GameRequests] Sent game state, response: {response}");
        }
        public void SendGameEvent(string[] keys, object[] values)
        {
            CreateGameEventDTO gameEvent = new CreateGameEventDTO();
            gameEvent.accountId = ForgeStoredSettings.Instance.AccountDTOResponse.id;
            gameEvent.gameId = _catchedGameId;
            gameEvent.@event = new Dictionary<string, object>();
            for (int i = 0; i < keys.Length; i++)
            {
                gameEvent.@event[keys[i]] = values[i];
            }
            GameApi.CreateGameEventRequest(_catchedGameId, ForgeStoredSettings.Instance.AccountDTOResponse.id, gameEvent, SendGameEventCallback);
        }
        private void SendGameEventCallback(RequestException exception, string response)
        {
            Debug.Log($"[GameRequests] Sent game event, response: {response}");
        }
    }
}