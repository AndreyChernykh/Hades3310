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
            LevelLoader.Instance.LoadLevel(LEVELS.MAP);
        }
    }
}
