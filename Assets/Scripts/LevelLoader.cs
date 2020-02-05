﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENES {
    TITLE,
    GAME_OVER,
    VICTORY,
    TRY_AGAIN,
    WEAPON_SELECT,
    BLESSINGS,
    SHOP,
    MAP,
    ATTEMPT,
    BOSS_FIGHT
}

public class LevelLoader : MonoBehaviour {
    [SerializeField]
    private float transitionTime = 1f;

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private static LevelLoader _instance;

    public static LevelLoader Instance { get { return _instance; } }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevelTask(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevel(SCENES scene) {
        StartCoroutine(LoadLevelTask((int) scene));
    }

    private IEnumerator LoadLevelTask(int levelIndex) {
        yield return new WaitForSecondsRealtime(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
