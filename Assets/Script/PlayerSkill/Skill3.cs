using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3 : SkillBase
{
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }
}
