using UnityEngine;
namespace NFTCreator
{
    [System.Serializable]
    public class NFTSocketAcc
    {
        [SerializeField] private string _name;
        [SerializeField] private Transform _socket;
        [NFTGCO.Helpers.SearchableEnum]
        [SerializeField] private NFTTokenAttributeEnum _tokenAttributeIndex;
        [SerializeField] private NFTGCO.Helpers.UDictionary<long, GameObject> _options;

        public string Name => _name;
        public Transform Socket => _socket;
        public NFTTokenAttributeEnum TokenAttributeIndex => _tokenAttributeIndex;
        public NFTGCO.Helpers.UDictionary<long, GameObject> Options => _options;
    }
}