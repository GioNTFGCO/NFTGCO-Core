using System.Collections;
using System.Collections.Generic;
using Forge;
using Forge.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Models.DTO;
using UnityEngine;

namespace NFTGCO
{
    public class UpdateAccountManager : MonoBehaviour
    {
        [SerializeField] private UpdateAccountUi _updateAccountUi;

        public void CheckFirstSocialLogin()
        {
            if (Config.Instance.LoginType == "social" && ForgeStoredSettings.Instance.AccountDTOResponse.name == ForgeStoredSettings.Instance.AccountDTOResponse.email)
            {
                //open panel here
            }
        }
        private void UpdateUserNickname()
        {
            AccountNicknameDTO accountNickname = new AccountNicknameDTO()
            {
                name = ForgeStoredSettings.Instance.SocialName,
                username = _updateAccountUi.GetNickName(),//add nickname here!
                email = ForgeStoredSettings.Instance.AccountDTOResponse.email
            };
            AuthApi.UpdateUserNickname(Config.Instance.AccessToken, accountNickname, UpdateUserNicknameCallback);
        }
        private void UpdateUserNicknameCallback(RequestException exception, string arg2)
        {
            Debug.Log(exception);
            Debug.Log(arg2);
            //this nickname is already used
            if (exception.StatusCode == 400)
            {

            }
        }
    }
}