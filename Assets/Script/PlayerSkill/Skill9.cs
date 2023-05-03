using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill9 : SkillBase
{
    protected override void SkillAction()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player playerComponent))
        {
            player._health += 10;
            Debug.Log(player._health);
        }
    }
}
