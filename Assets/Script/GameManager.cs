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
    

    [Header("TimeCountForEnemySpawn1")] 
    [SerializeField] private float timeCount1;
    [SerializeField] private float startEnemySpawnTime1;
    [SerializeField] private float enemySpawnTime1;
    [SerializeField] private float minEnemySpawnTime1;
    [SerializeField] private float reduceEnemySpawnTime1;
    [SerializeField] private float enemySpawnRate1;
    
    [Header("TimeCountForEnemySpawn2")] 
    [SerializeField] private float timeCount2;
    [SerializeField] private float startEnemySpawnTime2;
    [SerializeField] private float enemySpawnTime2;
    [SerializeField] private float minEnemySpawnTime2;
    [SerializeField] private float reduceEnemySpawnTime2;
    [SerializeField] private float enemySpawnRate2;

    [Header("TimeCountForUi")] 
    [SerializeField] private float timeCountForUi;
    [SerializeField] private TextMeshProUGUI timeCountText;


    void Start()
    {
        Invoke("SpawnEnemy1",enemySpawnTime1);
        Invoke("SpawnEnemy2",enemySpawnTime2);
    }
    
    void Update()
    {
        if (timeCountForUi >= startEnemySpawnTime1)
        {
            TimeCountForEnemySpawn1();
        }
        
        TimeCountForEnemySpawn1();
        if (timeCountForUi >= startEnemySpawnTime2)
        {
            TimeCountForEnemySpawn2();
        }
       
        TimeCountForUi();
        
    }

    private void TimeCountForEnemySpawn1()
    {
        timeCount1 += Time.deltaTime;
        
        if (timeCount1 >= enemySpawnRate1)
        {
            enemySpawnTime1 -= reduceEnemySpawnTime1;
            if (enemySpawnTime1 < minEnemySpawnTime1)
            {
                enemySpawnTime1 = minEnemySpawnTime1;
            }
            timeCount1 -= enemySpawnRate1;
        }
    }
    
    private void TimeCountForEnemySpawn2()
    {
        timeCount2 += Time.deltaTime;
        
        if (timeCount2 >= enemySpawnRate2)
        {
            enemySpawnTime2 -= reduceEnemySpawnTime2;
            if (enemySpawnTime2 < minEnemySpawnTime2)
            {
                enemySpawnTime2 = minEnemySpawnTime2;
            }
            timeCount2 -= enemySpawnRate2;
        }
    }
    
    private void SpawnEnemy1()
    {
        if (timeCountForUi >= startEnemySpawnTime1)
        {
            GameObject enemyObject = Instantiate(enemy1, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        }
        Invoke("SpawnEnemy1",enemySpawnTime1);
    }
    
    private void SpawnEnemy2()
    {
        if (timeCountForUi >= startEnemySpawnTime2)
        {
            GameObject enemyObject = Instantiate(enemy2, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        }
        Invoke("SpawnEnemy2",enemySpawnTime2);
    }
    
    private void TimeCountForUi()
    {
        timeCountForUi += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(timeCountForUi / 60) + ":" + Mathf.FloorToInt(timeCountForUi % 60).ToString("00");
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
