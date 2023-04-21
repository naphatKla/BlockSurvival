using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeedAttack;

    [SerializeField] private float offSetScale;

    [SerializeField] private GameObject Skill_1;
    [SerializeField] private float SkillLaserOffScale;

    [SerializeField] private float Skill_1Cooldown;
    private float _skillsCooldown01;
    private bool _skillsisCooldown = false;



    void Start()
    {
        InvokeRepeating("BulletSpawn", 0, bulletSpeedAttack);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRoatateOnMouseCursor();
        UsingSkillHandle();
        SkillsCooldown();
    }

    private void BulletSpawn()
    {
        Vector3 bulletOffSet = transform.up * offSetScale;
        GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation);
        Destroy(bulletSpawn,0.5f);
    }
    
    private void SkillSpawn(GameObject SkillsType,float _skillCooldown,float SkillsCooldown)
    {
        Vector3 SkillOffSet = transform.up * SkillLaserOffScale;
        GameObject SkillSpawn = Instantiate(SkillsType, transform.position + SkillOffSet, transform.rotation,transform);
        _skillsisCooldown = true;
        _skillCooldown = SkillsCooldown;
        Destroy(SkillSpawn,1f);
    }

    private void SkillsCooldown()
    {
        if (_skillsCooldown01 > 0)
        {
            _skillsCooldown01 -= Time.deltaTime;
        }
        else
        {
            _skillsCooldown01 = Skill_1Cooldown;
            _skillsisCooldown = false;
        }
    }


    private void UsingSkillHandle()
    {
        if (!_skillsisCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SkillSpawn(Skill_1,_skillsCooldown01,Skill_1Cooldown);
                Debug.Log("Skill 1");
            }
        }
        
    }
    

    private void BulletShortGunPatternSpawn()
    {
        Vector3 bulletOffSet = transform.up * offSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation * Quaternion.Euler(0,0,angle));
            Destroy(bulletSpawn,0.5f);
            angle -= 30;
        }
    }

private void PlayerRoatateOnMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
        
    }
    
    
    
    
}
