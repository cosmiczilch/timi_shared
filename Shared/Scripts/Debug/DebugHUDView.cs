using TimiShared.Debug;
using UnityEngine;

public class DebugHUDView : MonoBehaviour {

    [SerializeField] private TextMesh _fpsCounterTextMesh;
    [SerializeField] private Transform _root;

    private bool _isEnabled = false;

    private void Start() {
        if (this._fpsCounterTextMesh == null) {
            TimiDebug.LogWarningColor("FPS counter text mesh not set", LogColor.orange);
            GameObject.Destroy(this.gameObject);
        }
        if (this._root == null) {
            TimiDebug.LogWarningColor("Root not set", LogColor.orange);
            GameObject.Destroy(this.gameObject);
        }
        this._root.gameObject.SetActive(this._isEnabled);
    }

    private void Update() {
        if (Input.GetButtonDown("Cancel")) {
            this._isEnabled = !this._isEnabled;
            this._root.gameObject.SetActive(this._isEnabled);
        }

        if (this._isEnabled) {
            // TODO: Use the average fps over the last 60 readings or something
            this._fpsCounterTextMesh.text = ((int)(1.0f / Time.deltaTime)).ToString();
        }
    }
}
