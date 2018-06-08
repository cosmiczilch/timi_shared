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
        public void LoadSceneAsync(string sceneName, LoadSceneMode mode, System.Action<bool> callback) {
            this.StartCoroutine(this.LoadSceneAsyncInternal(sceneName, mode, callback));
        }
        #endregion

        private IEnumerator LoadSceneAsyncInternal(string sceneName, LoadSceneMode mode, System.Action<bool> callback) {
            if (string.IsNullOrEmpty(sceneName)) {
                TimiDebug.LogErrorColor("Scene name not set", LogColor.red);
                callback.Invoke(false);
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, mode);
            while (!asyncOperation.isDone) {
                yield return null;
            }
            callback.Invoke(true);
        }

    }
}
