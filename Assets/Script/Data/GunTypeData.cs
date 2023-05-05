using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunTypeData", menuName = "ScriptableObjects/GunTypeData", order = 1)]
public class GunTypeData : ScriptableObject
{
    [Header("Gun Stats")]
    public float damage;
    public float attackSpeed;
    
    [Header("Bullet")]
    public float bulletSpeed;
    public float bulletOffSetScale;
    public float destroyTime;

    [Header("Max Stats")]
    public float maxDamage;
    public float maxAttackSpeed;
    public float maxBulletSpeed;
    
    [Header("Upgrade Stats")]
    public float upgradeDamage;
    public float upgradeAttackSpeed;
    public float upgradeBulletSpeed;
}
