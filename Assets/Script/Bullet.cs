using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private Rigidbody2D Rigidbody2D;

    [Header("bulletInfo")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] public float bulletDamage;
    
    public enum BulletType
    {
        Player,
        Enemy
    }
    
    private Player _player;
    
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (bulletType.Equals(BulletType.Enemy))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Player player = col.gameObject.GetComponent<Player>();
            
                if(player == null) return;
                player.TakeDamage(bulletDamage * _player.playerDamage);
            }
            return;
        }
        
        if (col.gameObject.CompareTag("Guard"))
        {
            col.GetComponent<Guard>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            
            if(_enemy == null) return;
            _enemy.TakeDamage(bulletDamage * _player.playerDamage);
        }
    }
}
