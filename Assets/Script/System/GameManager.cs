using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;  

    [Header("Wave Settings")]
    public int startingEnemies = 5;
    [SerializeField] private int currentWave = 1;
    private int enemiesToSpawn;
    private List<GameObject> aliveEnemies = new List<GameObject>();

    [Header("Timing")]
    public float spawnDelay = 0.5f;

    [SerializeField] private int aliveEnemyCount;
    private int totalKills = 0;

    public UImanager uiManager; // riferimento all'UIManager nell'Inspector

    void Start()
    {
        uiManager.SetWave(currentWave);
        uiManager.SetKillCount(totalKills);
        StartWave();
    }

    void StartWave()
    {
        enemiesToSpawn = startingEnemies + (currentWave - 1) * 2;
        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            aliveEnemies.Add(enemy);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.player = GameObject.FindGameObjectWithTag("Player").transform;
                enemyScript.onDeath += EnemyKilled;
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }


    void Update()
    {

        aliveEnemies.RemoveAll(e => e == null);

        aliveEnemyCount = aliveEnemies.Count;


        if (aliveEnemies.Count == 0)
        {
            currentWave++;
            uiManager.SetWave(currentWave);
            StartWave();
        }
    }

    void EnemyKilled(GameObject enemy)
    {
        totalKills++;
        aliveEnemies.Remove(enemy);
        uiManager.SetKillCount(totalKills);
    }
}