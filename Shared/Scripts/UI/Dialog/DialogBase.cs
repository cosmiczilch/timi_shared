using System.Collections.Generic;
using TimiShared.Debug;
using TimiShared.Loading;
using UI.Dialog;
using UnityEngine;

namespace TimiShared.UI {
    public class DialogBase : MonoBehaviour, IDialogTransitionsDelegate {


        [SerializeField] protected DialogContainer.TransitionType _introTransitionType;
        [SerializeField] protected DialogContainer.TransitionType _exitTransitionType;

        private DialogContainer _container;
        protected DialogContainer Container {
            get {
                return this._container;
            }
        }

        private string _prefabName;

        #region LifeCycle
        protected virtual void Awake() {
        }

        protected virtual void OnDestroy() {
            _loadedDialogs.Remove(this._prefabName);
        }

        protected virtual void Init(string prefabName, DialogContainer container) {
            this._prefabName = prefabName;
            this._container = container;
            container.Init(this.transform, this._introTransitionType, this._exitTransitionType, this);
        }

        protected void Show() {
            this.gameObject.SetActive(true);
            this.Container.PlayIntroTransition();
        }

        protected void Hide() {
            this.Container.PlayExitTransition();
        }
        #endregion

        #region IDialogTransitionsDelegate
        public void OnDialogShowComplete() {
        }

        public void OnDialogHideComplete() {
            this.gameObject.SetActive(false);
        }
        #endregion

        #region Static Presentation
        private const string kDialogContainerPrefabPath = "Prefabs/DialogContainer";

        private static Dictionary<string, DialogBase> _loadedDialogs = new Dictionary<string, DialogBase>();

        public static void Present(string prefabName) {
            if (!_loadedDialogs.ContainsKey(prefabName)) {

                // Create new dialog container:
                DialogBase.CreateNewDialogContainer((container) => {

                    // Instantiate the dialog prefab
                    PrefabLoader.Instance.InstantiateAsynchronous(prefabName, null, (loadedGO) => {
                        DialogBase dialogBase = loadedGO.GetComponent<DialogBase>();
                        if (dialogBase != null) {

                            dialogBase.Init(prefabName, container);
                            dialogBase.Show();

                        } else {
                            TimiDebug.LogErrorColor(prefabName + " is not a dialog", LogColor.red);
                        }
                    });
                });

            }
        }

        private static void CreateNewDialogContainer(System.Action<DialogContainer> callback) {
            PrefabLoader.Instance.InstantiateAsynchronous(kDialogContainerPrefabPath, UIRootView.Instance.MainCanvas.transform, (loadedGO) => {
                DialogContainer container = loadedGO.GetComponent<DialogContainer>();
                if (container != null) {
                    callback.Invoke(container);
                } else {
                    TimiDebug.LogErrorColor("Not a container", LogColor.red);
                }
            });
        }
        #endregion

    }
}