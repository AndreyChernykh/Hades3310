using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshPro text;

    private void OnEnable() {
        Stats.OnMoneyChange += UpdateMoney;
    }

    private void OnDisable() {
        Stats.OnMoneyChange -= UpdateMoney;
    }

    private void Start() {
        UpdateMoney();
    }

    void UpdateMoney() {
        text.SetText(Stats.Money.ToString());
    }
}
