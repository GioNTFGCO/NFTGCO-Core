using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.PackageManager;
namespace NFTGCO.Core
{
    public static class NFTGCOPackageInitializer
    {
        private const string TextMeshProPackageName = "com.unity.textmeshpro";

        [InitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            if (!IsTextMeshProInstalled())
            {
                InstallTextMeshPro();
            }
        }

        private static bool IsTextMeshProInstalled()
        {
            return UnityEditor.PackageManager.PackageInfo.FindForAssetPath(TextMeshProPackageName) != null;
        }

        private static void InstallTextMeshPro()
        {
            Client.Add(TextMeshProPackageName);
        }
    }
}
#endif