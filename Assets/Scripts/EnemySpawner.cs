using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyMeta {
    [SerializeField]
    public GameObject enemyPrefab;

    [SerializeField]
    public int spawnWeight;
}

public class EnemySpawner : MonoBehaviour {
    public List<float> xSpawnCoodr;
    public List<float> ySpawnCoord;

    public List<EnemyMeta> enemies;

    public int enemiesCount;
    public int seed;
    public int maxEnemySeed;

    public Player player;

    private int combinedWeight;
    private System.Random rng;

    void Start() {
        rng = new System.Random(seed);

        for (int i = 0; i < enemiesCount; i++) {
            SpawnEnemy(GetEnemyToSpawn());
        }
    }

    EnemyMeta GetEnemyToSpawn() {
        foreach (EnemyMeta m in enemies) {
            combinedWeight += m.spawnWeight;
        }

        float rngWeight = rng.Next(0, combinedWeight);
        for (int i = 0; i < enemies.Count; i++) {
            EnemyMeta meta = enemies[i];
            if (meta.spawnWeight < 0) continue;

            if (rngWeight < meta.spawnWeight) {
                return meta;
            }
            rngWeight -= meta.spawnWeight;
        }

        // if (weights == null || weights.Length == 0) return -1;

        // float w;
        // float t = 0;
        // int i;
        // for (i = 0; i < weights.Length; i++) {
        //     w = weights[i];

        //     if (float.IsPositiveInfinity(w)) {
        //         return i;
        //     }
        //     else if (w >= 0f && !float.IsNaN(w)) {
        //         t += weights[i];
        //     }
        // }

        // float r = Random.value;
        // float s = 0f;

        // for (i = 0; i < weights.Length; i++) {
        //     w = weights[i];
        //     if (float.IsNaN(w) || w <= 0f) continue;

        //     s += w / t;
        //     if (s >= r) return i;
        // }

        // return -1;

        Debug.LogWarning("Spawn a default enemy");
        return enemies[0];
    }

    void SpawnEnemy(EnemyMeta meta) {
        Vector2 spawnPoint = new Vector2(xSpawnCoodr[rng.Next(0, xSpawnCoodr.Count)], ySpawnCoord[rng.Next(0, ySpawnCoord.Count)]);
        GameObject enemyObj = Instantiate(meta.enemyPrefab, spawnPoint, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.SetSeed(rng.Next(0, maxEnemySeed));
        enemy.SetPlayer(player);
    }
}
