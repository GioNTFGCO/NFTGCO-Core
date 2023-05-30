using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public static class NTFGCOAPI
{
    private const string BASE_URL = "ENVIRONMENT"; // = "https://dev.gaxos99.com";
    private static string ENVIRONMENT = "";
    
    public const string CONTENT_TYPE_JSON = "application/json";
    public const string CONTENT_TYPE_URLENCODED = "application/x-www-form-urlencoded";

    public const string ACCOUNT_BASE_URL = "/api/account/v1";
    public const string NFT_BASE_URL = "/api/nft/v1";
    public const string GAME_BASE_URL = "/api/games/v1/games";

    public static string GetBASEURL()
    {
        return PlayerPrefs.GetString(BASE_URL);
    }

#if UNITY_EDITOR
    [MenuItem("NFTGCO/Use Development", priority = 1)]
    private static void ChangeToDevelopment()
    {
        PlayerPrefs.SetString(BASE_URL, "https://dev.gaxos99.com");
        ENVIRONMENT = "Development";
        Debug.Log($"Change to {ENVIRONMENT}");
    }

    [MenuItem("NFTGCO/Use Production", priority = 2)]
    private static void ChangeToProduction()
    {
        PlayerPrefs.SetString(BASE_URL, "another value");
        ENVIRONMENT = "Production";
        Debug.Log($"Change to {ENVIRONMENT}");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log($"You are using {ENVIRONMENT} environment.");
    }
#endif
}