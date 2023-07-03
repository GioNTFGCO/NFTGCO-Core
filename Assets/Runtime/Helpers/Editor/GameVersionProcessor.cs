using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace NFTGCO.Helpers.Editor
{
    public class GameVersionProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder
        {
            get { return 0; }
        }

        private const string INITIAL_VERSION = "0.0";

        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log($"NFTGCO Preprocess Build: {report.summary.platform} {report.summary.outputPath}");

            string currentVersion = FindCurrentVersion();
            if (EditorUserBuildSettings.development)
            {
                UpdateVersion(currentVersion);
            }
        }

        private string FindCurrentVersion()
        {
            string[] currentVersion = PlayerSettings.bundleVersion.Split('[', ']');
            return currentVersion.Length == 1 ? INITIAL_VERSION : currentVersion[1];
        }

        private void UpdateVersion(string version)
        {
            if (float.TryParse(version, out float versionNumber))
            {
                //float newVersion = versionNumber + 0.01f;
                string date = DateTime.UtcNow.ToString("MMddyyyyHHmm");
                //PlayerSettings.bundleVersion = $"Version [{newVersion}] - {date}";
                PlayerSettings.bundleVersion = $"{INITIAL_VERSION}.{date}";
            }
        }
    }
}