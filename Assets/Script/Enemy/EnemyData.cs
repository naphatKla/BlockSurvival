using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public Sprite sprite;
    [Space]
    public Enemy.EnemyType enemyType;
    public GameObject bulletPrefab;
    public float fireRate;
    public float attackRange;

    [Space] [Header("Enemy Stats")]
    public float maxHp;
    public float attackDamage;
    
    [Header("Movement")]
    public float maxSpeed;
    public  float minSpeed;
    public float distanceThreshold;
    
    [Header("Particle Effect")]
    public ParticleSystem deadParticleSystem;
}
