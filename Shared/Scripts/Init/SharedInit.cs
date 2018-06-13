using TimiShared.Debug;
using TimiShared.Loading;
using TimiShared.Service;
using UnityEngine;

namespace TimiShared.Init {
    public class SharedInit : MonoBehaviour, IInitializable {

        [SerializeField] private PrefabLoader _prefabloader;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private AssetLoader _assetLoader;

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

            // Register SharedDataModel
            SharedDataModel sharedDataModel = new SharedDataModel();
            sharedDataModel.LoadData();
            ServiceLocator.RegisterService<SharedDataModel>(sharedDataModel);

            this.IsFullyInitialized = true;
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
    }
}