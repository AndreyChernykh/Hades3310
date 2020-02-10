using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshPro text;

    void Update() {
        text.SetText(Stats.money.ToString());
    }
}
