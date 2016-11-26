using TimiShared.Init;
using UnityEngine;

namespace TimiShared.UI {
    public class UIRootView : MonoBehaviour, IInitializable {

        #region Singleton
        private static UIRootView _instance;
        public static UIRootView Instance {
            get {
                return _instance;
            }
        }
        #endregion

        #region Properties
        [SerializeField] private Canvas _mainCanvas;
        public Canvas MainCanvas {
            get {
                return this._mainCanvas;
            }
        }
        #endregion

        #region IInitializable
        public void StartInitialize() {
            // Nothing to do
        }

        public bool IsFullyInitialized {
            get; private set;
        }
        #endregion

        #region Unity LifeCycle
        private void Awake() {
            UIRootView._instance = this;
            this.IsFullyInitialized = true;
        }
        #endregion
    }
}