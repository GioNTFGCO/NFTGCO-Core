using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class RefreshTokenRequestDTO
    {
        public string client_id;
        public string refresh_token;
        public string grant_type;
    }
}