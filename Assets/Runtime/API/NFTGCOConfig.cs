using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NFTGCO.Core.RestClient;
using UnityEngine;

namespace NFTGCO.API
{
    public class NFTGCOConfig : NFTGCO.Helpers.Singleton<NFTGCOConfig>
    {
        public const string ConfigAccessToken = "access_token";
        public const string ConfigRefreshToken = "refresh_token";
        public const string ConfigLoginType = "loginType";

        [Space] [SerializeField] private bool _clearKeys;

        [Space] [SerializeField] private int _companyId = 1;

        [NFTGCO.Helpers.SearchableEnum] [SerializeField]
        private NFTGCO.Core.Global.NFTGCOGamesId _gameId;

        private int _sessionSequenceNumber = 0;
        private string _accessToken;
        private string _refreshToken;
        private string _loginType;
        private bool _loginOfflineMode;
        private bool _enabledRegistration;

        public string AccessToken => PlayerPrefs.GetString(ConfigAccessToken);
        public string RefreshToken => PlayerPrefs.GetString(ConfigRefreshToken);
        public string LoginType => PlayerPrefs.GetString(ConfigLoginType);
        public int CompanyId => _companyId;
        public NFTGCO.Core.Global.NFTGCOGamesId GameId => _gameId;
        public bool LoginOfflineMode => _loginOfflineMode;
        public bool EnabledRegistration => _enabledRegistration;

        protected override void Awake()
        {
            base.Awake();
            if (_clearKeys)
            {
                PlayerPrefs.DeleteKey(ConfigAccessToken);
                PlayerPrefs.DeleteKey(ConfigRefreshToken);
                PlayerPrefs.DeleteKey(ConfigLoginType);
            }

            RegisterAPI.RegistrationStatus(RegistrationCallback);
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

        public void SetLoginOfflineMode()
        {
            _loginOfflineMode = true;
        }

        public int GetSSN
        {
            set => _sessionSequenceNumber = value;
            get => _sessionSequenceNumber++;
        }

        private void RegistrationCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                Debug.Log($"Error registration status: {exception.Message}");
                return;
            }

            var values = JsonConvert.DeserializeObject<Dictionary<string, bool>>(response.Text);
            _enabledRegistration = values.First().Value;
        }
    }
}