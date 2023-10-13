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
        public string game_uuid;
        public References references;
        public List<AttributeData> attributes;
        public string data;
    }
}