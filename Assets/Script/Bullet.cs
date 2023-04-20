using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Rigidbody2D;
    
    [Header("bulletInfo")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    void Start()
    {
        Rigidbody2D.velocity = transform.up * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
