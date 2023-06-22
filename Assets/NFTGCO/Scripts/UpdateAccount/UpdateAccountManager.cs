using System;
using System.Collections;
using System.Collections.Generic;
using NFTGCO.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Helpers;
using NFTGCO.Models.DTO;
using SceneField.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class UpdateAccountManager : MonoBehaviour
    {
        [SerializeField] private UpdateAccountUi _updateAccountUi;
        [SerializeField] private NFTGCOLoginNFT nftgcoLoginNft;

        private void Start()
        {
            _updateAccountUi.ClosePanel();
            this.DoAfter(() => NFTGCOStoredManager.Instance.AccountDTOResponse != null,
                delegate { Init(); });
        }

        public void Init()
        {
            _updateAccountUi.Init(OnUpdateUserUsername, null);
            _updateAccountUi.ShowCurrentUsername(NFTGCOStoredManager.Instance.AccountDTOResponse.username);
        }
        public void OpenUpdateNicknamePanel()
        {
            _updateAccountUi.ShowPanel();
            _updateAccountUi.ShowCurrentUsername(NFTGCOStoredManager.Instance.AccountDTOResponse.username);
        }
        public bool CheckFirstSocialLogin(string username, string email)
        {
            bool state = username == email;
            return state;
        }

        private void OnUpdateUserUsername()
        {
            if (!_updateAccountUi.IsUsernameEmpty() && _updateAccountUi.IsUsernameValid())
            {
                UpdateUserNickname();
            }
            else
            {
                UiMessage.OnMessageSent?.Invoke("Enter a username of at least 3 characters");
            }
        }



        private void UpdateUserNickname()
        {
            AccountUsernameDTO accountUsername = new AccountUsernameDTO()
            {
                name = NFTGCOStoredManager.Instance.AccountDTOResponse.name,
                username = _updateAccountUi.GetUsername(),
                email = NFTGCOStoredManager.Instance.AccountDTOResponse.email
            };
            AccountAPI.UpdateUserUsername(accountUsername, UpdateUserUsernameCallback);
        }

        private void UpdateUserUsernameCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                if (exception.StatusCode == 400) ////this nickname is already used
                {
                    UiMessage.OnMessageSent?.Invoke("This username is already used");
                }
            }

            //server send a 200, means the nickname is changed
            if (response.StatusCode == 200)
            {
                //nickname is changed
                UiMessage.OnMessageSent?.Invoke($"Your username now is: {_updateAccountUi.GetUsername()}");
                NFTGCOStoredManager.Instance.AccountDTOResponse.username = _updateAccountUi.GetUsername();
                _updateAccountUi.ClosePanel();
                
                nftgcoLoginNft.GetNFTS();
            }
        }
    }
}