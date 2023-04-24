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
    
    [Header("EnemySpawnPointManager")] 
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private List<Transform> spawnPointsLocation;
    
    [Header("EnemySpawnerNo.1")] 
    [SerializeField] private float startSpawn1;
    [SerializeField] private float spawnRate1;
    [SerializeField] private float minSpawnRate1;
    [SerializeField] private float reduceSpawnRate1;
    [SerializeField] private float reduceSpawnTime1;
    private float timeCount1;

    
    [Header("EnemySpawnerNo.2")] 
    [SerializeField] private float startSpawn2;
    [SerializeField] private float spawnRate2;
    [SerializeField] private float minSpawnRate2;
    [SerializeField] private float reduceSpawnRate2;
    [SerializeField] private float reduceSpawnTime2;
    private float timeCount2;

    [Header("TimeCountUi")] 
    [SerializeField] private TextMeshProUGUI timeCountText;
    private float timeCountUi;
    
    void Start()
    {
        Invoke("SpawnEnemy1",spawnRate1);
        Invoke("SpawnEnemy1",spawnRate1);
        Invoke("SpawnEnemy2",spawnRate2);
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
            GameObject enemyObject = Instantiate(enemy1, spawnPointsLocation[Random.Range(0,spawnPointsLocation.Count)].position, quaternion.identity);
        }
        Invoke("SpawnEnemy1",spawnRate1);
    }
    
    private void SpawnEnemy2()
    {
        if (timeCountUi >= startSpawn2)
        {
            GameObject enemyObject = Instantiate(enemy2, spawnPointsLocation[Random.Range(0,spawnPointsLocation.Count)].position, quaternion.identity);
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

        foreach (Transform spawnPoint in spawnPointsLocation)
        {
            Gizmos.DrawWireSphere(spawnPointsLocation[0].position,0.5f);
        }
    }
    
}
