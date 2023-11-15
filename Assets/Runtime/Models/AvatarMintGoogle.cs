using System;
using System.Collections.Generic;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class AvatarMintGoogle
    {
        public string payment_token;
        public List<MintData> data = new();
    }
}