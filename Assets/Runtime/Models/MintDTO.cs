using System.Collections.Generic;

namespace Runtime.Models
{
    public class MintDTO
    {
        public int game_id;
        public string wallet;
        public string collection;
        public string name;
        public string description;
        public string image;
        public string nft_type;
        public string account_id;
        public List<Attribute> attributes;
        public string data;
    }
    public class Attribute
    {
        public string trait_type;
        public string value;
    }
}