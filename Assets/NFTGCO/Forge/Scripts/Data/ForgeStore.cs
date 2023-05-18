using System.Collections;
using System.Collections.Generic;
using NFTCreator;
using UnityEngine;
namespace Forge
{
    [System.Serializable]
    public class ForgeStore
    {
        private NFTTokenAttributeEnum _nftTokenAttribute;
        private List<long> _serverTokenAttributes;

        public NFTTokenAttributeEnum NFTTokenAttribute => _nftTokenAttribute;
        public List<long> ServerTokenAttributes => _serverTokenAttributes;

        public ForgeStore(NFTTokenAttributeEnum tokenEnum)//, List<long> serverToken)
        {
            _nftTokenAttribute = tokenEnum;
            _serverTokenAttributes = new List<long>();//serverToken;
        }
        public void AddNewServerTokeAttribute(long serverToken)
        {
            _serverTokenAttributes.Add(serverToken);
        }
    }
}