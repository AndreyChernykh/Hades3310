using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponOption {
    public GameObject pointer;
    public Weapons weapon;
}

public class WeaponSelect : MonoBehaviour {
    [SerializeField]
    private WeaponOption optionOne;
    [SerializeField]
    private WeaponOption optionTwo;

    private bool inputDisabled;

    private void Start() {
        Stats.ResetStats();

        optionOne.pointer.SetActive(true);
        optionTwo.pointer.SetActive(false);
    }

    private void Update() {
        if (inputDisabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            optionOne.pointer.SetActive(true);
            optionTwo.pointer.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            optionOne.pointer.SetActive(false);
            optionTwo.pointer.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1")) {
            AudioManager.Instance.Play("action");
            PickWeapon();
        }

    }

    private void PickWeapon() {
        inputDisabled = true;

        if (optionOne.pointer.activeSelf) {
            Stats.selectedWeapon = optionOne.weapon;
        }
        else {
            Stats.selectedWeapon = optionTwo.weapon;
        }

        LevelLoader.Instance.LoadLevel(LEVELS.SHOP);
    }
}
