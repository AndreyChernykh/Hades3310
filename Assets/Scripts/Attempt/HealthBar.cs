using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    TMPro.TextMeshPro text;

    private void Awake() {
        text = GetComponent<TMPro.TextMeshPro>();
    }

    void Update() {
        text.SetText($"{Stats.currentHealth.ToString()}/{Stats.attemptMaxHealth.ToString()}");
    }
}
