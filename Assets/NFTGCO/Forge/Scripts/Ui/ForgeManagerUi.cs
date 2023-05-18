using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Forge
{
    public class ForgeManagerUi : MonoBehaviour
    {
        [SerializeField] private UDictionary<string, CanvasGroup> _panels;
        [SerializeField] private UDictionary<string, Button> _loginButtons;
        [SerializeField] private CanvasGroup _nftgcoLoginPanel;
        [SerializeField] private Button _openRegisterButton;

        private void Start()
        {
            CompilationButtons();
            DelegateButtons();

            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_nftgcoLoginPanel, false);
            _openRegisterButton.gameObject.SetActive(false);
#if UNITY_EDITOR
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_nftgcoLoginPanel, true);
            _openRegisterButton.gameObject.SetActive(true);
#endif
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
        /// <param name="OnLogin">Events to add</param>
        public void DelegateButtonLoginCallback(string buttonId, System.Action OnLogin)
        {
            _loginButtons[buttonId].onClick.AddListener(() => OnLogin?.Invoke());
        }
        /// <summary>
        /// Delegate events to Register with NFTGCO API Button
        /// </summary>
        /// <param name="OnRegister">Events to add</param>
        public void DelegateButtonRegisterCallback(System.Action OnRegister)
        {
            _openRegisterButton.onClick.AddListener(() => OnRegister?.Invoke());
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
        /// <summary>
        /// Show or Hide the Block Panel
        /// </summary>
        /// <param name="state"></param>
        public void ShowHideBlockPanel(bool state)
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_panels["BLOCK"], state);
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