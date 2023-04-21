using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] private Player player;

    [Header("Enemy SpawnPoint Manager")] 
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private float enemySpawnTime;

    [Header("TimeCountForEnemySpawn")] 
    [SerializeField] private float timeCount;
    [SerializeField] private float minEnemySpawnTime;
    [SerializeField] private float reduceEnemySpawnTime;
    [SerializeField] private float enemySpawnRate;

    [Header("TimeCountForUi")] 
    [SerializeField] private float timeCountForUi;
    [SerializeField] private TextMeshProUGUI timeCountText;


    void Start()
    {
        Invoke("SpawnEnemy",enemySpawnTime);

    }
    
    void Update()
    {
        TimeCountForEnemySpawn();
        TimeCountForUi();
    }

    private void TimeCountForEnemySpawn()
    {
        timeCount += Time.deltaTime;
        
        if (timeCount >= enemySpawnRate)
        {
            enemySpawnTime -= reduceEnemySpawnTime;
            if (enemySpawnTime < minEnemySpawnTime)
            {
                enemySpawnTime = minEnemySpawnTime;
            }
            timeCount -= enemySpawnRate;
        }
    }

    private void TimeCountForUi()
    {
        timeCountForUi += Time.deltaTime;
        timeCountText.text = "Time: " + Mathf.FloorToInt(timeCountForUi / 60) + ":" + Mathf.FloorToInt(timeCountForUi % 60).ToString("00");
    }

    private void SpawnEnemy()
    {
        GameObject enemyObject = Instantiate(enemy, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        Invoke("SpawnEnemy",enemySpawnTime);
        //spawnEnemy 2 ตัวพร้อมกัน
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Transform spawnPoint in enemySpawnPoints)
        {
            Gizmos.DrawWireSphere(enemySpawnPoints[0].position,0.5f);
        }
    }
}
