using System;
using System.Collections.Generic;
using System.Linq;
using NFTGCO;
using NFTGCO.API;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public static class NTFGCOAPI
{
    public const string CONTENT_TYPE_JSON = "application/json";
    public const string CONTENT_TYPE_URLENCODED = "application/x-www-form-urlencoded";

    public const string ACCOUNT_BASE_URL = "/api/account/v1/";
    public const string NFT_BASE_URL = "/api/nft/v1/";
    public const string NFT_BASE_URL_V2 = "/api/nft/v2/";
    public const string GAME_BASE_URL = "/api/game/v1/games";
    public const string PAYMENT_BASE_URL = "/api/payment/v1/";

    public static Dictionary<string, string> GetRequestHeaders()
    {
        return new Dictionary<string, string>
        {
            { "Authorization", $"Bearer {NFTGCOConfig.Instance.AccessToken}" },
            { "browser_info", "" },
            { "company_id", $"{GameSettingsSO.Instance.CompanyId}" },
            { "game_id", $"{(long)GameSettingsSO.Instance.GameId}" },
            { "platform", GameSettingsSO.Instance.GamePlatformEnum.ToString() },
            { "ssn", $"{NFTGCOConfig.Instance.GetSsn}" },
            { "ts", $"{GetTime()}" },
            { "client_version", GameSettingsSO.Instance.CoreVersionID },
            { "session_uuid", $"{NFTGCOConfig.Instance.DeviceGuid}" },
            { "device_id", DeviceUuid() }
        };
    }

    public static Dictionary<string, string> GetModifiedHeadersWithoutAuth()
    {
        var headers = GetRequestHeaders();
        headers.Remove(headers.Keys.First());
        return headers;
    }

    public static Dictionary<string, string> GetModifiedHeadersWithUserData()
    {
        var headers = GetRequestHeaders();
        headers.Add("account_id", NFTGCOConfig.Instance.UserAccountId ?? "");
        headers.Add("user_id", NFTGCOConfig.Instance.UserAccountUuId ?? "");
        return headers;
    }

    public static Dictionary<string, string> GetModifiedHeadersForTc()
    {
        var headers = GetRequestHeaders();
        headers.Remove(headers.Keys.First());
        headers.Add("tc_version", GameSettingsSO.Instance.TermsAndConditionsVersion);
        headers.Add("tc_accepted", "true");
        return headers;
    }

    private static long GetTime()
    {
        var utcNow = System.DateTime.UtcNow;
        return ConvertToTimestamp(utcNow);
    }

    private static long ConvertToTimestamp(DateTime dateTime)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = dateTime - epochStart;
        return (long)timeSpan.TotalSeconds;
    }

    public static string DeviceUuid()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    public static bool IsEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return GameSettingsSO.Instance.GameEnvironmentEnum == GameEnvironmentEnum.Development ? true : false;
#endif
    }
}
