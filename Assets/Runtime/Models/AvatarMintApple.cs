using System;
using System.Collections.Generic;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class AvatarMintApple
    {
        public string transactionId;
        public List<MintData> data = new();
    }
}