using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Price : MonoBehaviour {
    public int price;

    [SerializeField]
    private TMPro.TextMeshPro text;

    void Start() {
        text.SetText(price.ToString());
    }

}
