using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopOption {
    public GameObject pointer;
    public Price price;
}

public class SelectItem : MonoBehaviour {
    public List<ShopOption> options = new List<ShopOption>();
    public GameObject notEnoughMoney;
    public GameObject maxHealthIncreased;
    public GameObject damageIncreased;
    public TMPro.TextMeshPro maxHealth;
    public TMPro.TextMeshPro damage;

    bool inputDisabled;
    int selectedIndex = 0;

    void Start() {
        UpdatePointers();
    }

    void Update() {
        if (inputDisabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {

            if (selectedIndex > 0) {
                selectedIndex -= 1;
            }
            UpdatePointers();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            if (selectedIndex < options.Count - 1) {
                selectedIndex += 1;
            }
            UpdatePointers();
        }

        if (Input.GetButtonDown("Fire1")) {
            if (notEnoughMoney.activeSelf) {
                notEnoughMoney.SetActive(false);
            }
            else if (maxHealthIncreased.activeSelf) {
                maxHealthIncreased.SetActive(false);
            }
            else if (damageIncreased.activeSelf) {
                damageIncreased.SetActive(false);
            }
            else {
                PickItem();
            }
        }

    }

    void UpdatePointers() {
        for (int i = 0; i < options.Count; i++) {
            if (i != selectedIndex) {
                options[i].pointer.SetActive(false);
            }
            else {
                options[i].pointer.SetActive(true);
            }
        }
    }

    void PickItem() {
        if (selectedIndex == options.Count - 1) {
            // Exit
            inputDisabled = true;

            LevelLoader.Instance.LoadLevel(SCENES.WEAPON_SELECT);

            return;
        }

        int price = options[selectedIndex].price.price;

        if (Stats.money < price) {
            notEnoughMoney.SetActive(true);
            return;
        }

        if (selectedIndex == 0) {
            Stats.permanentMaxHealth += 3;
            Stats.money -= price;
            maxHealth.SetText(Stats.permanentMaxHealth.ToString());
            maxHealthIncreased.SetActive(true);
        }
        else if (selectedIndex == 1) {
            damageIncreased.SetActive(true);
            Stats.permanentPower += 1;
            Stats.money -= price;
            damage.SetText(Stats.permanentPower.ToString());
        }
    }
}
