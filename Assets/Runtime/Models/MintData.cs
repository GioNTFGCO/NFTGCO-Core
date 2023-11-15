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
        public List<AttributeData> attributes;
        public string data;
        public References references;
        public string uuid;
    }

    [Serializable]
    public class MintDataV2
    {
        public int game_id;
        public string wallet;
        public string collection;
        public string name;
        public string description;
        public string image;
        public string nft_type;
        public List<AttributeData> attributes;
        public string data;
        public References references;
    }
}