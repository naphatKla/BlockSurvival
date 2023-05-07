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
    [SerializeField] public bool isKnockBack;
    [SerializeField] public float knockBackForce;
    [SerializeField] public float knockBackDuration;
    public float currentAttackCooldown;
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
        currentAttackCooldown = 0;
        player = FindObjectOfType<Player>();
        SkillAction();
        Destroy(gameObject, destroyTime);
    }

    protected virtual void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        currentAttackCooldown = Mathf.Clamp(currentAttackCooldown, 0, cooldownPerHit);
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
        
        if (col.gameObject.CompareTag("Enemy") && currentAttackCooldown <= 0) 
        {
            _enemiesInSkillArea.ForEach(target => target.TakeDamage(skillDamage, isKnockBack, knockBackForce, knockBackDuration));
            currentAttackCooldown = cooldownPerHit;
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

