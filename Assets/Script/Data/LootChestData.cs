using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootChestData", menuName = "ScriptableObjects/LootChestData", order = 1)]
public class LootChestData : ScriptableObject
{
    [Header("Upgrade Stats")] 
    public float upgradeHealth;
    public float upgradeDamage;
    public float upgradeAttackSpeed;
    public float upgradeWalkSpeed;
    public float upgradeSprintSpeed;
    public float upgradeDashSpeed;
    public float upgradeSprintStaminaDrain;
    public float upgradeDashStaminaDrain;
}
