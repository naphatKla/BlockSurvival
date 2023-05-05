using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GunTypeData gunTypeData;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float bulletDamage;
    [HideInInspector] public float bulletOffSetScale;
    [HideInInspector] public float destroyTime;

    private Player _player;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        AssignBulletData();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        Destroy(gameObject, destroyTime);
    }
    void FixedUpdate()
    {
        _rigidbody2D.velocity = transform.up * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            
            if(_enemy == null) return;
            _enemy.TakeDamage(bulletDamage * _player.playerDamage);
        }
    }

    private void AssignBulletData()
    {
        bulletDamage = gunTypeData.damage;
        bulletSpeed = gunTypeData.bulletSpeed;
        bulletOffSetScale = gunTypeData.bulletOffSetScale;
        destroyTime = gunTypeData.destroyTime;
    }
}
