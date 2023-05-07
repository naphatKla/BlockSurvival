using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    //[Header("Skill Type")]
    // public GameObject skillType;
    // public float skillOffset;
    
    public KeyCode skillKey;

    [Header("Skill Data")]
    [SerializeField] public string name;
    [SerializeField] public float skillDamage;
    [SerializeField] public float skillCooldown;
    [SerializeField] public float skillLevelUnlock;
    [HideInInspector] public float skillCurrentCooldown;
    [SerializeField] public float destroyTime;
    [SerializeField] public float skillOffset;
    
    [Header("Skill Hit Mode")]
    [SerializeField] public HitMode hitMode;
    [SerializeField] public float cooldownPerHit;
    [SerializeField] private bool isKnockBack;
    [SerializeField] private float knockBackForce;
    [SerializeField] private float knockBackDuration;
    private float _currentAttackCooldown;
    [HideInInspector] public Player player;
    
    public enum HitMode
    {
        Single,
        Multiple
    }
    
    [HideInInspector] public bool isCooldown;
    
    void Start()
    {
        skillCurrentCooldown = 0;
        _currentAttackCooldown = 0;
        player = FindObjectOfType<Player>();
        SkillAction();
        Destroy(gameObject, destroyTime);
    }

    protected virtual void Update()
    {
        _currentAttackCooldown -= Time.deltaTime;
        _currentAttackCooldown = Mathf.Clamp(_currentAttackCooldown, 0, cooldownPerHit);
    }

    private readonly List<Enemy> _enemiesInSkillArea = new List<Enemy>();
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy"))
            _enemiesInSkillArea.Add(col.GetComponent<Enemy>());
        
        if(hitMode == HitMode.Multiple) return;

        if (col.gameObject.CompareTag("Enemy"))
            col.GetComponent<Enemy>().TakeDamage(skillDamage, isKnockBack, knockBackForce, knockBackDuration);
    }

    protected virtual void OnTriggerStay2D(Collider2D col)
    {
        if(hitMode == HitMode.Single) return;
        
        if (col.gameObject.CompareTag("Enemy") && _currentAttackCooldown <= 0) 
        {
            _enemiesInSkillArea.ForEach(target => target.TakeDamage(skillDamage, isKnockBack, knockBackForce, knockBackDuration));
            _currentAttackCooldown = cooldownPerHit;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            StartCoroutine(RemoveTargetList(other.GetComponent<Enemy>()));
    }

    IEnumerator RemoveTargetList(Enemy enemy)
    {
        yield return new WaitForFixedUpdate();
        _enemiesInSkillArea.Remove(enemy);
    }
    
    protected abstract void SkillAction();
}

