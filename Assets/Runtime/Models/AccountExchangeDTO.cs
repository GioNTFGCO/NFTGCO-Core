using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace NFTGCO.Models.DTO
{
    [System.Serializable]
    public class AccountExchangeDTO
    {
        public string scope;
        public string access_token;
        public int expires_in;
        public int refresh_expires_in;
        public string refresh_token;
        public string token_type;
        [JsonProperty(PropertyName = "not-before-policy")]
        public int not_before_policy;
        public string session_state;
    }
    
    [System.Serializable]
    public class AccountTokenExchange
    {
        public string jwt;
        public string provider;
        public AccountTokenExchange(string token, string API)
        {
            jwt = token;
            provider = API;
        }
    }
}