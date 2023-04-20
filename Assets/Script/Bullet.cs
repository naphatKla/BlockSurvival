using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;

    [Header("bulletInfo")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    
    
    void Start()
    {
        Rigidbody2D.velocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            BulletHit(_enemy,bulletDamage);
        }
    }

    private void BulletHit(Enemy enemy, float damage)
    {
        enemy.enemyHP -= damage;
    }
}
