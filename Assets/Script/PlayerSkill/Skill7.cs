using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill7 : SkillBase
{
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }
}
