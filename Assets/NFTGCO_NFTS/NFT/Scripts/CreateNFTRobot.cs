using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NFTGCO;
using NFTGCO.Helpers;
using UnityEngine;

namespace NFTCreator
{
    public class CreateNFTRobot : MonoBehaviour
    {
        private const string ArmorKey = "Armor";
        private const string AccessoryKey = "Accessory";
        private const string AuraKey = "Aura";
        private const int RobotId = 0;

        [SerializeField] private NFTSocket[] _socketsArmor;
        [SerializeField] private NFTSocket[] _socketsAccessories;
        [SerializeField] private NFTSocket _socketsAuras;

        private bool _createOnStart;
        private List<string> _tokenAttributeEnum;
        [SerializeField] [ReadOnly] private UDictionary<string, UDictionary<string, GameObject>> _tokensInRuntime;
        
        private void Awake()
        {
            _tokensInRuntime = new UDictionary<string, UDictionary<string, GameObject>>
            {
                { ArmorKey, new UDictionary<string, GameObject>() },
                { AccessoryKey, new UDictionary<string, GameObject>() },
                { AuraKey, new UDictionary<string, GameObject>() }
            };
        }

        private void Start()
        {
            CatchNFTTokenAttributeEnum();

            if (!NFTGCOStoredManager.Instance.ReceivedArmors)
                return;

            CreateRobotAssets();
        }

        public void CreateRobotAssets(bool generateRandom = false)
        {
            if (NFTGCOStoredManager.Instance.StoredResponse == null)
                return;

            foreach (string attributeName in _tokenAttributeEnum)
            {
                DisableAssetsInRuntime(attributeName);
            }

            for (int i = 0; i < _socketsArmor.Length; i++)
            {
                GenerateRobotAsset(i, _socketsArmor[i].TokenAttributeIndex);
            }

            for (int i = 0; i < _socketsAccessories.Length; i++)
            {
                GenerateRobotAsset(i, _socketsAccessories[i].TokenAttributeIndex);
            }

            //since auras have only one item, we just need to call the index 0
            GenerateRobotAsset(0, _socketsAuras.TokenAttributeIndex);
        }

        /// <summary>
        /// Generate the robot asset of the NFT
        /// </summary>
        /// <param name="indexID">the part of the armor-accessory-aura</param>
        /// <param name="tokenAttributeId">the part of the body that the asset are gonna created</param>
        private void GenerateRobotAsset(int indexID, NFTTokenAttributeEnum tokenAttributeId)
        {
            var tokenFromServerId = NFTGCOStoredManager.Instance.GetStoreByNFTTokenAttribute(tokenAttributeId)
                .ServerTokenAttributes[RobotId];

            GenerateRobotAssets(indexID, tokenAttributeId, tokenFromServerId);
        }

        private void GenerateRobotAssets(int indexID, NFTTokenAttributeEnum tokenAttributeId, long tokenFromServerId)
        {
            if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == ArmorKey)
            {
                CreateAssetWithKey(_socketsArmor[indexID], tokenFromServerId, tokenAttributeId, true);
            }
            else if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == AccessoryKey)
            {
                CreateAssetWithKey(_socketsAccessories[indexID], tokenFromServerId, tokenAttributeId, false);
            }
            else if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == AuraKey)
            {
                CreateAssetWithKey(_socketsAuras, tokenFromServerId, tokenAttributeId, false);
            }
        }
        
        private void CreateAssetWithKey(NFTSocket nftObject, long nftKey, NFTTokenAttributeEnum tokenAttribute, bool worldPositionStays)
        {
            GameObject NFTAsset = GetValueFromNFTSocketAccWithKey(nftObject, nftKey);

            if (NFTAsset != null)
            {
                string tokenKey = $"{tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name}-{tokenAttribute}-{NFTAsset.name}";

                if (!_tokensInRuntime[tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name].ContainsKey(tokenKey))
                {
                    GameObject tokenAsset = Instantiate(NFTAsset, nftObject.Socket, worldPositionStays);

                    _tokensInRuntime[tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name].Add(tokenKey, tokenAsset);
                    tokenAsset.SetActive(true);
                }
                else if (_tokensInRuntime[tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name].ContainsKey(tokenKey))
                {
                    _tokensInRuntime[tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name][tokenKey].SetActive(true);
                }
            }
        }

        private void DisableAssetsInRuntime(string enumAttribute)
        {
            foreach (var item in _tokensInRuntime[enumAttribute])
            {
                item.Value.SetActive(false);
            }
        }

        private void DisableAssetsInRuntimeWithKeyCondition(string enumAttribute, string keyCondition)
        {
            foreach (var item in _tokensInRuntime[enumAttribute])
            {
                if (item.Key.Contains(keyCondition))
                {
                    item.Value.SetActive(false);
                }
            }
        }

        private GameObject GetValueFromNFTSocketAccWithKey(NFTSocket nftsocketacc, long nftKey)
        {
            foreach (KeyValuePair<long, GameObject> TokenAttribute in nftsocketacc.Options)
                if (TokenAttribute.Key == nftKey)
                    return TokenAttribute.Value;
            return null;
        }

        private void CatchNFTTokenAttributeEnum()
        {
            _tokenAttributeEnum = new List<string>();
            foreach (NFTTokenAttributeEnum item in System.Enum.GetValues(typeof(NFTTokenAttributeEnum)))
            {
                string attributeName = item.GetAttribute<NFTTokenAttributeEnumAttribute>().Name;
                if (!_tokenAttributeEnum.Contains(attributeName))
                    _tokenAttributeEnum.Add(attributeName);
            }
        }

        #region Random Creation

        public void GenerateRandomSocketAttribute(NFTTokenAttributeEnum tokenAttributeId)
        {
            DisableAssetsInRuntimeWithKeyCondition(tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name,
                tokenAttributeId.ToString());

            NFTSocket NFTObject = System.Array.Find(_socketsArmor, element => element.TokenAttributeIndex == tokenAttributeId);
            long randomNFTIndex = NFTObject.Options.ElementAt(Random.Range(0, NFTObject.Options.Count)).Key;

            CreateAssetWithKey(NFTObject, randomNFTIndex, tokenAttributeId, true);
        }
        
        public void GenerateRandomAccessoryAsset(NFTTokenAttributeEnum tokenAttributeId)
        {
            DisableAssetsInRuntimeWithKeyCondition(tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name,
                tokenAttributeId.ToString());

            NFTSocket NFTObject = System.Array.Find(_socketsAccessories, element => element.TokenAttributeIndex == tokenAttributeId);
            long randomNFTIndex = NFTObject.Options.ElementAt(Random.Range(0, NFTObject.Options.Count)).Key;

            CreateAssetWithKey(NFTObject, randomNFTIndex, tokenAttributeId, false);
        }

        public void GenerateRandomAuraAsset()
        {
            DisableAssetsInRuntime(AuraKey);
            long randomNFTIndex = _socketsAuras.Options.ElementAt(Random.Range(0, _socketsAuras.Options.Count)).Key;
            CreateAssetWithKey(_socketsAuras, randomNFTIndex, NFTTokenAttributeEnum.Aura_Accessory, false);
        }

        #endregion
    }
}