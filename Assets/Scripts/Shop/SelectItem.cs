using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShopOption {
    public GameObject pointer;
    public Price price;
}

public class SelectItem : MonoBehaviour {
    [SerializeField]
    private List<ShopOption> options = new List<ShopOption>();
    [SerializeField]
    private GameObject notEnoughMoney;
    [SerializeField]
    private GameObject maxHealthIncreased;
    [SerializeField]
    private GameObject damageIncreased;
    [SerializeField]
    private TMPro.TextMeshPro maxHealth;
    [SerializeField]
    private TMPro.TextMeshPro damage;

    private bool inputDisabled;
    private int selectedIndex = 0;

    private void Start() {
        UpdatePointers();
    }

    private void Update() {
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

    private void UpdatePointers() {
        for (int i = 0; i < options.Count; i++) {
            if (i != selectedIndex) {
                options[i].pointer.SetActive(false);
            }
            else {
                options[i].pointer.SetActive(true);
            }
        }
    }

    private void PickItem() {
        if (selectedIndex == options.Count - 1) {
            ExitShop();

            return;
        }

        int price = options[selectedIndex].price.price;

        if (Stats.Money < price) {
            notEnoughMoney.SetActive(true);
            return;
        }

        if (selectedIndex == 0) {
            Stats.permanentMaxHealth += 3;
            Stats.attemptMaxHealth = Stats.permanentMaxHealth;
            Stats.CurrentHealth = Stats.permanentMaxHealth;
            Stats.Money -= price;
            maxHealth.SetText(Stats.permanentMaxHealth.ToString());
            maxHealthIncreased.SetActive(true);
        }
        else if (selectedIndex == 1) {
            damageIncreased.SetActive(true);
            Stats.permanentPower += 1;
            Stats.currentPower = Stats.permanentPower;
            Stats.Money -= price;
            damage.SetText(Stats.permanentPower.ToString());
        }
    }

    private void ExitShop() {
        inputDisabled = true;

        LevelLoader.Instance.LoadLevel(LEVELS.MAP);
    }
}
