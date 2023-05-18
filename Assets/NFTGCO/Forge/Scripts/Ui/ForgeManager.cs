using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Forge
{
    public class ForgeManager : MonoBehaviour
    {
        [SerializeField] private ForgeManagerUi _forgeManagerUi;
        [Tooltip("Login / Register")]
        [SerializeField] private string _initialPanelId;
        // Start is called before the first frame update
        void Start()
        {
            _forgeManagerUi.Init(_initialPanelId);
        }
    }
}