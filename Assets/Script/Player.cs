using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private float playerSpeed;

    private float _currentSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
        
    }
}
