using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : SkillBase
{
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }

    protected override void OnTriggerStay2D(Collider2D col)
    {
        base.OnTriggerStay2D(col);
        if (col.gameObject.CompareTag("Guard"))
        {
            float distance = Vector2.Distance(transform.position, col.transform.position) - skillOffset;
            Debug.DrawRay(transform.position,distance * transform.up,Color.red);
            transform.localScale = new Vector3(transform.localScale.x, distance, 1);
        }
    }
}
