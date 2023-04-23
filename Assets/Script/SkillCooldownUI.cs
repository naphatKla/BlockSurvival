using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private List<Image> skillImageList;
    private SkillSystem _skillSystem;
    
    private void Start()
    {
        _skillSystem = FindObjectOfType<SkillSystem>();
        
        foreach (Image skillImage in skillImageList)
        {
            skillImage.fillAmount = 0;
        }
    }

    private void Update()
    {
        foreach (SkillBase skill in _skillSystem.skills)
        {
            skillImageList[_skillSystem.skills.IndexOf(skill)].fillAmount = skill.skillCurrentCooldown / skill.skillCooldown;
        }
    }
}
