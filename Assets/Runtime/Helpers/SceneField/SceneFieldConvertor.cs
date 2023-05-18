using UnityEngine;
namespace SceneField.Core
{
    [System.Serializable]
    public class SceneFieldConvertor
    {
        [SerializeField] private Object _sceneAsset;
        [SerializeField] private string _sceneName = "";

        public string SceneName => _sceneName;

        public static implicit operator string(SceneFieldConvertor sceneField)
        {
            return sceneField.SceneName;
        }
    }
}