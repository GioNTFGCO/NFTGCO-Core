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
    public class ForgeLoginNFT : MonoBehaviour
    {
        public Action OnNFTGCODataReceived;

        private AccountNFTsDTO _userAccountNFTs;
        private AccountNFTsDTO _accountNFTs;

        public void GetNFTS()
        {
            NFTAPI.GetAccountNftsRequest(GetNFTsCallback);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void GetNFTsCallback(RequestException exception, ResponseHelper response)
        {
            AccountNFTsDTO accountNFTs = JsonUtility.FromJson<AccountNFTsDTO>(response.Text);
            List<TokenDetailsDTO> accountNFTsList = accountNFTs.nftDetails;

            ForgeStoredSettings.Instance.RetrieveRobotParts(accountNFTsList);
            Debug.Log("Get nfts callback");

            if (accountNFTsList.Count == 0)
            {
                GetLastAvatar();
            }
            else
            {
                OnNFTGCODataReceived?.Invoke();
            }
        }

        private void CreateAvatar()
        {
            NFTAPI.CreateInitialAvatarRequest(ForgeStoredSettings.Instance.AccountDTOResponse.id, CreateAvatarCallback);
        }

        private void CreateAvatarCallback(RequestException exception, ResponseHelper response)
        {
            if (response.StatusCode == 201)
            {
                AvatarDataDTO avatarData = JsonUtility.FromJson<AvatarDataDTO>(response.Text);
                Debug.Log($"Create avatar: {avatarData.id}");
                WW.Waiters.WaitController.DoAfterWait(1f, () =>
                {
                    GetLastAvatar();
                });
            }
        }

        private void GetLastAvatar()
        {
            NFTAPI.GetLastAvatar(ForgeStoredSettings.Instance.AccountDTOResponse.id,
                GetLastAvatarCallback);
        }

        private void GetLastAvatarCallback(RequestException exception, ResponseHelper response)
        {
            if (string.IsNullOrEmpty(response.Text))
            {
                Debug.Log("Error to get last avatar");
                CreateAvatar();
                return;
            }
            
            if (response.StatusCode == 200)
            {
                NFTGCO.Models.DTO.AvatarDataDTO avatarData =
                    JsonUtility.FromJson<NFTGCO.Models.DTO.AvatarDataDTO>(response.Text);
            
                TokenDetailsDTO tokenDetails = new TokenDetailsDTO()
                {
                    tokenId = avatarData.id,
                    tokenAttributes = avatarData.tokenAttributes
                };
            
                List<TokenDetailsDTO> avatars = new List<TokenDetailsDTO>();
                avatars.Add(tokenDetails);
            
                ForgeStoredSettings.Instance.GetLastAvatar(avatarData);
                ForgeStoredSettings.Instance.RetrieveRobotParts(avatars);
            
                OnNFTGCODataReceived?.Invoke();
            }
        }


        public void IncreaseNftXp(int xp)
        {
            IncreaseNftXpRequest requestData = new IncreaseNftXpRequest()
            {
                tokenId = ForgeStoredSettings.Instance.StoredResponse.First().tokenId,
                quantity = xp
            };
            NFTAPI.IncreaseNftXpRequest(requestData, IncreaseNftXpCallback);
        }

        private void IncreaseNftXpCallback(RequestException exception, ResponseHelper response)
        {
            long callBackValue;
            if (Int64.TryParse(response.Text, out callBackValue))
            {
                // request responds with a <long> number
                Debug.Log($"Xp added: {callBackValue}");
            }
        }
    }
}