using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using NFTGCO.Core.RestClient;
using NFTGCO.Helpers;
using UnityEngine;

namespace NFTGCO.API
{
    public class NFTGCOConfig : NFTGCO.Helpers.Singleton<NFTGCOConfig>
    {
        public const string ConfigAccessToken = "access_token";
        public const string ConfigRefreshToken = "refresh_token";
        public const string ConfigLoginType = "loginType";

        private const string LastBackgroundTimeKey = "LastBackgroundTime";
        private const int MaxSecondsBeforeReset = 3600; // 1 hour in seconds

        [Space] [SerializeField] private bool _clearKeys;

        private int _sessionSequenceNumber = 0;

        private int SessionSequenceNumber
        {
            get => _sessionSequenceNumber;
            set
            {
                _sessionSequenceNumber = value;
                PlayerPrefs.SetInt(nameof(_sessionSequenceNumber), _sessionSequenceNumber);
                PlayerPrefs.Save();
            }
        }

        private string _accessToken;
        private string _refreshToken;
        private string _loginType;

        public string AccessToken => PlayerPrefs.GetString(ConfigAccessToken);
        public string RefreshToken => PlayerPrefs.GetString(ConfigRefreshToken);
        public string LoginType => PlayerPrefs.GetString(ConfigLoginType);
        public bool LoginOfflineMode { get; private set; }
        public bool EnabledRegistration { get; private set; }

        public int GetSsn
        {
            set => SessionSequenceNumber = value;
            get => SessionSequenceNumber++;
        }

        public string DeviceUuid { get; private set; }

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

            _sessionSequenceNumber = PlayerPrefs.GetInt(nameof(_sessionSequenceNumber), 0);

            GenerateDeviceUuid();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // The game has been put into the background, store the current date and time
                PlayerPrefs.SetString(LastBackgroundTimeKey, DateTime.UtcNow.ToString("O"));
                PlayerPrefs.Save();
            }
            else
            {
                // The game has resumed, check if 1 hour has passed since it was put into the background
                string lastBackgroundTimeStr = PlayerPrefs.GetString(LastBackgroundTimeKey, string.Empty);
                if (!string.IsNullOrEmpty(lastBackgroundTimeStr) &&
                    DateTime.TryParse(lastBackgroundTimeStr, out DateTime lastBackgroundTime))
                {
                    TimeSpan timeInBackground = DateTime.UtcNow - lastBackgroundTime;
                    if (timeInBackground.TotalSeconds >= MaxSecondsBeforeReset)
                    {
                        // At least 1 hour has passed, reset the value of _sessionSequenceNumber to 0
                        _sessionSequenceNumber = 0;

                        GenerateDeviceUuid();
                    }
                }
            }
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
            LoginOfflineMode = true;
        }

        private void RegistrationCallback(RequestException exception, ResponseHelper response)
        {
            if (exception != null)
            {
                Debug.Log($"Error registration status: {exception.Message}");
                return;
            }

            var values = JsonConvert.DeserializeObject<Dictionary<string, bool>>(response.Text);
            EnabledRegistration = values.First().Value;
        }

        private void GenerateDeviceUuid()
        {
            var uuid = Guid.NewGuid();
            DeviceUuid = uuid.ToString();
        }
    }
}