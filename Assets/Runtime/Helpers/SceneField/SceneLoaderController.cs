using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneField.Core
{
    public class SceneLoaderController : MonoBehaviour
    {
        [SerializeField] private SceneFieldConvertor _loadingScene;
        [SerializeField] private SceneFieldConvertor _levelScene;
        private AsyncOperation _loadingSceneAsync;
        public void StartLevel()
        {
            _loadingSceneAsync = SceneManager.LoadSceneAsync(_loadingScene);
            _loadingSceneAsync.allowSceneActivation = false;
            if (_loadingSceneAsync != null)
            {
                _loadingSceneAsync.allowSceneActivation = true;
            }
            SceneManager.LoadSceneAsync(_levelScene);
        }
        public void StarCustomtLevel(SceneFieldConvertor levelScene)
        {
            _loadingSceneAsync = SceneManager.LoadSceneAsync(_loadingScene);
            _loadingSceneAsync.allowSceneActivation = false;
            if (_loadingSceneAsync != null)
            {
                _loadingSceneAsync.allowSceneActivation = true;
            }
            SceneManager.LoadSceneAsync(levelScene);
        }
    }
}