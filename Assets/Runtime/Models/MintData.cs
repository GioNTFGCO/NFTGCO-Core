using System;
using System.Collections.Generic;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class MintData
    {
        public int game_id;
        public string wallet;
        public string collection;
        public string name;
        public string description;
        public string image;
        public string nft_type;
        public string account_id;
        public List<AttributeData> attributes;
        public string data;
    }
}