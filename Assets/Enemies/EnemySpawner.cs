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
    [SerializeField]
    private UnityEvent OnAllEnemiesKilled;

    [SerializeField]
    private List<float> xSpawnCoodr;
    [SerializeField]
    private List<float> ySpawnCoord;

    [SerializeField]
    private List<EnemyMeta> enemiesPerLevel;
    [SerializeField]
    private List<GameObject> enemies;

    [SerializeField]
    private Player player;

    private int combinedWeight;
    private System.Random rng;
    private EnemyMeta enemySpawnPool;

    private List<Enemy> spawnedEnemies = new List<Enemy>();

    private void Start() {
        rng = new System.Random(Mathf.RoundToInt(Time.time));

        enemySpawnPool = enemiesPerLevel[Stats.currentRoom];

        for (int i = 0; i < enemySpawnPool.enemyCount; i++) {
            SpawnEnemy(GetEnemyToSpawn(), i);
        }
    }

    private GameObject GetEnemyToSpawn() {
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
#if UNITY_EDITOR
        Debug.LogWarning("Spawn a default enemy");
#endif
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
