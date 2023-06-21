using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NFTGCO
{
    public class UiMessage : MonoBehaviour
    {
        public static System.Action<string> OnMessageSent;

        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private float _messageTime = 2f;

        private Coroutine _coroutine;
        private WaitForSeconds _waitTime;

        private void OnEnable()
        {
            OnMessageSent += SetMessage;
        }
        private void OnDisable()
        {
            OnMessageSent -= SetMessage;
        }

        private void Start()
        {
            if (_messageText)
                _messageText.text = "";

            _waitTime = new WaitForSeconds(_messageTime);
        }
        private void SetMessage(string message)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(IESetMessage(message, _messageTime));
            }
            else
            {
                _coroutine = StartCoroutine(IESetMessage(message, _messageTime));
            }
        }
        
        private IEnumerator IESetMessage(string message, float time)
        {
            if (_messageText)
                _messageText.text = $"{message}";

            Debug.Log(message);

            yield return _waitTime;

            if (_messageText)
                _messageText.text = "";
        }
    }
}