using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Forge
{
    public class ForgeRegisterUi : MonoBehaviour
    {
        [SerializeField] private NFTGCO.Helpers.UDictionary<string, TMP_InputField> _formInputFields;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _cancelRegisterButton;
        [SerializeField] private Button _registerUserButton;
        [SerializeField] private CanvasGroup _registrationConfirmation;
        [SerializeField] private Button _registrationDoneButton;

        private Dictionary<string, string> _userFormData;
        private bool _inputfieldsFilled;

        public Dictionary<string, string> UserFormData => _userFormData;
        public bool InputFieldsFilled => _inputfieldsFilled;

        private void Start()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_registrationConfirmation, false);
        }

        public void Init(System.Action OnRegister, System.Action OnCloseRegister)
        {
            _formInputFields["email"].contentType = TMP_InputField.ContentType.EmailAddress;
            _formInputFields["password"].contentType = TMP_InputField.ContentType.Password;
            _formInputFields["repeatPassword"].contentType = TMP_InputField.ContentType.Password;

            _registerUserButton.onClick.AddListener(() =>
            {
                OnRegister?.Invoke();
            });

            _cancelRegisterButton.onClick.AddListener(() => OnCloseRegister?.Invoke());
            _registrationDoneButton.onClick.AddListener(() => OnCloseRegister?.Invoke());
        }
        public void TestForm(string name, string username, string email, string password)
        {
            _formInputFields["name"].text = name;
            _formInputFields["username"].text = username;
            _formInputFields["email"].text = email;
            _formInputFields["password"].text = password;
            _formInputFields["repeatPassword"].text = password;
        }
        public void GetFormData()
        {
            _userFormData = new Dictionary<string, string>
        {
            {"name",_formInputFields["name"].text},
            {"username",_formInputFields["username"].text},
            {"email",_formInputFields["email"].text},
            {"password",_formInputFields["password"].text},
            {"repeatPassword",_formInputFields["password"].text}
        };
        }

        public void CheckInputfieldsFilled()
        {
            foreach (var item in _formInputFields)
            {
                _inputfieldsFilled = CheckInputfieldIsEmpty(item.Key) ? false : true;
            }
        }
        private bool CheckInputfieldIsEmpty(string imputKey)
        {
            return string.IsNullOrEmpty(_formInputFields[imputKey].text);
        }
        public bool CheckSamePassword()
        {
            return _formInputFields["password"].text == _formInputFields["repeatPassword"].text ? true : false;
        }
        public void SetMessage(string newText)
        {
            _messageText.text = newText;
        }
        public void RegisterButtonBehaviour(bool state)
        {
            _registerUserButton.interactable = state;
        }
        public void ClearFields()
        {
            foreach (var item in _formInputFields)
            {
                item.Value.text = "";
            }
        }
        public void EnableRegistrationConfirmation()
        {
            NFTGCO.Helpers.NFTGCOHelpers.CanvasGroupBehaviour(_registrationConfirmation, true);
        }
    }
}