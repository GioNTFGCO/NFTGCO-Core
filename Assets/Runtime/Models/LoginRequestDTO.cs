using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class LoginRequestDTO
    {
        public string client_id;
        public string username;
        public string password;
        public string grant_type;
    }
}