using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : SkillBase
{
    [SerializeField] private float speed;
    protected override void SkillAction()
    {
        Rigidbody2D skillRigidbody2D = GetComponent<Rigidbody2D>();
        skillRigidbody2D.velocity = transform.up * speed;
    }
}
