using System.Collections.Generic;
using UnityEngine;
using TimiShared.Debug;

namespace TimiShared.Init {
    public class SharedInit : MonoBehaviour {

        [SerializeField] private List<GameObject> _initializables;

        private void Awake() {
            this.Initialize();
        }

        private void Initialize() {
            if (this._initializables != null) {
                var enumerator = this._initializables.GetEnumerator();
                while (enumerator.MoveNext()) {
                    if (enumerator.Current == null) {
                        TimiDebug.LogErrorColor("Initializable object list has a null object", LogColor.red);
                        continue;
                    }
                    IInitializable initializable = enumerator.Current.GetComponent<IInitializable>();
                    if (initializable != null) {
                        initializable.StartInitialize();
                    } else {
                        TimiDebug.LogErrorColor(enumerator.Current.name + " has no " + typeof(IInitializable).Name, LogColor.red);
                    }
                }
            }
        }

    }
}