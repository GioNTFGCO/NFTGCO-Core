using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class NFTAttributesResponse
    {
        public List<long> Attributes;
        public long NftType;
    }
}