using System.Diagnostics;
using TimiShared.Debug;
using TimiShared.Loading;
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

        [SerializeField] private Camera _uiCamera;
        public Camera UICamera {
            get {
                return this._uiCamera;
            }
        }

        [SerializeField] private string _debugHUDViewPrefabPath;

        #region Unity LifeCycle
        private void Awake() {
            if (UIRootView.Instance != null) {
                TimiDebug.LogWarningColor("There should never be more than one UIRootView in the scene!", LogColor.orange);
            }
            UIRootView._instance = this;

            this.SetupDebugHUDView();
        }
        #endregion

        [Conditional("TIMI_DEBUG")]
        private void SetupDebugHUDView() {
            if (string.IsNullOrEmpty(this._debugHUDViewPrefabPath)) {
                TimiDebug.LogWarningColor("Debug hud view prefab path not set", LogColor.orange);
                return;
            }
            PrefabLoader.Instance.InstantiateAsynchronous(this._debugHUDViewPrefabPath, this.MainCanvas.transform);
        }
    }
}