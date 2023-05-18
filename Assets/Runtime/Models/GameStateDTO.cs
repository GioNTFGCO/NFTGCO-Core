using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;
namespace NFTGCO.Models.DTO
{
    [Serializable]
    public class GameStateDTO
    {
        public long id;
        public string userId;
        public long gameId;
        public Dictionary<string, object> state;
    }
}