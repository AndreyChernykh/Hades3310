using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GOD {
    ZEUS,
    APHRODITE,
    ARES,
    DIONYSUS
}

[System.Serializable]
public struct BlessingOption {
    public Sprite sprite;
    public GOD god;
}

[System.Serializable]
public struct BlessingSlot {
    public GameObject slot;
    public GameObject pointer;
}

public class Blessings : MonoBehaviour {
    [SerializeField]
    private BlessingSlot slotOne;
    [SerializeField]
    private BlessingSlot slotTwo;

    private BlessingOption blessingOne;
    private BlessingOption blessingTwo;

    [SerializeField]
    private ModalDialog dialog;

    [SerializeField]
    private List<BlessingOption> options = new List<BlessingOption>();

    private bool inputDisabled;

    private void Start() {
        slotOne.pointer.SetActive(true);
        slotTwo.pointer.SetActive(false);

        PickBlessings();
    }

    private void Update() {
        if (inputDisabled) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            slotOne.pointer.SetActive(true);
            slotTwo.pointer.SetActive(false);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            slotOne.pointer.SetActive(false);
            slotTwo.pointer.SetActive(true);
        }

        if (Input.GetButtonDown("Fire1")) {
            if (dialog.IsOpened) {
                inputDisabled = true;
                LevelLoader.Instance.LoadLevel(LEVELS.MAP);
            }
            else {
                ApplyBlessing(slotOne.pointer.activeSelf ? blessingOne.god : blessingTwo.god);
            }
        }

    }

    private void PickBlessings() {
        System.Random rng = new System.Random(System.DateTime.Now.Millisecond);

        blessingOne = options[rng.Next(0, 2)];
        blessingTwo = options[rng.Next(2, 4)];

        slotOne.slot.GetComponent<SpriteRenderer>().sprite = blessingOne.sprite;
        slotTwo.slot.GetComponent<SpriteRenderer>().sprite = blessingTwo.sprite;
    }

    private void ApplyBlessing(GOD blessing) {
        if (blessing == GOD.ZEUS) {
            dialog.SetText("Zeus grants you power! +5 Max health");
            Stats.attemptMaxHealth += 5;
        }
        else if (blessing == GOD.ARES) {
            dialog.SetText("Ares grants you strength! +1 Damage");
            Stats.currentPower += 1;
        }
        else if (blessing == GOD.APHRODITE) {
            dialog.SetText("Aphrodite healed your wounds! +6 Health");
            Stats.CurrentHealth = Mathf.Min(Stats.CurrentHealth + 6, Stats.attemptMaxHealth);
        }
        else if (blessing == GOD.DIONYSUS) {
            dialog.SetText("Dionysus grants you riches! Money (+10) ");
            Stats.Money += 10;
        }

        dialog.Open();
    }
}
