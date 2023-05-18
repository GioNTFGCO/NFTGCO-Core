using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class TokenDetailsDTO
    {
        public long tokenId;
        public long mintType;
        public long avatarType;
        public string imageUrl;
        public long xp;
        public List<long> tokenAttributes;
    }
}