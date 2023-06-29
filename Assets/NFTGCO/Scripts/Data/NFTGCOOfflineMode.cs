using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using NFTGCO.API;
using NFTGCO.Helpers;
using NFTGCO.Models.DTO;
using SceneField.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCO
{
    public class NFTGCOOfflineMode : MonoBehaviour
    {
        private const string FakeGameState = "{\"id\":0,\"userId\":\"\",\"gameId\":0}";

        private const string FakeAccount =
            "{\"id\":1132,\"name\":\"Gio\",\"username\":\"giodev\",\"email\":\"giovanni@nftgco.com\",\"walletAddress\":\"0x7dea016e90c06c0dc8749fffcfdfcf8da017331b\",\"userId\":\"b80e84cb-42fa-4c85-9b37-be3e6a464674\",\"admin\":\"false\",\"enabled\":\"false\",\"totalXp\":158027,\"spentXp\":0}";

        private const string FakeAvatarData =
            "{\"id\":1471,\"accountId\":1132,\"ownerAddress\":\"\",\"mintType\":\"GENESIS\",\"tokenId\":0,\"avatarTypeId\":1,\"mintStatus\":\"PENDING\",\"tokenAttributes\":[7,3,2,5,1,0,4,10022,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807]}";

        private const string FakeStoredAvatar =
            "{\"tokenId\":1471,\"mintType\":0,\"avatarType\":0,\"imageUrl\":\"\",\"xp\":0,\"tokenAttributes\":[7,3,2,5,1,0,4,10022,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807,9223372036854775807]}";

        [SerializeField] private SceneLoaderController _sceneLoaderController;

        [Space] public InspectorButton initWithFakeDataButton = new InspectorButton("InitWithFakeData");

        public void InitWithFakeData()
        {
            NFTGCOConfig.Instance.SetLoginOfflineMode();
            
            GameStateDTO gameStateDto = JsonConvert.DeserializeObject<GameStateDTO>(FakeGameState);
            NFTGCOStoredManager.Instance.SetGameStateDTO(gameStateDto);

            AccountDto accountDto = JsonConvert.DeserializeObject<AccountDto>(FakeAccount);
            NFTGCOStoredManager.Instance.SetAccountDTOResponse(accountDto);

            AvatarDataDTO avatarData = JsonUtility.FromJson<AvatarDataDTO>(FakeAvatarData);
            NFTGCOStoredManager.Instance.GetLastAvatar(avatarData);

            TokenDetailsDTO tokenDetails = JsonUtility.FromJson<TokenDetailsDTO>(FakeStoredAvatar);
            List<TokenDetailsDTO> tokenDetailsList = new List<TokenDetailsDTO>();
            tokenDetailsList.Add(tokenDetails);
            NFTGCOStoredManager.Instance.RetrieveRobotParts(tokenDetailsList);

            _sceneLoaderController.StartLevel();
        }
    }
}