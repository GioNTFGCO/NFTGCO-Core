using UnityEngine;
namespace NFTCreator
{
    [System.Serializable]
    public class NFTSocket
    {
        [SerializeField] private string name;
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTTokenAttributeEnum _tokenAttributeIndex;
        [SerializeField] private Transform _socket;

        public GameObject[] options;
        public Transform Socket => _socket;
        public NFTTokenAttributeEnum TokenAttributeIndex => _tokenAttributeIndex;
    }
}