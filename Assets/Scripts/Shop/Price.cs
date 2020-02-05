using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Price : MonoBehaviour {

    public TMPro.TextMeshPro text;
    public int price;

    void Start() {
        text.SetText(price.ToString());
    }

}
