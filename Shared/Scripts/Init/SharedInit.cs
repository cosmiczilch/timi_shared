using TimiShared.Loading;
using TimiShared.Service;
using UnityEngine;

namespace TimiShared.Init {
    public class SharedInit : MonoBehaviour, IInitializable {

        #region IInitializable
        public void StartInitialize() {
            ServiceLocator.RegisterService<PrefabLoader>(new PrefabLoader());
        }

        public bool IsFullyInitialized() {
            return false;
        }
        #endregion
    }
}