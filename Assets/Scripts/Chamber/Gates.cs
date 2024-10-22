﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates : MonoBehaviour {
    [SerializeField]
    private GameObject closedGates;
    [SerializeField]
    private GameObject openedGates;

    private bool isOpened = false;

    public void Open() {
        closedGates.SetActive(false);
        openedGates.SetActive(true);
        isOpened = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!isOpened) {
            return;
        }

        if (other.GetComponent<Player>() != null) {
            AudioManager.Instance.Play("chamber");
            if (Stats.currentRoom + 1 == Stats.bossRoom) {
                LevelLoader.Instance.LoadLevel(LEVELS.BOSS_FIGHT);
                return;
            }

            if (Stats.currentRoom == 4 || Stats.currentRoom == 8 || Stats.currentRoom == 12) {
                LevelLoader.Instance.LoadLevel(LEVELS.BLESSINGS);
            }
            else {
                LevelLoader.Instance.LoadLevel(LEVELS.MAP);
            }
        }
    }
}
