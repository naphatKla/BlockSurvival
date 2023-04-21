using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float enemyHP;
    [SerializeField] private float enemyDamage;

    [SerializeField] private TextMeshProUGUI enemyText;
    
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyText.text = $"{enemyHP}";
    }
}
