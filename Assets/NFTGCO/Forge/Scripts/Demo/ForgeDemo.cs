// using Forge.API;
// using NFTGCO.Models.DTO;
// using NFTGCO.Core.RestClient;
// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// namespace Forge
// {
//     public class ForgeDemo : MonoBehaviour
//     {
//         [SerializeField] private InputField userIdInputField;
//         [SerializeField] private string _userIdText;
//         [SerializeField] private string _the_url;
//         [SerializeField] private string _token_from_url;

//         private AuthResponseDTO _authResponse;
//         private AccountNFTsDTO _accountNFTs;
//         private AccountNFTsDTO _userAccountNFTs;

//         // START - Auth Requests
//         public void Auth()
//         {
//             AuthApi.AuthRequest("eduard5", "Parola1?", AuthCallback);
//         }

//         public void AuthByToken()
//         {
//             _the_url = Application.absoluteURL;

// #if UNITY_EDITOR
//             _the_url = "blablabla?_auth=eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJGajYtTmRwUW00RWJCbEc3REdPdUQwUm9DQnhoaWljV3Z1ZzUtSTBiWXBnIn0.eyJleHAiOjE2NjY4MTkwOTAsImlhdCI6MTY2Njc5NzQ5MCwianRpIjoiMzdhNGM2NzAtYTk0Ni00NDBjLTkyZGUtYmQ1OTFkYWFkMWIzIiwiaXNzIjoiaHR0cHM6Ly9kZXYuZ2F4b3M5OS5jb20vYXV0aC9yZWFsbXMvbmZ0Z2NvLXNlcnZpY2UiLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiOGRhM2IxYjgtZjk4MC00M2U3LTkyYWYtZGI3NmU4ZDhmZTNiIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoibmZ0Z2NvLXNlcnZpY2UiLCJzZXNzaW9uX3N0YXRlIjoiM2VkYzRiYmYtOWQyMy00YmJiLWFjMDEtZTAxZmUyYjhlMDJkIiwiYWNyIjoiMSIsImFsbG93ZWQtb3JpZ2lucyI6WyIqIl0sInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJkZWZhdWx0LXJvbGVzLW5mdGdjby1zZXJ2aWNlIiwib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiIsIkFETUlOIiwiVVNFUiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiZW1haWwgcHJvZmlsZSIsInNpZCI6IjNlZGM0YmJmLTlkMjMtNGJiYi1hYzAxLWUwMWZlMmI4ZTAyZCIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJuYW1lIjoiZWR1YXJkIiwicHJlZmVycmVkX3VzZXJuYW1lIjoiZWR1YXJkNSIsImdpdmVuX25hbWUiOiJlZHVhcmQiLCJlbWFpbCI6ImVkdWFyZC5kb3JuZWFudSs2QHNvZnRiaW5hdG9yLmNvbSJ9.GZAlkOsv4WigDzAVnRyNnbtwJENqyonUrfq2tWr-Hjx6y0s9BgldGH-Vcr_zAWKGyrd9BYay_7Ctz_u8dp0m1a9m8-ieNa6w4jvPorQnf7tKYcDeYN_U6xf2tFJqzXArYBRAz8ja7uAzwcDxTm25bYc9TZG9bikU-arAtaYRgck_8DRzNzFsbL1EUvsHbwIBy1riWRrf8sTPW9nNRBNZVAfUzGbJEaNEw6LkFR4htKuiZMBS3samBFEq-H0G1Qcs1UqSdPDUFDoIdRfNZLWJAPlakwdZIsUPSIgezfaF2zDtexdqws4WCNFXmLLDx0vrrUz30g3cnnEW6H0OrrJdEQ";
// #endif

//             int index = _the_url.IndexOf("?_auth=");

//             _token_from_url = _the_url.Substring(index + 7);
//             Debug.Log("(url)" + _token_from_url);

//             AuthByToken(_token_from_url);
//         }

//         void AuthCallback(RequestException exception, AuthResponseDTO response)
//         {
//             Config.Instance.SetAccessToken(response.access_token);
//             Config.Instance.SetRefreshToken(response.refresh_token);
//             Debug.Log(response.access_token);
//         }



//         void AuthWithTokenCallback(RequestException exception, AuthResponseDTO response)
//         {
//             Config.Instance.SetAccessToken(response.access_token);
//             Config.Instance.SetRefreshToken(response.refresh_token);
//             Debug.Log(response.access_token);
//         }
//         void AuthByToken(string token)
//         {
//             Config.Instance.SetAccessToken(token);
//             Config.Instance.SetRefreshToken(token);
//             Debug.Log(token);
//         }
//         public void RefreshToken()
//         {
//             AuthApi.RefreshTokenRequest(Config.Instance.RefreshToken, RefreshTokenCallback);
//         }

//         void RefreshTokenCallback(RequestException exception, AuthResponseDTO response)
//         {
//             // Save access token and refresh token and use them later when needed
//             Config.Instance.SetAccessToken(response.access_token);
//             Config.Instance.SetRefreshToken(response.refresh_token);
//             Debug.Log(response.access_token);

//         }

//         void GetAccountDataCallback(RequestException exception, AccountDto response)
//         {
//             Debug.Log(response.id);
//         }

//         public void GetAccountData()
//         {
//             AuthApi.GetAccountData(Config.Instance.AccessToken, GetAccountDataCallback);
//         }

//         // END - Auth Requests

//         // START - NFTs Requests

//         void GetNFTSCallback(RequestException exception, List<TokenDetailsDTO> response)
//         {
//             response.ForEach(i => Debug.Log(i.tokenId));
//         }


//         void GetNFTSByIdCallback(RequestException exception, List<TokenDetailsDTO> response)
//         {
//             response.ForEach(i => Debug.Log(i.tokenId));
//         }

//         public void GetNFTsById(string userId)
//         {
//             // 1st parameter is user id
//             NFTApi.GetAccountNftsByIdRequest(Config.Instance.AccessToken, userId, GetNFTSByIdCallback);
//         }

//         void GetAvailableNFTXpByIdCallback(RequestException exception, long response)
//         {
//             // request responds with a <long> number
//             Debug.Log("NFT Available Xp: " + response);
//         }

//         void GetUserTotalXpByIdCallback(RequestException exception, long response)
//         {
//             // request responds with a <long> number
//             Debug.Log("Total User Xp: " + response);
//         }

//         void IncreaseNftXpCallback(RequestException exception, long response)
//         {
//             // request responds with a <long> number
//             Debug.Log("Xp added: " + response);
//         }

//         public void GetNFTS()
//         {
//             NFTApi.GetAccountNftsRequest(Config.Instance.AccessToken, GetNFTSCallback);
//         }


//         public void GetNFTsByAddress(string walletAddress)
//         {
//             // 1st parameter is user wallet address
//             NFTApi.GetAccountNftsByAddressRequest(Config.Instance.AccessToken, walletAddress, GetNFTSByIdCallback);
//         }

//         public void GetAvailableNFTXpById()
//         {
//             // 1st parameter is NFT id
//             NFTApi.GetNftAvailableXp(Config.Instance.AccessToken, 40, GetAvailableNFTXpByIdCallback);
//         }

//         public void GetUserTotalXpById(string userId)
//         {
//             // 1st parameter is user id
//             NFTApi.GetTotalXpOfOwnerByUserId(Config.Instance.AccessToken, userId, GetUserTotalXpByIdCallback);
//         }

//         public void IncreaseNftXp()
//         {
//             // tokenId is NFT id, and quantity is amount of XP
//             IncreaseNftXpRequest requestData = new IncreaseNftXpRequest();
//             requestData.tokenId = 40;
//             requestData.quantity = 300;
//             Debug.Log(requestData.tokenId);
//             NFTApi.IncreaseNftXp(Config.Instance.AccessToken, requestData, IncreaseNftXpCallback);
//         }

//         // END - NFTs Requests

//         // START - Game Requests

//         public void GetGamesCallback(RequestException exception, List<GameDTO> games)
//         {
//             games.ForEach(game => Debug.Log(game.name));
//         }

//         public void GetGames()
//         {
//             GameApi.GetGamesRequest(GetGamesCallback);
//         }

//         public void GetGameByIdCallback(RequestException exception, GameDTO game)
//         {
//             Debug.Log(game.name);
//         }

//         public void GetGameById()
//         {
//             GameApi.GetGameByIdRequest(1, GetGameByIdCallback);
//         }

//         public void GetGameStateCallback(RequestException exception, GameStateDTO gameState)
//         {
//             Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(gameState.state));
//         }

//         public void GetGameState()
//         {
//             GameApi.GetLatestGameStateRequest(1, "8da3b1b8-f980-43e7-92af-db76e8d8fe3b", GetGameStateCallback);
//         }

//         public void GetGameEventsCallback(RequestException exception, List<GameEventDTO> gameEvents)
//         {
//             gameEvents.ForEach(gameEvent => Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(gameEvent.@event)));
//         }

//         public void GetGameEvents()
//         {
//             GameApi.GetGameEventsRequest(1, "8da3b1b8-f980-43e7-92af-db76e8d8fe3b", DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), GetGameEventsCallback);
//         }
//         // END - Game Requests
//     }
// }