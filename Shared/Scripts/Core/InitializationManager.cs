using System.Collections;
using System.Collections.Generic;
using TimiShared.Debug;
using UnityEngine;

namespace TimiShared.Init {
    public class InitializationManager : MonoBehaviour {

        [SerializeField] private List<UnityEngine.Object> _initializables;

        private static float MAX_TIMEOUT_IN_SECONDS = 4;

        private void Awake() {
            this.StartCoroutine(this.SerialInitialize());
        }

        private IEnumerator SerialInitialize() {
            if (this._initializables == null || this._initializables.Count == 0) {
                yield break;
            }

            float startTimeInSeconds = Time.fixedTime;
            var enumerator = this._initializables.GetEnumerator();
            while (enumerator.MoveNext()) {
                IInitializable initializable = enumerator.Current as IInitializable;
                if (initializable == null) {
                    GameObject asGO = enumerator.Current as GameObject;
                    initializable = asGO.GetComponent<IInitializable>();
                }
                if (initializable == null) {
                    TimiDebug.LogErrorColor("Unable to convert " + enumerator.Current.name + " to " + typeof(IInitializable).Name, LogColor.red);
                    continue;
                }

                TimiDebug.LogColor("Initializing " + initializable.GetName, LogColor.green);
                initializable.StartInitialize();
                while (!initializable.IsFullyInitialized) {
                    if ((Time.fixedTime - startTimeInSeconds) > MAX_TIMEOUT_IN_SECONDS) {
                        TimiDebug.LogErrorColor("Initialization timed out waiting for: " + initializable.GetName, LogColor.red);
                        yield break;
                    }
                    yield return null;
                }
            }
            TimiDebug.LogColor("Initialization complete", LogColor.green);
        }

    }
}