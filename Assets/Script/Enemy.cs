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
    [SerializeField] private GameObject player;
    private float _currentSpeed;
    private float _currentHp;
    
    [Header("Bar")]
    [SerializeField] private Scrollbar hpBar;
    void Start()
    {
        _currentHp = maxHp;
        player = GameObject.Find("PlayerTest");
        hpBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Scrollbar>();
    }
    
    void Update()
    {
        FollowTargetHandle(player);
        EnemyBarFollowEnemy();
        EnemyBarUpdate();
    }

    
    float _smoothDampVelocity;
    private void FollowTargetHandle(GameObject target)
    {
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

    private void EnemyBarFollowEnemy()
    {
        Transform hpBarRecTransform = hpBar.GetComponent<RectTransform>();
        hpBarRecTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + new Vector3(0,0.7f,0));
    }

    private void EnemyBarUpdate()
    {
        hpBar.size = _currentHp / maxHp;
    }

}
