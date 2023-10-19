using System.Collections.Generic;
using NFTGCO.Models.DTO;

namespace NFTGCO.Models.DTO
{
    public class AvatarMintServer
    {
        public string payment_token;
        public List<MintData> data = new List<MintData>();
    }
}