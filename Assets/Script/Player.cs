using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Declare Variable
    [Header("Component")] 
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private TrailRenderer trailEffect;

    [Header("Player Stats")] 
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] public float playerAttackSpeed;
    [SerializeField] public float playerLevel;
    [SerializeField] public float playerDamage;
    private float _health;
    private float _stamina;
    
    [Header("UI Bar")] 
    [SerializeField] private Scrollbar healthBar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Scrollbar staminaBar;
    [SerializeField] private TextMeshProUGUI staminaText;

    [Header("Camera")] 
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float cameraSmoothDamp;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _velocity2 = Vector3.zero;

    [Header("Player Movement")] 
    [SerializeField] private KeyCode sprintKey;
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float dashStaminaDrain;
    [SerializeField] private float sprintStaminaDrain;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float staminaRecoveryCooldown;
    [HideInInspector] public Transform playerTransform;
    private float _currentSpeed;
    public PlayerStatus playerStatus;

    public enum PlayerStatus
    {
        Clear,
        Idle,
        Walk,
        Sprint,
        Dash,
        Stun,
        Dead,
        StaminaRecoveryCooldown
    }
    #endregion
    
    #region Unity Method
    void Start()
    {
        playerTransform = transform.GetChild(0);
        _health = maxHealth;
        _stamina = maxStamina;
        _currentSpeed = walkSpeed;
    }
    void Update()
    {
        RotatePlayerFollowMouseDirection();
        CameraFollowPlayer();
        PlayerMovementHandle();
        PlayerBarUpdate();
    }
    
    private IEnumerator Dash()
    {
        _stamina -= dashStaminaDrain;
        trailEffect.emitting = true;
        
        float dashTimeCount = 0;
        while (dashTimeCount < dashDuration)
        {
            SetPlayerStatus(PlayerStatus.Dash);
            dashTimeCount += Time.deltaTime;
            _currentSpeed = dashSpeed;
            yield return null;
        }
        
        trailEffect.emitting = false;
    }
    private IEnumerator StaminaRecoveryCooldown()
    {
        float timeCount = 0;

        while(timeCount < staminaRecoveryCooldown)
        {
            SetPlayerStatus(PlayerStatus.StaminaRecoveryCooldown);
            timeCount += Time.deltaTime;
            _currentSpeed = walkSpeed / 2;
            yield return null;
        }
        SetPlayerStatus(PlayerStatus.Clear);
    }
    #endregion

    #region method
    private void CameraFollowPlayer()
    {
        Vector3 targetPosition = transform.position + new Vector3(0, 0, -10);
        double cameraToTargetDistance = Math.Round(Vector3.Distance(targetPosition, playerCamera.transform.position),3);
        if(cameraToTargetDistance <= 0) return;
   
        Vector3 newCameraPosition = Vector3.SmoothDamp(playerCamera.transform.position, targetPosition,
            ref _velocity, cameraSmoothDamp);

        playerCamera.transform.position = newCameraPosition;
    }
    private void DashHandle()
    {
        if (!MovementConditionCheck(PlayerStatus.Dash)) return;
        StartCoroutine(Dash());
    }
    private void StaminaRegenHandle()
    {
        _stamina = Mathf.Clamp(_stamina, 0, maxStamina);
        bool staminaRegenConditionCheck =
            (!playerStatus.Equals(PlayerStatus.Sprint) && !playerStatus.Equals(PlayerStatus.Dash)) && _stamina < maxStamina;
            
        if (!staminaRegenConditionCheck) return;
        _stamina += Time.deltaTime * staminaRegen;
    }
    private void SprintHandle()
    {
        if (!MovementConditionCheck(PlayerStatus.Sprint)) return;
        
        SetPlayerStatus(PlayerStatus.Sprint);
        _stamina -= Time.deltaTime * sprintStaminaDrain;
        _currentSpeed = sprintSpeed;
    }
    private void PlayerMovementHandle()
    {
        StaminaRegenHandle();
        
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
        if(playerStatus.Equals(PlayerStatus.StaminaRecoveryCooldown)) return;
        
        if (playerCamera.velocity == Vector3.zero)
        {
            playerStatus = PlayerStatus.Idle;
            return;
        }

        if (_stamina <= 0) StartCoroutine(StaminaRecoveryCooldown());
        
        SetPlayerStatus(PlayerStatus.Walk);
        _currentSpeed = walkSpeed;
        
        SprintHandle();
        DashHandle();
    }
    private void PlayerBarUpdate()
    {
        staminaBar.size = _stamina / maxStamina;
        staminaText.text = $"{_stamina:F0} / {maxStamina}";
        healthBar.size = _health / maxHealth;
        healthText.text = $"{_health:F0} / {maxHealth}";
    }
    
    private bool MovementConditionCheck(PlayerStatus status)
    {
        switch (status)
        {
            case PlayerStatus.Sprint:
                return Input.GetKey(sprintKey) && _stamina > 0;
            case PlayerStatus.Dash:
                return Input.GetKeyDown(dashKey) && !playerStatus.Equals( PlayerStatus.Dash) && _stamina >= dashStaminaDrain;
            default: return false;
        }
    }

    private void SetPlayerStatus(PlayerStatus status)
    {
        if(status.Equals(playerStatus)) return;
        playerStatus = status;
    }
    
    private void RotatePlayerFollowMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = playerCamera.ScreenToWorldPoint(mousePosition);
        float xAngle = mousePosition.x - playerTransform.position.x;
        float yAngle = mousePosition.y - playerTransform.position.y;
        Vector2 direction = new Vector2(xAngle, yAngle);
        playerTransform.up = direction;
    }
    #endregion
}
