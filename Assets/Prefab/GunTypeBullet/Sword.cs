using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : SkillBase
{
    protected override void SkillAction()
    {
        transform.SetParent(player.playerTransform);
    }
}
