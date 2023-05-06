using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class LootChest : MonoBehaviour
{
    private Level _level;
    void Start()
    {
        _level = FindObjectOfType<Level>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _level.lootChestPickPoint++;
            _level.LootChestPick();
            Destroy(gameObject);
        }
    }
    
}
