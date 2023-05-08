using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletType bulletType;
    [SerializeField] private PlayerClassData playerClassData;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletDamage;
    [SerializeField] public float bulletOffSetScale;
    [SerializeField] public float destroyTime;

    public enum BulletType
    {
        Player,
        Enemy,
        PlayerHelper
    }
    
    protected Player player;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        if(bulletType.Equals(BulletType.Player))
            AssignBulletData();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        _rigidbody2D.velocity = transform.up * bulletSpeed;
        Destroy(gameObject, destroyTime);
    }
    void FixedUpdate()
    {
        //_rigidbody2D.velocity = transform.up * bulletSpeed;
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
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
        
        float damage = bulletType.Equals(BulletType.PlayerHelper) ? bulletDamage : player.playerDamage;
        
        if (col.gameObject.CompareTag("Guard"))
        {
            col.GetComponent<Guard>().TakeDamage(damage);
            Destroy(gameObject);
        }
        
        if (col.gameObject.CompareTag("Enemy"))
        {
            try
            {
                if(col == null) return;
                Enemy _enemy = col.gameObject.GetComponent<Enemy>();
                if(_enemy == null) return;
                _enemy.TakeDamage(damage);
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
