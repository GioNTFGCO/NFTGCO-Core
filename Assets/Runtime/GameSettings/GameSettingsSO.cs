using System;
using System.Collections;
using System.Collections.Generic;
using NFTGCO.Helpers;
using UnityEngine;
using UnityEngine.Serialization;

namespace NFTGCO
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "NFTGCO/GameSettings", order = 1)]
    public class GameSettingsSO : ScriptableObject
    {
        private static GameSettingsSO _instance;

        public static GameSettingsSO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameSettingsSO>("GameSettings");
                }

                return _instance;
            }
        }

        [Header("Game Version and Build Number")]
        public string GameVersion;
        public int AndroidBundleVersion;
        public int IOSBuildNumber;

        [Space] [Header("Game Environment")]
        public GameEnvironmentEnum GameEnvironmentEnum = GameEnvironmentEnum.Development;

        [Space] [Header("Game Platform")] public GamePlatformEnum GamePlatformEnum = GamePlatformEnum.Android;
        
        [Space] [Header("Game Settings")]
        public int CompanyId = 1;
        public int GameId;

        [Space] [Header("Core Version")] 
        public string CoreVersionID = "2.0.0";
        
        [Space] [Header("Terms and conditions version")] 
        public string TermsAndConditionsVersion = "7/21/2023";
        
        [Space] [Header("Google Settings")]
        public string GoogleWebClientId = "googleWebClientId";
        
        public string GetGameEnvironment => GameEnvironmentEnum.GetAttribute<GameEnvironmentAttribute>().Uri;
    }
}