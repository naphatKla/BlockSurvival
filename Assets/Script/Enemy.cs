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
     private EnemyRespawn _enemyRespawnPoint;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        enemyText = GameObject.Find("EnemyHP").GetComponent<TextMeshProUGUI>();
        enemyText.text = $"{enemyHP}";
        _enemyRespawnPoint = GetComponentInParent<EnemyRespawn>();
    }

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;
        enemyText.text = $"{enemyHP}";

        if (enemyHP > 0) return;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        player.playerLevel += enemyExpDrop;
        Debug.Log("enemyDeath");
    }
}
