using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill9 : SkillBase
{
    protected override void SkillAction()
    {
        transform.rotation = Quaternion.identity;
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        
        if (collision.gameObject.CompareTag("Player"))
        {
            player.health += Time.deltaTime * 10f;
            Debug.Log(player.health);
        }
    }
}
