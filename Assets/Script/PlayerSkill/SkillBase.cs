using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //[Header("Skill Type")]
    // public GameObject skillType;
    // public float skillOffset;
        
    public KeyCode skillKey;
    
    [Header("Skill Data")]
    public string name;
    public float skillDamage;
    public float cooldown;
    public float destroyTime;
    public float skillOffset;
    [HideInInspector] public bool isCooldown;
    
    void Start()
    {
        SkillAction();
        Destroy(gameObject, destroyTime);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            _enemy.TakeDamage(skillDamage);
        }
    }
    protected abstract void SkillAction();
}

