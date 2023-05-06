using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Max Stats")]
    public float maxHpLimit;
    public float walkSpeedLimit;
    public float sprintSpeedLimit;
    public float dashSpeedLimit;

    [Header("Upgrade Stats")] 
    public float upgradeHealth;
    public float upgradeWalkSpeed;
    public float upgradeSprintSpeed;
    public float upgradeDashSpeed;
    public float upgradeSprintStaminaDrain;
    public float upgradeDashStaminaDrain;
}
