using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace NFTGCO.Localization
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField] private LocalizedStringTable _localizedStringTable;

        private StringTable _stringTable;

        public LocalizedStringTable LST 
        {
            get => _localizedStringTable;
            set => _localizedStringTable = value;
        }
        public StringTable StringTable => _stringTable;

        void OnEnable()
        {
            _localizedStringTable.TableChanged += OnTableChanged;
        }
        void OnDisable()
        {
            _localizedStringTable.TableChanged -= OnTableChanged;
        }
        private void OnTableChanged(StringTable value)
        {
            var op = _localizedStringTable.GetTableAsync();
            if (op.IsDone)
            {
                OnTableLoaded(op);
            }
            else
            {
                op.Completed -= OnTableLoaded;
                op.Completed += OnTableLoaded;
            }
        }

        private void OnTableLoaded(AsyncOperationHandle<StringTable> op)
        {
            _stringTable = op.Result;
        }
    }
}