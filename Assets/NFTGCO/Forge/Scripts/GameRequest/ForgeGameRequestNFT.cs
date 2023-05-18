using System.Collections;
using System.Collections.Generic;
using Forge.API;
using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using UnityEngine;
namespace Forge
{
    public class ForgeGameRequestNFT : MonoBehaviour
    {
        public static System.Action<long> OnGetAvailableNFTXpById;
        public static System.Action<long, long> OnIncreaseNftXp;
        public static System.Action OnGetNFTsById;
        public static System.Action OnGetNFTsByAddress;
        public static System.Action OnGetUserTotalXpById;

        private void OnEnable()
        {
            OnGetAvailableNFTXpById += GetAvailableNFTXpById;
            OnIncreaseNftXp += IncreaseNftXp;
            OnGetNFTsById += GetNFTsById;
            OnGetNFTsByAddress += GetNFTsByAddress;
            OnGetUserTotalXpById += GetUserTotalXpById;
        }
        private void OnDisable()
        {
            OnGetAvailableNFTXpById -= GetAvailableNFTXpById;
            OnIncreaseNftXp -= IncreaseNftXp;
            OnGetNFTsById -= GetNFTsById;
            OnGetNFTsByAddress -= GetNFTsByAddress;
            OnGetUserTotalXpById -= GetUserTotalXpById;
        }

        private void GetAvailableNFTXpById(long nftId)
        {
            // 1st parameter is NFT id
            NFTApi.GetNftAvailableXp(Config.Instance.AccessToken, nftId, GetAvailableNFTXpByIdCallback);
        }
        private void GetAvailableNFTXpByIdCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log("[NFT Requests] NFT Available Xp: " + response);
            ForgeStoredSettings.Instance.SetCurrentNFTXp(response);
        }
        private void IncreaseNftXp(long nftId, long xpAmount)
        {
            // tokenId is NFT id, and quantity is amount of XP
            IncreaseNftXpRequest requestData = new IncreaseNftXpRequest();
            requestData.tokenId = nftId;
            requestData.quantity = xpAmount;
            Debug.Log(requestData.tokenId);
            NFTApi.IncreaseNftXpRequest(Config.Instance.AccessToken, requestData, IncreaseNftXpCallback);
        }
        private void IncreaseNftXpCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log("[NFT Requests] Xp added: " + response);
        }

        #region Methods are never used
        private void GetNFTsById()
        {
            // 1st parameter is user id
            NFTApi.GetAccountNftsByIdRequest(Config.Instance.AccessToken, ForgeStoredSettings.Instance.AccountDTOResponse.userId, GetNFTSByIdCallback);
        }
        private void GetNFTsByAddress()
        {
            // 1st parameter is user wallet address
            NFTApi.GetAccountNftsByWalletAddressRequest(Config.Instance.AccessToken, ForgeStoredSettings.Instance.AccountDTOResponse.walletAddress, GetNFTSByIdCallback);
        }
        private void GetNFTSByIdCallback(RequestException exception, List<TokenDetailsDTO> response)
        {
            response.ForEach(i => Debug.Log("[NFT Requests] nft id: " + i.tokenId));
        }
        private void GetUserTotalXpById()
        {
            // 1st parameter is user id
            NFTApi.GetTotalXpOfOwnerByUserId(Config.Instance.AccessToken, ForgeStoredSettings.Instance.AccountDTOResponse.userId, GetUserTotalXpByIdCallback);
        }
        private void GetUserTotalXpByIdCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log("[NFT Requests] Total User Xp: " + response);
        }
        #endregion
    }
}