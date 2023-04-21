using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Component")] 
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private TrailRenderer trailEffect;

    [Header("PlayerStat")] 
    [SerializeField] private float maxStamina;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] private float dashStaminaDrain;
    [SerializeField] private float sprintStaminaDrain;

    [Header("UI Bar")] 
    [SerializeField] private Scrollbar staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;

    [Header("Camera")] 
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraSmoothDamp;
    private Vector2 _velocity = Vector2.zero;

    [Header("Movement")] 
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float _currentSpeed;
    private float _dashTime;
    private bool _isDash;
    private bool _isSprint;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        FollowCam();
        
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        if (!_isDash && !_isSprint && stamina < maxStamina )
        {
            stamina += Time.deltaTime * staminaRegen;
        }
        
        
        _currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= sprintStaminaDrain )
        {
            stamina -= Time.deltaTime * sprintStaminaDrain;
            _currentSpeed = sprintSpeed;
            _isSprint = true;
        }
        else
        {
            _isSprint = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isDash && stamina >= dashStaminaDrain)
        {
            _isDash = true;
            stamina -= dashStaminaDrain;
            trailEffect.emitting = true;
            _dashTime = 0;
        }

        if (_dashTime < dashDuration)
        {
            _dashTime += Time.deltaTime;
            _currentSpeed = dashSpeed;
        }
        else
        {
            trailEffect.emitting = false;
            _isDash = false;
        }

        staminaBar.size = stamina / maxStamina;
        staminaText.text = $"{stamina:F0} / {maxStamina}";
        
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;

       
    }
    private void FollowCam()
    {
        Vector2 cameraPosition = Vector2.SmoothDamp(playerCamera. transform.position, transform.position,
            ref _velocity, cameraSmoothDamp);

        playerCamera.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }
}


