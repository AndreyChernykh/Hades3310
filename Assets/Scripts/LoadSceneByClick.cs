using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneByClick : MonoBehaviour {
    public LEVELS scene;

    bool disableInput;

    void Update() {
        if (!disableInput && Input.GetButtonDown("Fire1")) {
            disableInput = true;
            LevelLoader.Instance.LoadLevel(scene);
        }
    }
}
