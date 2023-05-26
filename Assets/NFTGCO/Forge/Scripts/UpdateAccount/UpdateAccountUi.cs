using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class UpdateAccountUi : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _panelCanvasGroup;
        [SerializeField] private TextMeshProUGUI _oldUsernameText;
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Button _closePanel;

        public void Init(System.Action OnUpdate, System.Action OnClose)
        {
            _updateButton.onClick.AddListener(()=> OnUpdate?.Invoke());
            _closePanel.onClick.AddListener(() =>
            {
                OnClose?.Invoke();
                ClosePanel();
            });
            _closePanel.gameObject.SetActive(false);
        }
        public string GetUsername()
        {
            return _usernameInputField.text;
        }
        public bool IsUsernameEmpty()
        {
            return _usernameInputField.text == "";
        }
        public bool IsUsernameValid()
        {
            return _usernameInputField.text.Length >= 3;
        }

        public void ShowCurrentUsername(string currentUsername)
        {
            _oldUsernameText.text = currentUsername;
        }
        public void ShowPanel()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_panelCanvasGroup,true);
            _usernameInputField.text = String.Empty;
        }
        public void ClosePanel()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_panelCanvasGroup,false);
            _usernameInputField.text = String.Empty; 
        }
    }
}