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

    [Header("EnemySpawnPointManager")] 
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private List<Transform> enemySpawnPoints;
    

    [Header("EnemySpawnerNo.1")] 
    [SerializeField] private float timeCount1;
    [SerializeField] private float startSpawn1;
    [SerializeField] private float spawnRate1;
    [SerializeField] private float minSpawnRate1;
    [SerializeField] private float reduceSpawnRate1;
    [SerializeField] private float reduceSpawnTime1;
    
    [Header("EnemySpawnerNo.2")] 
    [SerializeField] private float timeCount2;
    [SerializeField] private float startSpawn2;
    [SerializeField] private float spawnRate2;
    [SerializeField] private float minSpawnRate2;
    [SerializeField] private float reduceSpawnRate2;
    [SerializeField] private float reduceSpawnTime2;

    [Header("TimeCountUi")] 
    [SerializeField] private float timeCountUi;
    [SerializeField] private TextMeshProUGUI timeCountText;


    void Start()
    {
        Invoke("SpawnEnemy1",spawnRate1);
        Invoke("SpawnEnemy2",spawnRate2);
    }
    
    void Update()
    {
        TimeCountUi();
        
        if (timeCountUi >= startSpawn1)
        {
            TimeCountSpawnerNo1();
        }
        
        if (timeCountUi >= startSpawn2)
        {
            TimeCountSpawnerNo2();
        }

    }

    private void TimeCountSpawnerNo1()
    {
        timeCount1 += Time.deltaTime;
        
        if (timeCount1 >= reduceSpawnTime1)
        {
            spawnRate1 -= reduceSpawnRate1;
            if (spawnRate1 < minSpawnRate1)
            {
                spawnRate1 = minSpawnRate1;
            }
            timeCount1 -= reduceSpawnTime1;
        }
    }
    
    private void TimeCountSpawnerNo2()
    {
        timeCount2 += Time.deltaTime;
        
        if (timeCount2 >= reduceSpawnTime2)
        {
            spawnRate2 -= reduceSpawnRate2;
            if (spawnRate2 < minSpawnRate2)
            {
                spawnRate2 = minSpawnRate2;
            }
            timeCount2 -= reduceSpawnTime2;
        }
    }
    
    private void SpawnEnemy1()
    {
        if (timeCountUi >= startSpawn1)
        {
            GameObject enemyObject = Instantiate(enemy1, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        }
        Invoke("SpawnEnemy1",spawnRate1);
    }
    
    private void SpawnEnemy2()
    {
        if (timeCountUi >= startSpawn2)
        {
            GameObject enemyObject = Instantiate(enemy2, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        }
        Invoke("SpawnEnemy2",spawnRate2);
    }
    
    private void TimeCountUi()
    {
        timeCountUi += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(timeCountUi / 60) + ":" + Mathf.FloorToInt(timeCountUi % 60).ToString("00");
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
