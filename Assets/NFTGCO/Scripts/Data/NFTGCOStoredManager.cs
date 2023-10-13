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
        
        private List<TokenDetailsDTO> _storedResponse;
        private AvatarDataDTO _avatarData;
        private List<NFTGCOStored> _serverSocketsAccessories;
        
        private bool _receivedArmors;
        
        public GameStateDTO GameState => _gameState;
        public AccountDto AccountDTOResponse => _accountDTOResponse;
        public List<TokenDetailsDTO> StoredResponse => _storedResponse;
        public bool ReceivedArmors => _receivedArmors;

        public void SetGameStateDTO(GameStateDTO newGameStateDTO) => _gameState = newGameStateDTO;

        public void SetAccountDTOResponse(AccountDto newAccountDto)
        {
            _accountDTOResponse = newAccountDto;
            NFTGCOConfig.Instance.SetUserAccountId(newAccountDto.id.ToString(), newAccountDto.userId);
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
                _serverSocketsAccessories = new List<NFTGCOStored>();
                _storedResponse = new List<TokenDetailsDTO>();

                for (int i = 0; i < response.Count; i++)
                {
                    _storedResponse.Add(response[i]);

                    if (response[i].tokenAttributes.Count == TOTAL_TOKEN_ATTRIBUTES)
                    {
                        for (int totalTokenAttributes = 0; totalTokenAttributes < TOTAL_TOKEN_ATTRIBUTES; totalTokenAttributes++)
                        {
                            NFTGCOStored cachedNftgcoStored = _serverSocketsAccessories.Find(x =>
                                x.NFTTokenAttribute == (NFTTokenAttributeEnum)totalTokenAttributes);
                            if (cachedNftgcoStored == null)
                            {
                                NFTGCOStored nftgcoStored = new NFTGCOStored((NFTTokenAttributeEnum)totalTokenAttributes);
                                _serverSocketsAccessories.Add(nftgcoStored);
                                _serverSocketsAccessories[totalTokenAttributes].AddNewServerTokeAttribute(response[i].tokenAttributes[totalTokenAttributes]);
                            }
                            else
                            {
                                _serverSocketsAccessories[totalTokenAttributes].AddNewServerTokeAttribute(response[i].tokenAttributes[totalTokenAttributes]);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("[FORGE]Token attributes count dont match anymore, token count is:" +
                                       response[i].tokenAttributes.Count);
                    }
                }

                _receivedArmors = true;
            }
        }

        public NFTGCOStored GetStoreByNFTTokenAttribute(NFTTokenAttributeEnum nfttoken)
        {
            return _serverSocketsAccessories.Find(x => x.NFTTokenAttribute == nfttoken) != null
                ? _serverSocketsAccessories.Find(x => x.NFTTokenAttribute == nfttoken)
                : null;
        }
        
        public long GetServerTokenByIndex(NFTTokenAttributeEnum nfttoken, int index)
        {
            return _serverSocketsAccessories.Find(x => x.NFTTokenAttribute == nfttoken) != null
                ? _serverSocketsAccessories.Find(x => x.NFTTokenAttribute == nfttoken).ServerTokenAttributes[index]
                : 0;
        }

        public void ClearData()
        {
            _gameState = null;
            _accountDTOResponse = null;

            _storedResponse = new List<TokenDetailsDTO>();
            _serverSocketsAccessories = new List<NFTGCOStored>();

            _receivedArmors = false;
        }
    }
}