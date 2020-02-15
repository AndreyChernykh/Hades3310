using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private TMPro.TextMeshPro text;

    private void Awake() {
        text = GetComponent<TMPro.TextMeshPro>();
    }

    private void OnEnable() {
        Stats.OnCurrentHealthChange += UpdateHealtBar;
    }

    private void OnDisable() {
        Stats.OnCurrentHealthChange -= UpdateHealtBar;
    }

    private void Start() {
        UpdateHealtBar();
    }

    void UpdateHealtBar() {
        text.SetText($"{Stats.CurrentHealth.ToString()}/{Stats.temporaryMaxHealth.ToString()}");
    }
}
