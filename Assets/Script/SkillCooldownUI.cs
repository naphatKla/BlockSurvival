using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] private List<Image> skillImageList;
    [SerializeField] private List<TextMeshProUGUI> skillTextList;
    [SerializeField] private List<Image> skillLockImageList;
    [SerializeField] private List<TextMeshProUGUI> skillUnLockTextList;
    [SerializeField] private ParticleSystem skillUnlockEffect;
    private List<bool> _isLockedList;
    private SkillSystem _skillSystem;
    private Level _level;
    
    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _skillSystem = FindObjectOfType<SkillSystem>();
        skillTextList.ForEach(skillText => skillText.text = _skillSystem.skills[skillTextList.IndexOf(skillText)].name);
        _isLockedList = new List<bool>() { false, false, false, false, false };
        
        
        foreach (Image skillImage in skillImageList)
        {
            _skillSystem.skills[skillImageList.IndexOf(skillImage)].skillCurrentCooldown = 0;
            skillImage.fillAmount = 0;
        }
    }

    private void Update()
    {
        foreach (SkillBase skill in _skillSystem.skills)
        {
            if(_skillSystem.skills.IndexOf(skill) >= skillImageList.Count) break;
            skillImageList[_skillSystem.skills.IndexOf(skill)].fillAmount = skill.skillCurrentCooldown / skill.skillCooldown;
            
            if (_level.playerLevel < skill.skillLevelUnlock) continue;
            if (_isLockedList[_skillSystem.skills.IndexOf(skill)]) continue;
                StartCoroutine(SkillUnlock(skill));
        }
    }

    private IEnumerator SkillUnlock(SkillBase skill)
    {
        _isLockedList[_skillSystem.skills.IndexOf(skill)] = true;
        yield return new WaitForSeconds(0.2f);
        Animator animator = skillImageList[_skillSystem.skills.IndexOf(skill)].transform.parent.GetComponent<Animator>();
        animator.enabled = true;
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        skillLockImageList[_skillSystem.skills.IndexOf(skill)].gameObject.SetActive(false);
        ParticleEffectManager.Instance.PlayParticleEffect(skillUnlockEffect, skillImageList[_skillSystem.skills.IndexOf(skill)].transform.position, skillImageList[_skillSystem.skills.IndexOf(skill)].transform);
    }
}
