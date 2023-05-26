using System.Collections;
using System.Collections.Generic;
using Forge.API;
using NFTGCO.Core.RestClient;
using NFTGCO.Models;
using UnityEngine;
using Newtonsoft.Json.Utilities;

namespace Forge
{
    public class ForgeRegisterManager : MonoBehaviour
    {
        [SerializeField] private ForgeRegisterUi _registerUserUi;
        [SerializeField] private ForgeManagerUi _forgeManagerUi;
        [Space]
        [Header("Test only")]
        [SerializeField] private RegisterUserInfo _userInfo;

        [Space]
        [SerializeField] private NFTGCO.Helpers.InspectorButton RegisterTest = new NFTGCO.Helpers.InspectorButton("Post");

        private void Start()
        {
            _registerUserUi.Init(() =>
            {
                _registerUserUi.RegisterButtonBehaviour(false);

                _registerUserUi.GetFormData();

                RegisterAPI.RegisterUserRequest(_registerUserUi.UserFormData["name"],
                                         _registerUserUi.UserFormData["username"],
                                         _registerUserUi.UserFormData["email"],
                                         _registerUserUi.UserFormData["password"],
                                         RegisterUserCallback);
            }, () =>
            {
                _forgeManagerUi.ShowPanel("Initial");
                _registerUserUi.ClearFields();
            });

#if UNITY_EDITOR
            _registerUserUi.TestForm(_userInfo.name, _userInfo.username, _userInfo.email, _userInfo.password);
#endif
        }

        private void RegisterUserCallback(RequestException exception, string response)
        {
            string successMessage = "Email verification required";
            string errorMessage = "User with this username or email already registered.";

            //if RequestException is null, means the Request has no errors.
            if (exception != null)
            {
                if (exception.IsHttpError || exception.StatusCode == 400)
                {
                    _registerUserUi.RegisterButtonBehaviour(true);
                    _registerUserUi.SetMessage(errorMessage);
                }
            }
            else
            {
                if (response.Contains(successMessage))
                {
                    _registerUserUi.SetMessage("Confirmation was sended to your email - see Junk email also.");
                    _registerUserUi.EnableRegistrationConfirmation();
                    //show login panel
                }
            }
        }
    }
}