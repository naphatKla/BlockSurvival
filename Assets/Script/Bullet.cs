using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private PlayerClassData playerClassData;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public float bulletDamage;
    [HideInInspector] public float bulletOffSetScale;
    [HideInInspector] public float destroyTime;

    public enum BulletType
    {
        Player,
        Enemy
    }
    
    private Player _player;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        if(bulletType.Equals(BulletType.Player))
            AssignBulletData();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<Player>();
        _rigidbody2D.velocity = transform.up * bulletSpeed;
        Destroy(gameObject, destroyTime);
    }
    void FixedUpdate()
    {
        //_rigidbody2D.velocity = transform.up * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (bulletType.Equals(BulletType.Enemy))
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Player player = col.gameObject.GetComponent<Player>();
            
                if(player == null) return;
                player.TakeDamage(bulletDamage);
            }
            return;
        }
        
        if (col.gameObject.CompareTag("Guard"))
        {
            col.GetComponent<Guard>().TakeDamage(_player.playerDamage);
            Destroy(gameObject);
        }
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            try
            {
                if(col == null) return;
                Enemy _enemy = col.gameObject.GetComponent<Enemy>();
                if(_enemy == null) return;
                _enemy.TakeDamage(_player.playerDamage);
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
        }
    }

    private void AssignBulletData()
    {
        bulletSpeed = playerClassData.bulletSpeed;
        bulletOffSetScale = playerClassData.bulletOffSetScale;
        destroyTime = playerClassData.destroyTime;
    }
}
