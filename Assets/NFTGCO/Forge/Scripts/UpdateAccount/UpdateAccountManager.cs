using System;
using System.Collections;
using System.Collections.Generic;
using Forge;
using Forge.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Helpers;
using NFTGCO.Models.DTO;
using UnityEngine;

namespace NFTGCO
{
    public class UpdateAccountManager : MonoBehaviour
    {
        [SerializeField] private UpdateAccountUi _updateAccountUi;

        private void Start()
        {
            _updateAccountUi.ClosePanel();
            this.DoAfter(() => ForgeStoredSettings.Instance.AccountDTOResponse != null,
                delegate { Init(); });
        }

        public void Init()
        {
            _updateAccountUi.Init(OnUpdateUserUsername, null);
            //_updateAccountUi.ShowCurrentUsername(ForgeStoredSettings.Instance.AccountDTOResponse.username);
        }

        public bool CheckFirstSocialLogin()
        {
            bool state = Config.Instance.LoginType == "social" &&
                         ForgeStoredSettings.Instance.AccountDTOResponse.name ==
                         ForgeStoredSettings.Instance.AccountDTOResponse.email;
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
                Debug.Log("enter a username of at least 3 characters");
            }
        }

        public void OpenUpdateNicknamePanel()
        {
            _updateAccountUi.ShowPanel();
            _updateAccountUi.ShowCurrentUsername(ForgeStoredSettings.Instance.AccountDTOResponse.username);
        }

        private void UpdateUserNickname()
        {
            AccountUsernameDTO accountUsername = new AccountUsernameDTO()
            {
                name = ForgeStoredSettings.Instance.AccountDTOResponse.name,
                username = _updateAccountUi.GetUsername(),
                email = ForgeStoredSettings.Instance.AccountDTOResponse.email
            };
            AuthApi.UpdateUserUsername(Config.Instance.AccessToken, accountUsername, UpdateUserUsernameCallback);
        }

        private void UpdateUserUsernameCallback(RequestException exception, ResponseHelper arg2)
        {
            if (exception != null)
            {
                if (exception.StatusCode == 400)////this nickname is already used
                {
                    Debug.Log("This username is already used");
                }
            }
            //server send a 200, means the nickname is changed
            if (arg2.StatusCode==200)
            {
                //nickname is changed
                _updateAccountUi.ClosePanel();
            }
        }
    }
}