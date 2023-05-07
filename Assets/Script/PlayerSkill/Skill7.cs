using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill7 : SkillBase
{

    protected override void Update()
    {
        base.Update();
        player.isImmune = true;
    }
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(skillDamage, isKnockBack, knockBackForce, knockBackDuration);
        }
    }

    private void OnDestroy()
    {
        player.isImmune = false;
    }
    
}
