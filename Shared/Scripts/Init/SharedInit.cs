using TimiShared.Debug;
using TimiShared.Loading;
using TimiShared.Service;
using UnityEngine;

namespace TimiShared.Init {
    public class SharedInit : MonoBehaviour, IInitializable {

        [SerializeField] private PrefabLoader _prefabloader;
        [SerializeField] private SceneLoader _sceneLoader;

        #region IInitializable
        public void StartInitialize() {

            if (this._prefabloader != null) {
                ServiceLocator.RegisterService<PrefabLoader>(this._prefabloader);
            } else {
                TimiDebug.LogErrorColor("No prefab loader configured", LogColor.red);
            }

            if (this._sceneLoader != null) {
                ServiceLocator.RegisterService<SceneLoader>(this._sceneLoader);
            } else {
                TimiDebug.LogErrorColor("No scene loader configured", LogColor.red);
            }
            this.IsFullyInitialized = true;
        }

        public bool IsFullyInitialized {
            get; private set;
        }
        #endregion
    }
}