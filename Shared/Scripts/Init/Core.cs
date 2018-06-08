using UnityEngine;

public class Core : MonoBehaviour {

    void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }
}
