using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    #region Declare Variable
    [SerializeField] private EnemyType enemyType;
    // For range enemy
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float attackRange;

    [Space] [Header("Enemy Stats")]
    [SerializeField] private float maxHp;
    [SerializeField] private float attackDamage;
    
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float turnDirectionDamp;
    private bool _canMove;
    private float _smoothDampVelocity;
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
    #endregion

    #region Unity Method
    void Start()
    {
        _currentHp = maxHp;
        _canMove = true;
        _player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        _hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Scrollbar>();
        _hpBar.gameObject.SetActive(false);
        attackRange = Random.Range(attackRange - 2f, attackRange + 2f);
        attackRange = Mathf.Round(attackRange);
        
        if (enemyType != EnemyType.Range) return;
        Invoke(nameof(ShootBullet), fireRate);
    }
    
    void Update()
    {
        FollowTargetHandle(_player);
        EnemyBarUpdate();
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
    #endregion

    #region Method

    private Vector2 _directionDamp = Vector2.zero;
    private void FollowTargetHandle(Player target)
    {
        if (!_canMove) return;
        
        Vector2 targetPosition = target.transform.position;
        Vector2 direction = Vector2.SmoothDamp(transform.up,targetPosition - (Vector2)transform.position, ref _directionDamp, turnDirectionDamp) ;
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        transform.up = direction;

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
        if (Math.Round(distanceToTarget).Equals(attackRange))
        {
            rigidbody2D.velocity = Vector2.zero;
            return;
        }

        rigidbody2D.velocity = distanceToTarget > attackRange
            ? transform.up.normalized * _currentSpeed
            : -transform.up.normalized * _currentSpeed;

        _currentSpeed = Math.Round(distanceToTarget) < distanceThreshold
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

    private void ShootBullet()
    {
        Destroy(Instantiate(bulletPrefab, transform.position, transform.rotation),2f);
        Invoke(nameof(ShootBullet), fireRate);
    }
    
    #endregion
}
