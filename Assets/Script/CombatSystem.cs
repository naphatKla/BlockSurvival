using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    #region Declare Variables
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletSpeedAttack;
    [SerializeField] private float bulletOffSetScale;
    [SerializeField] private Skill skill1;
    #endregion

    
    #region Declare Struct
    [Serializable] public struct Skill
    {
        [Header("Skill Type")]
        public GameObject skillType;
        public float skillOffset;
        
        [Header("Skill Data")]
        public SkillSlot skillSlot;
        public string name;
        public float cooldown;
        [HideInInspector] public bool isCooldown;
    }
    public enum SkillSlot
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
        Skill6,
        Skill7,
        Skill8,
        Skill9,
        Skill10
    }
    #endregion

    
    #region Unity Method
    void Start()
    {
        InvokeRepeating("BulletSpawn", 0, bulletSpeedAttack);
    }
    void Update()
    {
        PlayerRotateOnMouseCursor();
        UsingSkillHandle();
    }
    private IEnumerator SkillsCooldown(Skill skill)
    {
        if (skill.skillType.Equals(skill1.skillType))
        {
            skill1.isCooldown = true;
            yield return new WaitForSeconds(skill1.cooldown);
            skill1.isCooldown = false;
        }
    }
    #endregion

    
    #region Method
    private void BulletSpawn()
    {
        Vector3 bulletOffSet = transform.up * bulletOffSetScale;
        GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation);
        Destroy(bulletSpawn,0.5f);
    }
    
    private void SkillSpawn(GameObject skillsType)
    {
        Vector3 skillOffSet = transform.up * skill1.skillOffset;
        GameObject skillSpawn = Instantiate(skillsType, transform.position + skillOffSet, transform.rotation,transform);
        StartCoroutine(SkillsCooldown(skill1));
        Destroy(skillSpawn,1f);
    }
    
    private void UsingSkillHandle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !skill1.isCooldown)
        {
            SkillSpawn(skill1.skillType);
            Debug.Log(skill1.name);
        }
    }
    
    private void BulletShortGunPatternSpawn()
    {
        Vector3 bulletOffSet = transform.up * bulletOffSetScale;
        float angle = 30;

        for (int i = 0; i < 3; i++)
        {
            GameObject bulletSpawn = Instantiate(bullet, transform.position + bulletOffSet, transform.rotation * Quaternion.Euler(0,0,angle));
            Destroy(bulletSpawn,0.5f);
            angle -= 30;
        }
    }
    
    private void PlayerRotateOnMouseCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = mousePosition - transform.position;
        transform.up = direction;
    }
    #endregion
}
