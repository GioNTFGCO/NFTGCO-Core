using System.Collections.Generic;
using System;
using System.Runtime.Serialization;

namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class CreateGameEventDTO
    {
        public string accountId;
        public long gameId;
        [DataMember(Name = "event")]
        public Dictionary<string, object> @event;
    }
}