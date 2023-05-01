using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    
    [Space] [Header("Enemy Stats")]
    [SerializeField] private float maxHp;
    [SerializeField] private float attackDamage;
    
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    private bool _canMove;
    private EnemyMovement _enemyMovement;
    
    // The condition of distance for change enemy speed depend on target distance
    [SerializeField] private float distanceThreshold;
    
    [Header("Other")]
    [HideInInspector] public Rigidbody2D rigidbody2D;
    private Player _player;
    private float _currentSpeed;
    private float _currentHp;

    [Header("Particle Effect")]
    [SerializeField] private ParticleSystem deadParticleSystem;
    
    [Header("Bar")]
    private Scrollbar _hpBar;
    
    public enum EnemyType
    {
        Melee,
        Range
    }
    
    public enum EnemyMovement
    {
        Clear,
        MoveToTarget,
        MoveAwayFromTarget
    }
    void Start()
    {
        _currentHp = maxHp;
        _canMove = true;
        _player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        _hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Scrollbar>();
        _hpBar.gameObject.SetActive(false);
    }
    
    void Update()
    {
        FollowTargetHandle(_player);
        EnemyBarUpdate();
    }

    
    float _smoothDampVelocity;
    private void FollowTargetHandle(Player target)
    {
        if(!_canMove) return;
        
        Vector2 direction = target.transform.position - transform.position;
        transform.up = direction;
        Vector2 targetPosition = target.transform.position;
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);

        // Melee Enemy
        if (enemyType == EnemyType.Melee)
        {
            if (distanceToTarget <= 0.5f)
            {
                rigidbody2D.velocity = Vector2.zero;
                return;
            }
            
            rigidbody2D.velocity = transform.up.normalized * _currentSpeed;
        
            _currentSpeed = distanceToTarget > distanceThreshold
                ? Mathf.SmoothDamp(_currentSpeed, maxSpeed, ref _smoothDampVelocity, 0.3f)
                : Mathf.SmoothDamp(_currentSpeed, minSpeed, ref _smoothDampVelocity, 0.3f);
            return;
        }
        
        // Range Enemy
        if (distanceToTarget < distanceThreshold)
        {
            _enemyMovement = EnemyMovement.MoveAwayFromTarget;
        }
        else if (distanceToTarget > distanceThreshold * 2)
        {
            _enemyMovement = EnemyMovement.MoveToTarget;
        }
        
        transform.up = _enemyMovement == EnemyMovement.MoveToTarget ? direction : -direction;
        
        rigidbody2D.velocity = transform.up.normalized * _currentSpeed;
        
        _currentSpeed = distanceToTarget < distanceThreshold
            ? Mathf.SmoothDamp(_currentSpeed, maxSpeed, ref _smoothDampVelocity, 0.3f)
            : Mathf.SmoothDamp(_currentSpeed, minSpeed, ref _smoothDampVelocity, 0.3f);
        
    }
    
    private void EnemyBarUpdate()
    {
        _hpBar.transform.parent.rotation = Quaternion.identity;
        _hpBar.size = _currentHp / maxHp;
        
    }
    
    public void TakeDamage(float damage, bool isKnockBack = false, float knockBackForce = 5, float knockBackDuration = 0.1f)
    {
        if(!_hpBar.gameObject.activeSelf) _hpBar.gameObject.SetActive(true);
        
        _currentHp -= damage;
        
        if (isKnockBack)
            StartCoroutine(KnockBack(knockBackForce,knockBackDuration));
        
        if (_currentHp <= 0)
        {
            ParticleEffectManager.Instance.PlayParticleEffect(deadParticleSystem,transform.position);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator KnockBack(float knockBackForce = 5, float knockBackDuration = 0.1f)
    {
        float timeCount = 0;

        while (timeCount < knockBackDuration)
        {
            _canMove = false;
            rigidbody2D.velocity = -transform.up * knockBackForce;
            timeCount += Time.deltaTime;
            yield return null;
        }
        rigidbody2D.velocity = Vector2.zero;

        _canMove = true;
    }

}
