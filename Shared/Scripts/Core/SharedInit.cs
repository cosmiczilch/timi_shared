using System.Collections;
using TimiShared.Debug;
using TimiShared.Loading;
using TimiShared.Service;
using UnityEngine;

namespace TimiShared.Init {
    public class SharedInit : MonoBehaviour, IInitializable {

        [SerializeField] private PrefabLoader _prefabloader;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AssetLoader _assetLoader;
        [SerializeField] private SharedDataModel _sharedDataModel;

        #region IInitializable
        public void StartInitialize() {

            // Make sure the protobuf library can de/serialize custom data types
            ProtobufInit.RegisterCustomTypes();

            // Register PrefabLoader
            if (this._prefabloader != null) {
                ServiceLocator.RegisterService<PrefabLoader>(this._prefabloader);
            } else {
                TimiDebug.LogErrorColor("No prefab loader configured", LogColor.red);
                return;
            }

            // Register SceneLoader
            if (this._sceneLoader != null) {
                ServiceLocator.RegisterService<SceneLoader>(this._sceneLoader);
            } else {
                TimiDebug.LogErrorColor("No scene loader configured", LogColor.red);
                return;
            }

            // Register AssetLoader
            if (this._assetLoader != null) {
                ServiceLocator.RegisterService<AssetLoader>(this._assetLoader);
            } else {
                TimiDebug.LogErrorColor("No asset loader configured", LogColor.red);
                return;
            }

            this.StartCoroutine(this.LoadDataModels());
        }

        public bool IsFullyInitialized {
            get; private set;
        }

        public string GetName {
            get {
                return this.GetType().Name;
            }
        }
        #endregion

        private IEnumerator LoadDataModels() {
            // Register SharedDataModel
            if (this._sharedDataModel == null) {
                TimiDebug.LogErrorColor("No shared data model configured", LogColor.red);
                yield break;
            }
            yield return this._sharedDataModel.LoadDataAsync();
            ServiceLocator.RegisterService<SharedDataModel>(this._sharedDataModel);

            // Add more data models here

            this.IsFullyInitialized = true;
        }
    }
}