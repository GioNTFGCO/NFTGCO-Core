using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Forge.API
{
    public class Config : NFTGCO.Helpers.Singleton<Config>
    {
        public const string ACCESS_TOKEN = "access_token";
        public const string REFRESH_TOKEN = "refresh_token";
        public const string LOGIN_TYPE = "loginType";

        [SerializeField] private bool _clearKeys;

        private string _accessToken;
        private string _refreshToken;
        private string _loginType;

        public string AccessToken => PlayerPrefs.GetString(ACCESS_TOKEN);
        public string RefreshToken => PlayerPrefs.GetString(REFRESH_TOKEN);
        public string LoginType => PlayerPrefs.GetString(LOGIN_TYPE);

        protected override void Awake()
        {
            base.Awake();
            if (_clearKeys)
            {
                PlayerPrefs.DeleteKey(ACCESS_TOKEN);
                PlayerPrefs.DeleteKey(REFRESH_TOKEN);
                PlayerPrefs.DeleteKey(LOGIN_TYPE);
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
            PlayerPrefs.SetString(ACCESS_TOKEN, newToken);
            _accessToken = newToken;
        }

        public void SetRefreshToken(string newToken)
        {
            PlayerPrefs.SetString(REFRESH_TOKEN, newToken);
            _refreshToken = newToken;
        }

        public void SetLoginType(string newLoginType)
        {
            PlayerPrefs.SetString(LOGIN_TYPE, newLoginType);
            _loginType = newLoginType;
        }
    }
}