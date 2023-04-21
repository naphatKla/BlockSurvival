using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float _currentSpeed;
    private float _dashTimeCount;
    private bool _isDash;
    
    [Header("Camera")] 
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraDamp;
    [SerializeField] private Vector2 cameraOffset;
    private Vector2 _velocity;

    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _currentSpeed = walkSpeed;
        Sprint();
        Dash();
        CameraFollowPlayer();
        
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
    }
    
    private void CameraFollowPlayer()
    {
        
        Vector2 cameraPosition = Vector2.SmoothDamp(playerCamera.transform.position, transform.position + (Vector3)cameraOffset,
            ref _velocity, cameraDamp);

        playerCamera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _currentSpeed = sprintSpeed;
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isDash)
        {
            _isDash = true;
            _dashTimeCount = 0;
        }

        if (_dashTimeCount < dashDuration)
        {
            _dashTimeCount += Time.deltaTime;
            _currentSpeed = dashSpeed;
        }
        else
        {
            _isDash = false;
        }
    }
}