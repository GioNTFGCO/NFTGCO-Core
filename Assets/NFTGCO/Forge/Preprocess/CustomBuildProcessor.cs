#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class CustomBuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder
    {
        get { return 0; }
    }

    public void OnPreprocessBuild(BuildReport report)
    {
        PlayerPrefs.DeleteKey("access_token");
        PlayerPrefs.DeleteKey("refresh_token");
        Debug.Log($"NFTGCO Preprocess Build: {report.summary.platform} {report.summary.outputPath}");
    }
}
#endif