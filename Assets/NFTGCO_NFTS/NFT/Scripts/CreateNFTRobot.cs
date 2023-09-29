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
        private const string ARMOR_KEY = "Armor";
        private const string ACCESSORY_KEY = "Accessory";
        private const string AURA_KEY = "Aura";


        [SerializeField] private NFTSocket[] _sockets;
        [SerializeField] private NFTSocketAcc[] _socketsAccessories;
        [SerializeField] private NFTSocketAcc _socketsAuras;

        private int _robotId;
        private bool _createOnStart;
        private Dictionary<string, Dictionary<string, GameObject>> _tokensInRuntime;
        private int _serverTokensCount;
        private int[] _syncSocketIds;
        private long[] _syncOptionIds;
        private List<string> _tokenAttributeEnum;

        public int RobotId
        {
            get => _robotId;
            set => _robotId = value;
        }

        private void Awake()
        {
            _robotId = NFTGCOGlobalData.Instance.NFTRobotID;

            _tokensInRuntime = new Dictionary<string, Dictionary<string, GameObject>>
            {
                { ARMOR_KEY, new Dictionary<string, GameObject>() },
                { ACCESSORY_KEY, new Dictionary<string, GameObject>() },
                { AURA_KEY, new Dictionary<string, GameObject>() }
            };
        }

        private void Start()
        {
            CatchNFTTokenAttributeEnum();

            if (!NFTGCOStoredManager.Instance.ReceivedArmors)
                return;

            _serverTokensCount = _sockets.Length + _socketsAccessories.Length + _socketsAuras.Options.Count;
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

            for (int i = 0; i < _sockets.Length; i++)
            {
                int index = i;
                GenerateRobotAsset(index, _sockets[index].TokenAttributeIndex);
            }

            for (int i = 0; i < _socketsAccessories.Length; i++)
            {
                int index = i;
                GenerateRobotAsset(index, _socketsAccessories[index].TokenAttributeIndex);
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
            var tokenFromServerId = NFTGCOStoredManager.Instance.GetStoreByNFTTokenAttribute(tokenAttributeId).ServerTokenAttributes[_robotId];

            GenerateRobotAssets(indexID, tokenAttributeId, tokenFromServerId);
        }

        private void GenerateRobotAssets(int indexID, NFTTokenAttributeEnum tokenAttributeId, long tokenFromServerId)
        {
            if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == ARMOR_KEY)
            {
                CreateAssetWithIndex(indexID, tokenFromServerId, ARMOR_KEY);
            }
            else if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == ACCESSORY_KEY)
            {
                CreateAssetWithKey(_socketsAccessories[indexID], tokenFromServerId, tokenAttributeId);
            }
            else if (tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name == AURA_KEY)
            {
                CreateAssetWithKey(_socketsAuras, tokenFromServerId, tokenAttributeId);
            }
        }

        private void CreateAssetWithIndex(int socketId, long accessoryId, string keyName)
        {
            if (_sockets[socketId].Options[accessoryId] == null)
                return;

            string tokenKey = $"{keyName}-{_sockets[socketId].Options[accessoryId].name}";

            if (!_tokensInRuntime[keyName].ContainsKey(tokenKey))
            {
                GameObject tokenAsset = Instantiate(_sockets[socketId].Options[accessoryId], _sockets[socketId].Socket, true);
                _tokensInRuntime[keyName].Add(tokenKey, tokenAsset);
                tokenAsset.SetActive(true);
            }
            else
            {
                _tokensInRuntime[keyName][tokenKey].SetActive(true);
            }
        }

        private void CreateAssetWithKey(NFTSocketAcc nftObject, long nftKey, NFTTokenAttributeEnum tokenAttribute)
        {
            GameObject NFTAsset = GetValueFromNFTSocketAccWithKey(nftObject, nftKey);

            if (NFTAsset != null)
            {
                string tokenKey = $"{tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name}-{tokenAttribute}-{NFTAsset.name}";

                if (!_tokensInRuntime[tokenAttribute.GetAttribute<NFTTokenAttributeEnumAttribute>().Name].ContainsKey(tokenKey))
                {
                    GameObject tokenAsset = Instantiate(NFTAsset, nftObject.Socket, false);

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

        private GameObject GetValueFromNFTSocketAccWithKey(NFTSocketAcc nftsocketacc, long nftKey)
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
            DisableAssetsInRuntime(tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name);

            NFTSocket NFTObject = System.Array.Find(_sockets, element => element.TokenAttributeIndex == tokenAttributeId);

            int randomSocketIndex = Random.Range(0, NFTObject.Options.Length);

            Debug.Log($" SocketId {(int)tokenAttributeId} - Random socket index: {randomSocketIndex} - KeyName {tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name}");

            CreateAssetWithIndex((int)tokenAttributeId, randomSocketIndex, tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name);
        }


        public void GenerateRandomAccessoryAsset(NFTTokenAttributeEnum tokenAttributeId)
        {
            DisableAssetsInRuntimeWithKeyCondition(tokenAttributeId.GetAttribute<NFTTokenAttributeEnumAttribute>().Name, tokenAttributeId.ToString());

            NFTSocketAcc NFTObject = System.Array.Find(_socketsAccessories, element => element.TokenAttributeIndex == tokenAttributeId);
            long randomNFTIndex = NFTObject.Options.ElementAt(Random.Range(0, NFTObject.Options.Count)).Key;

            CreateAssetWithKey(NFTObject, randomNFTIndex, tokenAttributeId);
        }

        public void GenerateRandomAuraAsset()
        {
            DisableAssetsInRuntime(AURA_KEY);
            long randomNFTIndex = _socketsAuras.Options.ElementAt(Random.Range(0, _socketsAuras.Options.Count)).Key;
            CreateAssetWithKey(_socketsAuras, randomNFTIndex, NFTTokenAttributeEnum.Aura_Accessory);
        }

        #endregion
    }
}