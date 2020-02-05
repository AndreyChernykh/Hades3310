using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct EnemyMeta {
    [SerializeField]
    public int enemyCount;

    [SerializeField]
    public List<int> spawnWeights;
}

public class EnemySpawner : MonoBehaviour {
    public UnityEvent OnAllEnemiesKilled;

    public List<float> xSpawnCoodr;
    public List<float> ySpawnCoord;

    public List<EnemyMeta> enemiesPerLevel;
    public List<GameObject> enemies;

    public Player player;

    private int combinedWeight;
    private System.Random rng;
    EnemyMeta enemySpawnPool;

    private List<Enemy> spawnedEnemies = new List<Enemy>();

    void Start() {
        rng = new System.Random(Mathf.RoundToInt(Time.time));

        enemySpawnPool = enemiesPerLevel[Stats.currentLevel];

        for (int i = 0; i < enemySpawnPool.enemyCount; i++) {
            SpawnEnemy(GetEnemyToSpawn(), i);
        }
    }

    GameObject GetEnemyToSpawn() {
        foreach (int weight in enemySpawnPool.spawnWeights) {
            combinedWeight += weight;
        }

        float rngWeight = rng.Next(0, combinedWeight);
        for (int i = 0; i < enemies.Count; i++) {
            GameObject enemyPrefab = enemies[i];
            int weight = enemySpawnPool.spawnWeights[i];
            if (weight < 0) continue;

            if (rngWeight < weight) {
                return enemyPrefab;
            }
            rngWeight -= weight;
        }

        Debug.LogWarning("Spawn a default enemy");
        return enemies[0];
    }

    void SpawnEnemy(GameObject enemyPrefab, int index) {
        Vector2 spawnPoint = new Vector2(xSpawnCoodr[rng.Next(0, xSpawnCoodr.Count)], ySpawnCoord[rng.Next(0, ySpawnCoord.Count)]);
        GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.SetSeed(rng.Next(0, Mathf.RoundToInt(Time.time) + index * 100));
        enemy.SetPlayer(player);

        spawnedEnemies.Add(enemy);

        enemy.OnDie += UnregisterEnemy;
    }

    void UnregisterEnemy(Enemy enemy) {
        spawnedEnemies.Remove(enemy);
        enemy.OnDie -= UnregisterEnemy;

        if (spawnedEnemies.Count <= 0) {
            OnAllEnemiesKilled.Invoke();
        }
    }
}
