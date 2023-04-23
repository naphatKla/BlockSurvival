using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Image skillImage1;
    private float _currentCooldown;
    private float _skillCooldown;
    private KeyCode _keyCap = FindObjectOfType<SkillSystem>().skills[0].skillKey;

    private void Start()
    {
        skillImage1.fillAmount = 0;
    }

    private void Update()
    {
        _currentCooldown = FindObjectOfType<SkillSystem>().skills[0].skillCurrentCooldown;
        _skillCooldown = FindObjectOfType<SkillSystem>().skills[0].skillCooldown;
        skillImage1.fillAmount = _currentCooldown / _skillCooldown;
    }
}
