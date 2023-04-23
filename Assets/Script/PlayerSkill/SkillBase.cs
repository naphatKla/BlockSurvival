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
    float _currentAttackCooldown;
    public float cooldownPerHit;
    
    [HideInInspector] public bool isCooldown;
    
    void Start()
    {
        SkillAction();
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        _currentAttackCooldown -= Time.deltaTime;
        if (_currentAttackCooldown < 0)
        {
            _currentAttackCooldown = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy") && _currentAttackCooldown == 0) 
        {
            Enemy _enemy = col.gameObject.GetComponent<Enemy>();
            _enemy.TakeDamage(skillDamage);
            _currentAttackCooldown += cooldownPerHit;
        }
    }
    protected abstract void SkillAction();
}

