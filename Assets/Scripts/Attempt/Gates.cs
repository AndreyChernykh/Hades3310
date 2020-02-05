using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour {
    public GameObject closedGates;
    public GameObject openedGates;

    public void Open() {
        closedGates.SetActive(false);
        openedGates.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() != null) {
            if (Stats.currentLevel == 12) {
                LevelLoader.Instance.LoadLevel(LEVELS.BOSS_FIGHT);
                return;
            }

            if (Stats.currentLevel == 4 || Stats.currentLevel == 8 || Stats.currentLevel == 12) {
                LevelLoader.Instance.LoadLevel(LEVELS.BLESSINGS);
            }
            else {
                LevelLoader.Instance.LoadLevel(LEVELS.MAP);
            }
        }
    }
}
