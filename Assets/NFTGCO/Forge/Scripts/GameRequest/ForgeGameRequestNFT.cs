using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Forge.API;
using NFTGCO.Models.DTO;
using NFTGCO.Core.RestClient;
using NFTGCO.Helpers;
using UnityEngine;

namespace Forge
{
    public class ForgeGameRequestNFT : MonoBehaviour
    {
        public static System.Action OnGetAvailableNFTXpById;
        public static System.Action<long> OnIncreaseNftXp;

        private void OnEnable()
        {
            OnGetAvailableNFTXpById += GetAvailableNFTXpById;
            OnIncreaseNftXp += IncreaseNftXp;
        }

        private void OnDisable()
        {
            OnGetAvailableNFTXpById -= GetAvailableNFTXpById;
            OnIncreaseNftXp -= IncreaseNftXp;
        }
        
        private void GetAvailableNFTXpById()
        {
            NFTAPI.GetNftAvailableXp(ForgeStoredSettings.Instance.StoredResponse.First().tokenId, GetAvailableNFTXpByIdCallback);
        }

        private void GetAvailableNFTXpByIdCallback(RequestException exception, long response)
        {
            Debug.Log("[NFT Requests] NFT Available Xp: " + response);
            ForgeStoredSettings.Instance.SetCurrentNFTXp(response);
        }

        private void IncreaseNftXp(long xpAmount)
        {
            IncreaseNftXpRequest requestData = new IncreaseNftXpRequest()
            {
                id = ForgeStoredSettings.Instance.StoredResponse.First().tokenId,
                quantity = xpAmount
            };

            NFTAPI.IncreaseNftXpRequest(requestData, IncreaseNftXpCallback);
        }

        private void IncreaseNftXpCallback(RequestException exception, ResponseHelper response)
        {
            if (Int64.TryParse(response.Text, out var callBackValue))
            {
                // request responds with a <long> number
                Debug.Log("[NFT Requests] Xp added: " + callBackValue);
            }
        }
    }
}