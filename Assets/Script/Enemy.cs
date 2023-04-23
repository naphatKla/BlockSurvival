using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    // The condition of distance for change enemy speed depend on target distance
    [SerializeField] private float distanceThreshold;
    
    [Header("Other")]
    // Must change when player is done.
    [SerializeField] private Player player;
    private float _currentSpeed;
    private float _currentHp;
    
    [Header("Bar")]
    [SerializeField] private Scrollbar hpBar;
    void Start()
    {
        _currentHp = maxHp;
        player = FindObjectOfType<Player>();
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

    public void TakeDamage(float damage)
    {
        if(!hpBar.gameObject.activeSelf) hpBar.gameObject.SetActive(true);
        
        _currentHp -= damage;
        
        if (_currentHp <= 0)
            Destroy(gameObject);
    }

}
