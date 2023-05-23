using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTGCO
{
    public class UpdateAccountUi : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nicknameInputField;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Button _closePanel;

        public string GetNickName()
        {
            return _nicknameInputField.text;
        }

        public bool IsNicknameEmpty()
        {
            return _nicknameInputField.text == "";
        }
        public bool IsNicknameValid()
        {
            return _nicknameInputField.text.Length >= 3;
        }
    }
}