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
        //private bool _requestCompleted;

        public void GetNFTS()
        {
            NFTApi.GetAccountNftsRequest(Config.Instance.AccessToken, GetNFTsCallback);
        }
        private void GetNFTsCallback(RequestException exception, List<TokenDetailsDTO> response)
        {
            //_requestCompleted = false;

            ForgeStoredSettings.Instance.RetrieveRobotParts(response);
            Debug.Log("Get nfts callback");

            //_requestCompleted = true;
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
            NFTApi.GetAccountNftsByIdRequest(Config.Instance.AccessToken, userId, GetNFTSByIdCallback);
        }
        public void GetNFTsByAddress(string walletAddress)
        {
            // 1st parameter is user wallet address
            NFTApi.GetAccountNftsByWalletAddressRequest(Config.Instance.AccessToken, walletAddress, GetNFTSByIdCallback);
        }
        private void GetNFTSByIdCallback(RequestException exception, List<TokenDetailsDTO> response)
        {
            response.ForEach(i => Debug.Log(i.tokenId));
        }
        public void GetAvailableNFTXpById()
        {
            // 1st parameter is NFT id
            NFTApi.GetNftAvailableXp(Config.Instance.AccessToken, 40, GetAvailableNFTXpByIdCallback);
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
            NFTApi.IncreaseNftXpRequest(Config.Instance.AccessToken, requestData, IncreaseNftXpCallback);
        }
        private void IncreaseNftXpCallback(RequestException exception, long response)
        {
            // request responds with a <long> number
            Debug.Log($"Xp added: {response}");
        }
        #endregion
    }
}