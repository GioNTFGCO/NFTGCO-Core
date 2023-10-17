using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SceneField.Core
{
    public class SceneLoaderController : MonoBehaviour
    {
        [SerializeField] private SceneFieldConvertor _loadingScene;
        [SerializeField] private SceneFieldConvertor _levelScene;
        [SerializeField] private string _levelKey;
        private AsyncOperation _loadingSceneAsync;
        
        public void StartLevel()
        {
            if (_loadingSceneAsync != null)
                return;
            
            _loadingSceneAsync = SceneManager.LoadSceneAsync(_loadingScene);
            _loadingSceneAsync.allowSceneActivation = false || _loadingSceneAsync != null;
            SceneManager.LoadSceneAsync(_levelScene);
        }
        public void StarCustomLevel(SceneFieldConvertor levelScene)
        {
            _loadingSceneAsync = SceneManager.LoadSceneAsync(_loadingScene);
            _loadingSceneAsync.allowSceneActivation = false || _loadingSceneAsync != null;
            SceneManager.LoadSceneAsync(levelScene);
        }
        public void LoadSceneAddressable()
        {
            Addressables.LoadSceneAsync(_levelKey);
        }
    }
}