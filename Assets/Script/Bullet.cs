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
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            
            _enemy.TakeDamage(bulletDamage * _player.playerDamage);
        }
    }
}
