using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NFTCreator;
using NFTGCO.API;
using NFTGCO.Helpers;
using NFTGCO.Models.DTO;
using UnityEngine;

namespace NFTGCO
{
    public class NFTGCOStoredManager : NFTGCO.Helpers.Singleton<NFTGCOStoredManager>
    {
        private const int TOTAL_TOKEN_ATTRIBUTES = 15;

        private GameStateDTO _gameState;

        [Tooltip("head-leftArm-rightArm-torso-waist-leftLef-rightLeg-background")]
        private AccountDto _accountDTOResponse;

        private long _currentNFTXp;
        private List<TokenDetailsDTO> _storedResponse;
        private AvatarDataDTO _avatarData;
        private List<NFTGCOStored> _serverSocketsAccesories;
        private long[] _robotXPAtt;

        [Tooltip("0 - Genesis NFT, 1 - Recycled NFT")]
        private long[] _mintTypeAtt;

        [Tooltip("avatarType: any number but right now we only use 1 which I think it means Robot")]
        private long[] _avatarTypeAtt;

        [Tooltip("tokenId is the id from blockchain")]
        private long[] _tokenIdAtt;

        private bool _receivedArmors;

        //social
        private string _socialName;

        public GameStateDTO GameState => _gameState;
        public AccountDto AccountDTOResponse => _accountDTOResponse;
        public List<TokenDetailsDTO> StoredResponse => _storedResponse;
        public long CurrentNFTXp => _currentNFTXp;
        public long[] RobotXPAtt => _robotXPAtt;
        public long[] MintTypeAtt => _mintTypeAtt;
        public long[] AvatarTypeAtt => _avatarTypeAtt;
        public long[] TokenIdAtt => _tokenIdAtt;
        public bool ReceivedArmors => _receivedArmors;
        public string SocialName => _socialName;

        public void SetGameStateDTO(GameStateDTO newGameStateDTO) => _gameState = newGameStateDTO;

        public void SetAccountDTOResponse(AccountDto newAccountDto)
        {
            _accountDTOResponse = newAccountDto;
            NFTGCOConfig.Instance.SetUserAccountId(newAccountDto.id.ToString(), newAccountDto.userId);
        }

        public void SetCurrentNFTXp(long newNFTXp) => _currentNFTXp = newNFTXp;

        //social
        public void SetSocialName(string newSocialName)
        {
            _socialName = newSocialName;
        }

        public void GetLastAvatar(AvatarDataDTO response)
        {
            _avatarData = response;
        }

        public void RetrieveRobotParts(List<TokenDetailsDTO> response)
        {
            _receivedArmors = false;
            if (response.Count > 0)
            {
                _serverSocketsAccesories = new List<NFTGCOStored>();
                _robotXPAtt = new long[response.Count];
                _mintTypeAtt = new long[response.Count];
                _avatarTypeAtt = new long[response.Count];
                _tokenIdAtt = new long[response.Count];
                _storedResponse = new List<TokenDetailsDTO>();

                for (int i = 0; i < response.Count; i++)
                {
                    _storedResponse.Add(response[i]);

                    if (response[i].tokenAttributes.Count == TOTAL_TOKEN_ATTRIBUTES)
                    {
                        for (int totalTokenAttributes = 0;
                             totalTokenAttributes < TOTAL_TOKEN_ATTRIBUTES;
                             totalTokenAttributes++)
                        {
                            NFTGCOStored cachedNftgcoStored = _serverSocketsAccesories.Find(x =>
                                x.NFTTokenAttribute == (NFTTokenAttributeEnum)totalTokenAttributes);
                            if (cachedNftgcoStored == null)
                            {
                                NFTGCOStored nftgcoStored =
                                    new NFTGCOStored((NFTTokenAttributeEnum)totalTokenAttributes);
                                _serverSocketsAccesories.Add(nftgcoStored);
                                _serverSocketsAccesories[totalTokenAttributes]
                                    .AddNewServerTokeAttribute(response[i].tokenAttributes[totalTokenAttributes]);
                            }
                            else
                            {
                                _serverSocketsAccesories[totalTokenAttributes]
                                    .AddNewServerTokeAttribute(response[i].tokenAttributes[totalTokenAttributes]);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("[FORGE]Token attributes count dont match anymore, token count is:" +
                                       response[i].tokenAttributes.Count);
                    }

                    _robotXPAtt[i] = response[i].xp;
                    _mintTypeAtt[i] = response[i].mintType;
                    _avatarTypeAtt[i] = response[i].avatarType;
                    _tokenIdAtt[i] = response[i].tokenId;
                }

                _receivedArmors = true;
            }
        }

        public NFTGCOStored GetForgeStoreByNFTTokenAttribute(NFTTokenAttributeEnum nfttoken)
        {
            return _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken) != null
                ? _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken)
                : null;
        }

        public long GetRandomServerTokenAttributes(NFTTokenAttributeEnum nfttoken)
        {
            return _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken) != null
                ? _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken).ServerTokenAttributes[
                    Random.Range(0,
                        _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken).ServerTokenAttributes
                            .Count)]
                : 0;
        }

        public long GetServerTokenByIndex(NFTTokenAttributeEnum nfttoken, int index)
        {
            return _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken) != null
                ? _serverSocketsAccesories.Find(x => x.NFTTokenAttribute == nfttoken).ServerTokenAttributes[index]
                : 0;
        }

        public void ClearData()
        {
            _gameState = null;
            _accountDTOResponse = null;
            _currentNFTXp = 0;

            _storedResponse = new List<TokenDetailsDTO>();
            _serverSocketsAccesories = new List<NFTGCOStored>();

            _robotXPAtt = null;
            _mintTypeAtt = null;
            _avatarTypeAtt = null;
            _tokenIdAtt = null;

            _receivedArmors = false;
        }
    }
}