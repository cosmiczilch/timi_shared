using UnityEngine;

namespace TimiShared.UI {
    public class UIRootView : MonoBehaviour {

        #region Singleton
        private static UIRootView _instance;
        public static UIRootView Instance {
            get {
                return _instance;
            }
        }
        #endregion

        [SerializeField] private Canvas _mainCanvas;
        public Canvas MainCanvas {
            get {
                return this._mainCanvas;
            }
        }

        #region Unity LifeCycle
        private void Awake() {
            UIRootView._instance = this;
        }
        #endregion
    }
}