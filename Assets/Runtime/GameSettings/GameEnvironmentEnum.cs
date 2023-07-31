using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace NFTGCO
{
    public enum GameEnvironmentEnum
    {
        [GameEnvironmentAttribute("https://dev.gaxos99.com")]
        Development,

        [GameEnvironmentAttribute("https://prod.gaxos.com")]
        Production
    }

    public class GameEnvironmentAttribute : Attribute
    {
        public string Uri { get; private set; }

        public GameEnvironmentAttribute(string uri)
        {
            Uri = uri;
        }
    }
}