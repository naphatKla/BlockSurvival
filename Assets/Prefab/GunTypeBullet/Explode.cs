using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] public float explodeDamage;
    private Player _player;
    void Start()
    {
        _player = FindObjectOfType<Player>();
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
            
            if(_enemy == null) return;
            _enemy.TakeDamage(explodeDamage * _player.playerDamage);
            
        }
    }
}
