using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class AuthResponseDTO
    {
        public string access_token;
        public string refresh_token;
        public int expires_in;
        public int refresh_expires_in;
        public string token_type;
        public string session_state;
        public string scope;
    }
}