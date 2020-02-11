using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private TMPro.TextMeshPro text;

    private void Awake() {
        text = GetComponent<TMPro.TextMeshPro>();
    }

    private void Update() {
        text.SetText($"{Stats.currentHealth.ToString()}/{Stats.attemptMaxHealth.ToString()}");
    }
}
