using System;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class AccountDto
    {
        public string id;
        public string name;
        public string username;
        public string email;
        public string walletAddress;
        public string userId;
        public string admin;
        public string enabled;
    }
}