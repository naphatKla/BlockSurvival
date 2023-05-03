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

    private void OnDestroy()
    {
        player.isImmune = false;
    }
}
