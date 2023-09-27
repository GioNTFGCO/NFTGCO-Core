using System;
using System.Collections.Generic;


namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class NFTData
    {
        public string id;
        public int game_id;
        public string wallet;
        public string collection;
        public string nft_type;
        public string token_id;
        public string name;
        public string description;
        public string image;
        public List<AttributeData> attributes;
        public string data;
        public string account_id;
        public string state;
        public References references;
    }
}