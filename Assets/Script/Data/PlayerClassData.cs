using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunTypeData", menuName = "ScriptableObjects/GunTypeData", order = 1)]
public class PlayerClassData : ScriptableObject
{
    [Header("======================= Start Stats =======================")][Space]
    public float health;
    public float damage;
    public float attackSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float dashSpeed;
    [Space]
    public float bulletSpeed;
    public float bulletOffSetScale;
    public float destroyTime;

    [Header("======================= Max Stats =======================")][Space]
    public float maxHpLimit;
    public float damageLimit;
    public float attackSpeedLimit;
    public float walkSpeedLimit;
    public float sprintSpeedLimit;
    public float dashSpeedLimit;
    
    [Header("======================= Upgrade Per Stats =======================")][Space]
    public float upgradeHealth;
    public float upgradeDamage;
    public float upgradeAttackSpeed;
    public float upgradeWalkSpeed;
    public float upgradeSprintSpeed;
    public float upgradeSprintStaminaDrain;
}
