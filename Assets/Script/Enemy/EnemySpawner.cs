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
    public float timeBeforeStart;
    // Less spawn rate = more enemy spawn
    public float spawnRate;
    public float minSpawnRate;
    public float reduceSpawnRate;
    public float reduceDuration;
    public int spawnAmount;
    
    [Header("TimeCountUi")] 
    [SerializeField] private TextMeshProUGUI timeCountText;
    private float _timeInGame;
    #endregion

    #region Unity Method
    void Start()
    {
        _timeInGame = 0;
        StartCoroutine(SpawnEnemyHandle());
    }
    
    void Update()
    {
        SetTimeInGameText();
    }
    
    private IEnumerator SpawnEnemyHandle()
    {
        yield return new WaitForSeconds(timeBeforeStart);
        StartCoroutine(ReduceSpawnRateHandle());
        
        while (true)
        {
            SpawnEnemy(spawnAmount);
           yield return new WaitForSeconds(spawnRate);
        }
    }
    
    private IEnumerator ReduceSpawnRateHandle()
    {
        while (true)
        { 
            yield return new WaitForSeconds(reduceDuration);
            spawnRate -= reduceSpawnRate;
            spawnRate = Mathf.Clamp(spawnRate, minSpawnRate, 100);
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
    private void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemy, spawnPositionList[Random.Range(0,spawnPositionList.Count)].position, quaternion.identity);
        }
    }
    
    private void SetTimeInGameText()
    {
        _timeInGame += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(_timeInGame / 60) + ":" + Mathf.FloorToInt(_timeInGame % 60).ToString("00");
    }
    #endregion
}
