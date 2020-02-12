using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalDialog : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshPro text;
    [SerializeField]
    private GameObject sprite;

    private bool isOpened;
    public bool IsOpened { get { return isOpened; } }

    public void SetText(string value) {
        text.SetText(value);
    }

    public void Open() {
        isOpened = true;
        text.gameObject.SetActive(true);
        sprite.SetActive(true);
    }

    public void Close() {
        isOpened = false;
        text.gameObject.SetActive(false);
        sprite.SetActive(false);
    }
}
