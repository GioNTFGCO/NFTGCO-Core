using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NFTGCO.API
{
    public class NFTGCOConfig : NFTGCO.Helpers.Singleton<NFTGCOConfig>
    {
        public const string ConfigAccessToken = "access_token";
        public const string ConfigRefreshToken = "refresh_token";
        public const string ConfigLoginType = "loginType";

        [SerializeField] private bool _clearKeys;

        private string _accessToken;
        private string _refreshToken;
        private string _loginType;

        public string AccessToken => PlayerPrefs.GetString(ConfigAccessToken);
        public string RefreshToken => PlayerPrefs.GetString(ConfigRefreshToken);
        public string LoginType => PlayerPrefs.GetString(ConfigLoginType);

        protected override void Awake()
        {
            base.Awake();
            if (_clearKeys)
            {
                PlayerPrefs.DeleteKey(ConfigAccessToken);
                PlayerPrefs.DeleteKey(ConfigRefreshToken);
                PlayerPrefs.DeleteKey(ConfigLoginType);
            }
        }

        private void Start()
        {
            _accessToken = AccessToken;
            _refreshToken = RefreshToken;
            _loginType = LoginType;
        }

        public void SetAccessToken(string newToken)
        {
            PlayerPrefs.SetString(ConfigAccessToken, newToken);
            _accessToken = newToken;
        }

        public void SetRefreshToken(string newToken)
        {
            PlayerPrefs.SetString(ConfigRefreshToken, newToken);
            _refreshToken = newToken;
        }

        public void SetLoginType(string newLoginType)
        {
            PlayerPrefs.SetString(ConfigLoginType, newLoginType);
            _loginType = newLoginType;
        }
    }
}