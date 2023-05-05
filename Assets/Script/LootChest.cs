using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class LootChest : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private Level _level;
    void Start()
    {
        _level = FindObjectOfType<Level>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _level.isPickedLootChest = true;
            Destroy(gameObject);
        }
    }
    
}
