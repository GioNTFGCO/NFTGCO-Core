using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public static class NTFGCOAPI
{
    private const string BASE_URL = "https://dev.gaxos99.com";

    public const string CONTENT_TYPE_JSON = "application/json";
    public const string CONTENT_TYPE_URLENCODED = "application/x-www-form-urlencoded";

    public const string ACCOUNT_BASE_URL = "/api/account/v1";
    public const string NFT_BASE_URL = "/api/nft/v1";
    public const string GAME_BASE_URL = "/api/game/v1/games";
    
    public static long GetTime()
    {
        DateTime utcNow = System.DateTime.UtcNow;
        return ConvertToTimestamp(utcNow);
    }

    private static long ConvertToTimestamp(DateTime dateTime)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan timeSpan = dateTime - epochStart;
        return (long)timeSpan.TotalSeconds;
    }
    public static string GetSessionUUID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }
}