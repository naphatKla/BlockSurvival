using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float maxHp;
    [SerializeField] private float attackDamage;
    
    [Header("Movement")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    private bool _canMove;
    
    // The condition of distance for change enemy speed depend on target distance
    [SerializeField] private float distanceThreshold;
    
    [Header("Other")]
    // Must change when player is done.
    [SerializeField] private Player player;
    private float _currentSpeed;
    private float _currentHp;
    public Rigidbody2D rigidbody2D;
    
    [Header("Particle Effect")]
    [SerializeField] private ParticleSystem _deadParticleSystem;
    
    [Header("Bar")]
    [SerializeField] private Scrollbar hpBar;
    void Start()
    {
        _currentHp = maxHp;
        _canMove = true;
        player = FindObjectOfType<Player>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Scrollbar>();
        hpBar.gameObject.SetActive(false);
    }
    
    void Update()
    {
        FollowTargetHandle(player);
        EnemyBarUpdate();
    }

    
    float _smoothDampVelocity;
    private void FollowTargetHandle(Player target)
    {
        if(!_canMove) return;
        
        Vector2 direction = target.transform.position - transform.position;
        transform.up = direction;
        Vector2 enemyPosition = transform.position;
        Vector2 targetPosition = target.transform.position;
        float distanceToTarget = Vector2.Distance(enemyPosition, targetPosition);
        
        if (distanceToTarget <= 0) return;
        
        enemyPosition =
            Vector2.MoveTowards(enemyPosition, targetPosition, _currentSpeed * Time.deltaTime);
        transform.position = enemyPosition;
        
        _currentSpeed = distanceToTarget > distanceThreshold
            ? Mathf.SmoothDamp(_currentSpeed, maxSpeed, ref _smoothDampVelocity, 0.3f)
            : Mathf.SmoothDamp(_currentSpeed, minSpeed, ref _smoothDampVelocity, 0.3f);
    }
    
    private void EnemyBarUpdate()
    {
        hpBar.transform.parent.rotation = Quaternion.identity;
        hpBar.size = _currentHp / maxHp;
        
    }

    public void TakeDamage(float damage, bool isBounce = false, float bounceForce = 5, float bounceDuration = 0.1f)
    {
        if(!hpBar.gameObject.activeSelf) hpBar.gameObject.SetActive(true);
        
        _currentHp -= damage;
        
        if (isBounce)
            StartCoroutine(BounceOff(bounceForce,bounceDuration));
        
        if (_currentHp <= 0)
        {
            ParticleEffectManager.Instance.PlayParticleEffect(_deadParticleSystem,transform.position);
            Destroy(gameObject);
        }
    }
    
    private IEnumerator BounceOff(float bounceForce = 5, float bounceDuration = 0.1f)
    {
        float timeCount = 0;

        while (timeCount < bounceDuration)
        {
            _canMove = false;
            rigidbody2D.velocity = -transform.up * 5;
            timeCount += Time.deltaTime;
            yield return null;
        }
        rigidbody2D.velocity = Vector2.zero;

        _canMove = true;
    }

}
