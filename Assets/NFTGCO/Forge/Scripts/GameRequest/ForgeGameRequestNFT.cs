using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            NFTAPI.GetNftAvailableXp(nftId, GetAvailableNFTXpByIdCallback);
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
            requestData.id = ForgeStoredSettings.Instance.StoredResponse.First().tokenId;
            requestData.quantity = xpAmount;
            Debug.Log(requestData.id);
            NFTAPI.IncreaseNftXpRequest(requestData, IncreaseNftXpCallback);
        }

        private void IncreaseNftXpCallback(RequestException exception, ResponseHelper response)
        {
            long callBackValue;
            if (Int64.TryParse(response.Text, out callBackValue))
            {
                // request responds with a <long> number
                Debug.Log("[NFT Requests] Xp added: " + callBackValue);
            }
        }

        #region Methods are never used

        private void GetNFTsById()
        {
            // 1st parameter is user id
            NFTAPI.GetAccountNftsByIdRequest(ForgeStoredSettings.Instance.AccountDTOResponse.userId,
                GetNFTSByIdCallback);
        }

        private void GetNFTsByAddress()
        {
            // 1st parameter is user wallet address
            NFTAPI.GetAccountNftsByWalletAddressRequest(ForgeStoredSettings.Instance.AccountDTOResponse.walletAddress,
                GetNFTSByIdCallback);
        }

        private void GetNFTSByIdCallback(RequestException exception, ResponseHelper response)
        {
            AccountNFTsDTO accountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            List<TokenDetailsDTO> accountNFTsList = accountNFTs.nftDetails;

            accountNFTsList.ForEach(i => Debug.Log("[NFT Requests] nft id: " + i.tokenId));
        }

        private void GetUserTotalXpById()
        {
            // 1st parameter is user id
            NFTAPI.GetTotalXpOfOwnerByUserId(ForgeStoredSettings.Instance.AccountDTOResponse.userId,
                GetUserTotalXpByIdCallback);
        }

        private void GetUserTotalXpByIdCallback(RequestException exception, ResponseHelper response)
        {
            long callBackValue;
            if (Int64.TryParse(response.Text, out callBackValue))
            {
                // request responds with a <long> number
                Debug.Log("[NFT Requests] Total User Xp: " + callBackValue);
            }
        }

        #endregion
    }
}