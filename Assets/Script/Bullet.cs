using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;

    [Header("bulletInfo")]
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletDamage;
    [SerializeField] public float explodeDamage;
    private Player _player;
    private CombatSystem _combatSystem;
    
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _combatSystem = FindObjectOfType<CombatSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && !_combatSystem.isGunTypeMissile)
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            
            if(_enemy == null) return;
            _enemy.TakeDamage(bulletDamage * _player.playerDamage);
        }
        
        if (col.gameObject.CompareTag("Enemy") && _combatSystem.isGunTypeMissile)
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            
            if(_enemy == null) return;
            _enemy.TakeDamage(bulletDamage * _player.playerDamage);
            GameObject explodeSpawn = Instantiate(_combatSystem.explode, transform.position, transform.rotation);
            _enemy.TakeDamage(explodeDamage * _player.playerDamage);
            Destroy(explodeSpawn,0.2f);
            Destroy(gameObject);
        }
    }
}
