using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStart : MonoBehaviour {
    [SerializeField]
    private string sound;

    void Start() {
        AudioManager.Instance.Play(sound);
    }
}
