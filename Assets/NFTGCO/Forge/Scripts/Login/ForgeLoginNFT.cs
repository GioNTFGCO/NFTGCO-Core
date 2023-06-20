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
            GetLastAvatar();
        }

        private void CreateAvatar()
        {
            NFTAPI.CreateInitialAvatarRequest(CreateAvatarCallback);
        }

        private void CreateAvatarCallback(RequestException exception, ResponseHelper response)
        {
            if (response.StatusCode == 201)
            {
                AvatarDataDTO avatarData = JsonUtility.FromJson<AvatarDataDTO>(response.Text);
                Debug.Log($"Create avatar: {avatarData.id}");
                WW.Waiters.WaitController.DoAfterWait(1f, GetLastAvatar);
            }
        }

        private void GetLastAvatar()
        {
            NFTAPI.GetLastAvatar(GetLastAvatarCallback);
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
    }
}