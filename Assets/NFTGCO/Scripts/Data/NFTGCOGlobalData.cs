using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NFTGCO
{
    public class NFTGCOGlobalData : NFTGCO.Helpers.Singleton<NFTGCOGlobalData>
    {
        private int _nftRobotID;//"RobotId"
        private float _totalXp;//"TotalXp"

        public int NFTRobotID => _nftRobotID;
        public float TotalXp => _totalXp;

        public void SetNFTRobotID(int newRobotId)
        {
            _nftRobotID = newRobotId;
        }
        public void SetTotalXP(float newtotalXp)
        {
            _totalXp = newtotalXp;
        }
    }
}