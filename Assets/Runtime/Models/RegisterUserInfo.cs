using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCO.Models
{
    [System.Serializable]
    public class RegisterUserInfo
    {
        public string name;
        public string username;
        public string email;
        public string password;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}