using System.Collections.Generic;
using System;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class GameEventDTO
    {
        public long id;
        public string userId;
        public long gameId;
        public Dictionary<string, object> @event;
    }
}