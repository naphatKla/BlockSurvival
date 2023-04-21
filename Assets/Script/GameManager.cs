using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    [Header("Player")]
    [SerializeField] private Player player;

    [Header("Enemy SpawnePoint Manager")] 
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private float enemySpawnTime;
    [SerializeField] private float enemyDestroyTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy",enemySpawnTime);
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SpawnEnemy()
    {
        GameObject enemyObject = Instantiate(enemy, enemySpawnPoints[Random.Range(0,enemySpawnPoints.Count)].position, quaternion.identity);
        Invoke("SpawnEnemy",enemySpawnTime);

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
