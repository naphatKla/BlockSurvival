using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region Declare Variables
    [Header("EnemySpawnPostion")]
    [SerializeField] private List<Transform> spawnPositionList;

    [Header("Enemy")] 
    public GameObject enemy;
    [Space] 
    public float timeStart;
    public float timeEnd;
    // Less spawn rate = more enemy spawn
    public float spawnRate;
    public float minSpawnRate;
    public float reduceSpawnRate;
    public float reduceDuration;
    public Vector2 spawnAmount;
    public bool isSpawnOneTime;
    
    private GameManager _gameManager;
    #endregion

    #region Unity Method
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        StartCoroutine(SpawnEnemyHandle());
    }
    
    private IEnumerator SpawnEnemyHandle()
    {
        yield return new WaitUntil(() => _gameManager.timeInGame >= timeStart);
        if(!isSpawnOneTime)
            StartCoroutine(ReduceSpawnRateHandle());
        
        while (_gameManager.timeInGame < timeEnd)
        {
            int randomAmout = Random.Range((int)spawnAmount.x, (int)spawnAmount.y+1);
            StartCoroutine(SpawnEnemy(randomAmout, 0.2f));
            if(isSpawnOneTime)
                break;
           yield return new WaitForSeconds(spawnRate);
        }
    }
    
    private IEnumerator ReduceSpawnRateHandle()
    {
        while (_gameManager.timeInGame < timeEnd)
        { 
            yield return new WaitForSeconds(reduceDuration);
            spawnRate -= reduceSpawnRate;
            spawnRate = Mathf.Clamp(spawnRate, minSpawnRate, 100);
        }
    }

    private IEnumerator SpawnEnemy(int amount, float delay)
    {
        int random = Random.Range(0,spawnPositionList.Count);
        
        if (_gameManager.enemyLeft < 300)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(enemy, spawnPositionList[random].position, quaternion.identity);
                _gameManager.enemyLeft++;
                _gameManager.enemySpawned++;
                yield return new WaitForSeconds(delay);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Transform spawnPoint in spawnPositionList)
        {
            Gizmos.DrawWireSphere(spawnPoint.position,0.5f);
        }
    }
    #endregion

    #region Method
    #endregion
}
