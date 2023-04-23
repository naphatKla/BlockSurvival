using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private List<SkillBase> skills;
    void Start()
    {
        // I don't know why this doesn't work
         foreach (SkillBase skill in skills)
         {
           skill.isCooldown = false;
         }
    }
    
    void Update()
    {
        foreach (SkillBase skill in skills)
        {
            if (CheckSkillCondition(skill))
            {
                PlaySkill(skill);
            }
        }
    }
    
    private bool CheckSkillCondition(SkillBase skill)
    {
        if (skill.isCooldown) return false;
        if (Input.GetKeyDown(skill.skillKey)) return true;
        return false;
    }
    
    private void PlaySkill(SkillBase skill) 
    {
        Vector3 skillOffSet = transform.up * skill.skillOffset; 
        Instantiate(skill, transform.position + skillOffSet, transform.rotation); 
        StartCoroutine(SkillsCooldown(skill));
    }
    
    private IEnumerator SkillsCooldown(SkillBase skill) 
    {
        skill.isCooldown = true; 
        yield return new WaitForSeconds(skill.cooldown); 
        skill.isCooldown = false;
    } 
}
