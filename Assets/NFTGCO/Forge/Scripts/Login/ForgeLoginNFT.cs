using System;
using System.Collections;
using System.Collections.Generic;
using Forge.API;
using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using UnityEngine;
namespace Forge
{
    public class ForgeLoginNFT : MonoBehaviour
    {
        private AccountNFTsDTO _userAccountNFTs;
        private AccountNFTsDTO _accountNFTs;
        
        [SerializeField]
        private NFTGCO.Helpers.InspectorButton CreateAvatarButton = new NFTGCO.Helpers.InspectorButton("CreateAvatar");

        [SerializeField]
        private NFTGCO.Helpers.InspectorButton
            GetLastAvatarButton = new NFTGCO.Helpers.InspectorButton("GetLastAvatar");

        public void GetNFTS()
        {
            NFTAPI.GetAccountNftsRequest(Config.Instance.AccessToken, GetNFTsCallback);
        }
        private void GetNFTsCallback(RequestException exception, List<TokenDetailsDTO> response)
        {
            //_requestCompleted = false;

            ForgeStoredSettings.Instance.RetrieveRobotParts(response);
            Debug.Log("Get nfts callback");

            //_requestCompleted = true;
        }
        private void CreateAvatar()
        {
            NFTAPI.CreateInitialAvatarRequest(Config.Instance.AccessToken,
                ForgeStoredSettings.Instance.AccountDTOResponse.id, CreateAvatarCallback);
        }

        private void GetLastAvatar()
        {
            NFTAPI.GetLastAvatar(Config.Instance.AccessToken, ForgeStoredSettings.Instance.AccountDTOResponse.id,
                GetLastAvatarCallback);
        }

        private void GetLastAvatarCallback(RequestException exception, ResponseHelper response)
        {
            Debug.Log(exception);
            Debug.Log(response);
        }

        public NFTGCO.Models.DTO.AvatarDataDTO avatarData;

        private void CreateAvatarCallback(RequestException exception, NFTGCO.Models.DTO.AvatarDataDTO response)
        {
            if (exception == null)
            {
                Debug.Log($"Create avatar: {response.id}");
                ForgeStoredSettings.Instance.ClearData();
                GetNFTS();
                avatarData = response;
            }
            else
            {
                Debug.Log(exception);
            }
        }
        #region Methods are never used
        private void GetNFTSCallback(RequestException exception, ResponseHelper response)
        {
            _accountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            Debug.Log(response.Text);
        }
        private void GetNFTSByIdCallback(RequestException exception, ResponseHelper response)
        {
            _userAccountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            Debug.Log(response.Text);
        }

        private void GetAvailableNFTXpByIdCallback(RequestException exception, ResponseHelper response)
        {
            // request responds with a <long> number
            Debug.Log("NFT Available Xp: " + response.Text);
        }
        public void GetNFTsById(string userId)
        {
            // 1st parameter is user id
            NFTAPI.GetAccountNftsByIdRequest(Config.Instance.AccessToken, userId, GetNFTSByIdCallback);
        }
        public void GetNFTsByAddress(string walletAddress)
        {
            // 1st parameter is user wallet address
            NFTAPI.GetAccountNftsByWalletAddressRequest(Config.Instance.AccessToken, walletAddress, GetNFTSByIdCallback);
        }
        private void GetNFTSByIdCallback(RequestException exception, List<TokenDetailsDTO> response)
        {
            response.ForEach(i => Debug.Log(i.tokenId));
        }
        public void GetAvailableNFTXpById()
        {
            // 1st parameter is NFT id
            NFTAPI.GetNftAvailableXp(Config.Instance.AccessToken, 40, GetAvailableNFTXpByIdCallback);
        }
        private void GetAvailableNFTXpByIdCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log("NFT Available Xp: " + response);
        }
        public void IncreaseNftXp()
        {
            // tokenId is NFT id, and quantity is amount of XP
            IncreaseNftXpRequest requestData = new IncreaseNftXpRequest();
            requestData.tokenId = 40;
            requestData.quantity = 300;
            Debug.Log(requestData.tokenId);
            NFTAPI.IncreaseNftXpRequest(Config.Instance.AccessToken, requestData, IncreaseNftXpCallback);
        }
        private void IncreaseNftXpCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log($"Xp added: {response}");
        }
        #endregion
    }
}