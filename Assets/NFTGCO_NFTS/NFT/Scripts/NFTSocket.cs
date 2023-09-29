using UnityEngine;
using UnityEngine.Serialization;

namespace NFTCreator
{
    [System.Serializable]
    public class NFTSocket
    {
        [SerializeField] private string name;
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTTokenAttributeEnum _tokenAttributeIndex;
        [SerializeField] private Transform _socket;

        [FormerlySerializedAs("options")] public GameObject[] Options;
        public Transform Socket => _socket;
        public NFTTokenAttributeEnum TokenAttributeIndex => _tokenAttributeIndex;
    }
}