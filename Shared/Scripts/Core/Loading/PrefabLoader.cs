using System.Collections;
using TimiShared.Debug;
using TimiShared.Service;
using UnityEngine;

namespace TimiShared.Loading {
    // TODO: Change this to not be a MonoBehaviour; right now the only reason this is a MonoBehaviour is for this.StartCoroutine
    // TODO: Add templated loaders
    public class PrefabLoader : MonoBehaviour, IService {

        public static PrefabLoader Instance {
            get {
                return ServiceLocator.Service<PrefabLoader>();
            }
        }

        #region Public API
        public GameObject InstantiateSynchronous(string path, Transform parent = null) {
            Object prefab = this.LoadPrefabSynchronous(path);
            GameObject instance = Instantiate(prefab) as GameObject;
            if (parent != null) {
                instance.transform.SetParent(parent, worldPositionStays: false);
            }
            return instance;
        }

        public void InstantiateAsynchronous(string path, Transform parent = null, System.Action<GameObject> callback = null) {
            this.LoadPrefabAsync(path, (Object prefab) => {
                GameObject instance = Instantiate(prefab) as GameObject;
                if (parent != null) {
                    instance.transform.SetParent(parent, worldPositionStays: false);
                }
                if (callback != null) {
                    callback.Invoke(instance);
                }
            });
        }

        public Object LoadPrefabSynchronous(string path) {
            Object prefab = Resources.Load(path);
            return prefab;
        }

        public void LoadPrefabAsync(string path, System.Action<Object> callback) {
            this.StartCoroutine(this.LoadAsyncInternal(path, callback));
        }
        #endregion

        private IEnumerator LoadAsyncInternal(string path, System.Action<Object> callback) {
            ResourceRequest request = Resources.LoadAsync(path);
            yield return request;
            if (request.asset == null) {
                TimiDebug.LogErrorColor("Failed to load resource at path: " + path, LogColor.red);
            }
            if (callback != null) {
                callback.Invoke(request.asset);
            }
        }
    }
}