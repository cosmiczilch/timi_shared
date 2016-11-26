using System.Collections;
using System.Collections.Generic;
using TimiShared.Debug;
using UnityEngine;

namespace TimiShared.Init {

    [System.Serializable]
    public class InitializableGroup : MonoBehaviour, IInitializable {

        [SerializeField] private string _groupName;
        [SerializeField] private List<GameObject> _initializableObjects;

        #region IInitializable
        public void StartInitialize() {
            this.InitializeGroup();
            this.StartCoroutine(this.WaitForGroupToFinishInitializing());
        }

        public bool IsFullyInitialized {
            get; private set;
        }
        #endregion

        private void InitializeGroup() {
            if (this._initializableObjects == null) {
                return;
            }

            var enumerator = this._initializableObjects.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (enumerator.Current == null) {
                    TimiDebug.LogErrorColor("Initializable object list in group " + this._groupName + " has a null object", LogColor.red);
                    continue;
                }
                IInitializable initializable = enumerator.Current.GetComponent<IInitializable>();
                if (initializable == null) {
                    TimiDebug.LogErrorColor(enumerator.Current.name + " has no " + typeof(IInitializable).Name, LogColor.red);
                    continue;
                }
                TimiDebug.LogColor("Initializing " + enumerator.Current.name, LogColor.green);
                initializable.StartInitialize();
            }
        }

        private IEnumerator WaitForGroupToFinishInitializing() {
            bool fullyInitialized;
            do {
                fullyInitialized = true;

                var enumerator = this._initializableObjects.GetEnumerator();
                while (enumerator.MoveNext()) {
                    IInitializable initializable = enumerator.Current.GetComponent<IInitializable>();
                    if (!initializable.IsFullyInitialized) {
                        fullyInitialized = false;
                        break;
                    }
                }

                if (!fullyInitialized) {
                    yield return null;
                }
            } while (!fullyInitialized);
            this.IsFullyInitialized = true;
        }
    }
}