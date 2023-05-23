using System.Collections.Generic;
using System;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class CreateGameStateDTO
    {
        public string accountId;
        public long gameId;
        public Dictionary<string, object> state;
    }
}