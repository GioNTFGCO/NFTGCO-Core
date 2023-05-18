using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Tables;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using TMPro;
using System;

namespace NFTGCO.Localization
{
    public class TranslatorLocalization : MonoBehaviour
    {
        [SerializeField] private LocalizationManager _localizationManager;
        [SerializeField] private string _key;

        private TextMeshProUGUI _text;

        private void OnEnable()
        {
            _localizationManager.LST.TableChanged += TableChanged;
        }
        private void OnDisable()
        {
            _localizationManager.LST.TableChanged -= TableChanged;
        }

        private void TableChanged(StringTable stringTable)
        {
            if (_text)
            {
                TranslateTextMeshProUGUI(stringTable);
            }
        }

        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            if (TryGetComponent<TextMeshProUGUI>(out _text))
            {
                TranslateTextMeshProUGUI(_localizationManager.StringTable);
            }
        }

        public void Init(LocalizationManager localizationManager, string textKey)
        {
            _localizationManager = localizationManager;
            _key = textKey;
        }

        private void TranslateTextMeshProUGUI(StringTable stringTable)
        {
            StringTableEntry entry = stringTable[_key];

            if (entry != null)
                _text.text = entry.LocalizedValue;
            else
                Debug.LogWarning($"No {stringTable.LocaleIdentifier.Code} translation for key: '{_key}'");
        }
    }
}