using System.Collections;
using TimiShared.Debug;
using TimiShared.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TimiShared.Loading {
    // TODO: Change this to not be a MonoBehaviour; right now the only reason this is a MonoBehaviour is for this.StartCoroutine
    // TODO: Add templated loaders
    public class SceneLoader : MonoBehaviour, IService {

        public static SceneLoader Instance {
            get {
                return ServiceLocator.Service<SceneLoader>();
            }
        }

        #region Public API
        public void LoadSceneAsync(string sceneName, LoadSceneMode mode, System.Action<string, bool> callback) {
            this.StartCoroutine(this.LoadSceneAsyncInternal(sceneName, mode, callback));
        }

        public void LoadSceneSync(string sceneName, LoadSceneMode mode) {
            if (!this.CanLoadScene(sceneName)) {
                TimiDebug.LogErrorColor("Scene name not set", LogColor.red);
                return;
            }
            SceneManager.LoadScene(sceneName, mode);
        }
        #endregion

        private bool CanLoadScene(string sceneName) {
            if (string.IsNullOrEmpty(sceneName)) {
                return false;
            }
            return true;
        }

        private IEnumerator LoadSceneAsyncInternal(string sceneName, LoadSceneMode mode, System.Action<string, bool> callback) {
            AsyncOperation asyncOperation = this.CanLoadScene(sceneName) ? SceneManager.LoadSceneAsync(sceneName, mode) : null;
            if (asyncOperation != null) {
                while (!asyncOperation.isDone) {
                    yield return null;
                }
                callback.Invoke(sceneName, true);
            } else {
                TimiDebug.LogErrorColor("Could not load scene: " + sceneName, LogColor.red);
                callback.Invoke(sceneName, false);
            }
        }

    }
}
