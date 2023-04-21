using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] public float skillDamage;
    [SerializeField] private float skillCooldown;
    [SerializeField] private Rigidbody2D skillRigidbody2D;
    void Start()
    {
        
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
            SkillHit(_enemy,skillDamage);
            
        }
    }

    private void SkillHit(Enemy enemy,float SkillDamage)
    {
        enemy.enemyHP -= SkillDamage;
    }
    
}
