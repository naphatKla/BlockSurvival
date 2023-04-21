using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemyHP;
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyExpDrop;

    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private Player player;

    [SerializeField] private GameObject enemy;



    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyText = GameObject.Find("EnemyHP").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
        enemyText.text = $"{enemyHP}";
        
    }

    private void OnDestroy()
    {
        player.playerLevel += enemyExpDrop;
        GameObject enemySpawn = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
        Debug.Log("enemyDeath");
    }
}
