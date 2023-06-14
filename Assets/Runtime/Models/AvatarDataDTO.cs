using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCO.Models.DTO
{
    [System.Serializable]
    public class AvatarDataDTO
    {
        public long id;
        public long accountId;
        public string ownerAddress;
        public string mintType;
        public long tokenId;
        public long avatarTypeId;
        public string mintStatus;
        public List<long> tokenAttributes;
    }
}