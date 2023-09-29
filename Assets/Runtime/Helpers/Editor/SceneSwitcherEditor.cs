#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace NFTGCO.Helpers.Editor
{
    public class SceneSwitcherEditor : EditorWindow
    {
        private string[] sceneNames;

        [MenuItem("NFTGCO/Tools/Scene Switcher")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SceneSwitcherEditor), false, "Scene Switcher");
        }

        private void OnEnable()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            sceneNames = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                int slashIndex = path.LastIndexOf('/');
                string sceneName = path.Substring(slashIndex + 1, path.LastIndexOf('.') - slashIndex - 1);
                sceneNames[i] = sceneName;
            }
        }

        private void OnGUI()
        {
            for (int i = 0; i < sceneNames.Length; i++)
            {
                if (GUILayout.Button(sceneNames[i]))
                {
                    SwitchScene(i);
                }
            }
        }

        private static void SwitchScene(int buildIndex)
        {
            if (EditorApplication.isPlaying)
            {
                SceneManager.LoadScene(buildIndex);
            }
            else
            {
                EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(buildIndex));
            }
        }
    }
}
#endif