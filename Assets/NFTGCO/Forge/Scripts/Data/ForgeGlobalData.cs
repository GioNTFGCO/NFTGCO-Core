using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Forge
{
    public class ForgeGlobalData : NFTGCO.Helpers.Singleton<ForgeGlobalData>
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