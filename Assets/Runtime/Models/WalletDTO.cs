using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class WalletDTO
    {
        public string address;
        public string message;
        public string signature;
    }
}