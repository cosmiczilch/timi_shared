using TimiShared.Debug;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    private Vector3 _previousMousePosition;
    int _screenWidth;
    int _screenHeight;

    private float _speed = 2.5f;

    private void OnEnable() {
        this._previousMousePosition = Input.mousePosition;
        this._screenWidth = Screen.width;
        this._screenHeight = Screen.height;
    }

#if UNITY_EDITOR
    private void Update() {
        Vector3 currentMousePosition = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.R)) {
            this.transform.localRotation = Quaternion.identity;

        } else if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) {
            TimiDebug.PrintCodePosition(LogColor.green);

            float x_delta = (currentMousePosition.x - this._previousMousePosition.x) / this._screenWidth;
            float y_delta = (currentMousePosition.y - this._previousMousePosition.y) / this._screenHeight;

            float yaw = +1 * this._speed * x_delta / Time.deltaTime;
            float pitch = -1 * this._speed * y_delta / Time.deltaTime;

            this.transform.Rotate(this.transform.up, yaw, Space.World);
            this.transform.Rotate(this.transform.right, pitch, Space.World);
        }

        this._previousMousePosition = currentMousePosition;
    }
#endif
}
