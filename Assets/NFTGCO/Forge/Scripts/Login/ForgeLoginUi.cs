using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace Forge
{
    public class ForgeLoginUi : MonoBehaviour
    {
        [Header("Login")]
        [SerializeField] private TMP_InputField _userNameIF;
        [SerializeField] private TMP_InputField _userPasswordIF;
        [SerializeField] private Button _loginButton;

        public string GetUserName() => _userNameIF.text;
        public string GetUserPassword() => _userPasswordIF.text;

        [Header("Forget Password")]
        [SerializeField] private Button _forgetPasswordButton;
        [SerializeField] private CanvasGroup _forgetPasswordPanel;
        [SerializeField] private Button _closeForgetPasswordButton;
        [SerializeField] private TMP_InputField _emailImputField;
        [SerializeField] private Button _resetPasswordButton;
        [SerializeField] private CanvasGroup _emailSentPanel;

        public string GetForgetPasswordEmail() => _emailImputField.text;

        private void Start()
        {
            _emailImputField.text = "";

            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_forgetPasswordPanel, false);
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_emailSentPanel, false);

            //forget password
            _forgetPasswordButton.onClick.AddListener(() =>
            {
                NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_forgetPasswordPanel, true);
                NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_emailSentPanel, false);
            });
            _closeForgetPasswordButton.onClick.AddListener(() =>
            {
                NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_forgetPasswordPanel, false);
                NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_emailSentPanel, false);

                _emailImputField.ReleaseSelection();
                _emailImputField.text = "";
            });
        }
        /// <summary>
        /// Init the Forge Login UI 
        public void Init(System.Action OnLogin, System.Action OnForgetPassword)
        {
            //login
            _loginButton.onClick.AddListener(() =>
            {
                OnLogin?.Invoke();
            });

            //forget password
            _resetPasswordButton.onClick.AddListener(() =>
            {
                OnForgetPassword?.Invoke();
            });
        }

        #region Login
        public void LoginButtonBehaviour(bool state)
        {
            _loginButton.interactable = state;
        }
        #endregion
        #region Forget Password
        public void EnableEmailSentPanel()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_emailSentPanel, true);
        }
        #endregion
        #region Test
        public void TestDataFields(string username, string password)
        {
            _userNameIF.text = username;
            _userPasswordIF.text = password;
        }
        #endregion
    }
}