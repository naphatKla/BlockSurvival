using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4 : SkillBase
{
    [SerializeField] private GameObject bullet;
    protected override void SkillAction()
    {
        Vector3 bulletOffSet = player.playerTransform.up;
        float angle = 18;

        for (int i = 0; i < 20; i++)
        {
            GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation * Quaternion.Euler(0,0,angle));
            bulletSpawn.GetComponent<Bullet>().bulletDamage = skillDamage;
            Destroy(bulletSpawn,destroyTime);
            angle -= 18;
        }
    }
    
}
