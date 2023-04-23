using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : SkillBase
{
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }
}
