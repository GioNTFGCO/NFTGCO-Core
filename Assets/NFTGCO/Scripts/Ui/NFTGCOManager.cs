using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class NFTGCOManager : MonoBehaviour
    {
        [SerializeField] private NFTGCOManagerUi nftgcoManagerUi;

        [Tooltip("Login / Register")] [SerializeField]
        private string _initialPanelId;

        // Start is called before the first frame update
        void Start()
        {
            nftgcoManagerUi.Init(_initialPanelId);
        }
    }
}