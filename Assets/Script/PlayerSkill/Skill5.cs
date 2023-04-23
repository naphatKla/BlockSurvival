using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5 : SkillBase
{
    [SerializeField] private GameObject bullet;
    private float _firstBulletSpeedAttack;


    protected override void SkillAction()
    {
        _firstBulletSpeedAttack = FindObjectOfType<CombatSystem>().bulletSpeedAttack;
        FindObjectOfType<SkillBase>().destroyTime-= Time.deltaTime;
        FindObjectOfType<CombatSystem>().bulletSpeedAttack *= 2f;
        Debug.Log(_firstBulletSpeedAttack);
    }

    private void OnDestroy()
    {
        FindObjectOfType<CombatSystem>().bulletSpeedAttack = _firstBulletSpeedAttack;
        Debug.Log(_firstBulletSpeedAttack);
    }
}
