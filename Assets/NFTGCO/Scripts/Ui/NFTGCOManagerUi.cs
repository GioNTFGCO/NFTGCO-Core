using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace NFTGCO
{
    public class NFTGCOManagerUi : MonoBehaviour
    {
        [SerializeField] private UDictionary<string, CanvasGroup> _panels;
        [SerializeField] private UDictionary<string, Button> _loginButtons;
        [SerializeField] private CanvasGroup _nftgcoLoginPanel;
        [SerializeField] private Button _openRegisterButton;
        [SerializeField] private Button _quitGameButton;

        private void Start()
        {
            CompilationButtons();
            DelegateButtons();

            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_nftgcoLoginPanel, false);
            _openRegisterButton.gameObject.SetActive(false);

#if UNITY_EDITOR
            EnableLoginForm();
#endif
        }

        public void EnableLoginForm()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_nftgcoLoginPanel, true);
        }

        /// <summary>
        /// Init the Forge Manager UI
        /// </summary>
        /// <param name="panelId"></param>
        public void Init(string panelId)
        {
            ShowPanel(panelId);
        }

        /// <summary>
        /// Delegate events to Login buttons
        /// </summary>
        /// <param name="buttonId">The id of the button in the UDictionary</param>
        /// <param name="onLogin">Events to add</param>
        public void DelegateButtonLoginCallback(string buttonId, Action onLogin)
        {
            _loginButtons[buttonId].onClick.AddListener(() => onLogin?.Invoke());
        }

        public void DelegateButtonRegisterCallback(Action onRegister)
        {
            _openRegisterButton.onClick.AddListener(() => onRegister?.Invoke());
        }
        
        public void DelegateButtonQuitGameCallback(Action onQuit)
        {
            _quitGameButton.onClick.AddListener(() => onQuit?.Invoke());
        }

        /// <summary>
        /// Open one panel
        /// </summary>
        /// <param name="panelId"></param>
        public void ShowPanel(string panelId)
        {
            foreach (var item in _panels)
            {
                NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(item.Value, false);
            }

            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_panels[panelId], true);
        }

        public void ShowRegistrationUnablePanel()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_panels["Registration"], true);
        }

        private void DelegateButtons()
        {
            //login
            //DelegateButtonLoginCallback("NFTGCO", () => OpenOnePanel("NFTGCO_Login"));

            //register
            DelegateButtonRegisterCallback(() => ShowPanel("NFTGCO_Register"));
        }

        /// <summary>
        /// Compilation buttons for the platform
        /// </summary>
        private void CompilationButtons()
        {
            foreach (var item in _loginButtons)
            {
                item.Value.gameObject.SetActive(false);
            }
#if UNITY_EDITOR
            foreach (var item in _loginButtons)
            {
                item.Value.gameObject.SetActive(true);
            }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
            //_loginButtons["NFTGCO"].gameObject.SetActive(true);
            _loginButtons["GOOGLE"].gameObject.SetActive(true);
#endif
#if UNITY_IOS && !UNITY_EDITOR
            //_loginButtons["NFTGCO"].gameObject.SetActive(true);
            _loginButtons["IOS"].gameObject.SetActive(true);
#endif
        }
    }
}