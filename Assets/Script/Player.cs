using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Component")] [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private TrailRenderer trainEffect;

    [Header("PlayerStat")] [SerializeField]
    private float maxMana;

    [SerializeField] private float mana;
    [SerializeField] private float reMana;
    [SerializeField] private float manaDash;
    [SerializeField] private float manaSprint;

    [Header("UI Bar")] [SerializeField] private Scrollbar manaBar;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Camera")] 
    [SerializeField] private Camera followCam;
    [SerializeField] private float dampCam;
    private Vector2 _velocity = Vector2.zero;

    [Header("Movement")] [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float _currentSpeed;
    private float _dashTime;
    private bool _isDash;
    private bool _isSprint;
    

// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowCam();
        
        mana = Mathf.Clamp(mana, 0, maxMana);
        if (!_isDash && !_isSprint && mana < maxMana )
        {
            mana += Time.deltaTime * reMana;
        }
        
        
        _currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && mana >= manaSprint )
        {
            mana -= Time.deltaTime * manaSprint;
            _currentSpeed = runSpeed;
            _isSprint = true;
        }
        else
        {
            _isSprint = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !_isDash && mana >= manaDash)
        {
            _isDash = true;
            mana -= manaDash;
            trainEffect.emitting = true;
            _dashTime = 0;
        }

        if (_dashTime < dashDuration)
        {
            _dashTime += Time.deltaTime;
            _currentSpeed = dashSpeed;
        }
        else
        {
            trainEffect.emitting = false;
            _isDash = false;
        }

        manaBar.size = mana / maxMana;
        manaText.text = $"{mana:F0} / {maxMana}";
        
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;

       
    }
    private void FollowCam()
    {
        Vector2 cameraPosition = Vector2.SmoothDamp(followCam. transform.position, transform.position,
            ref _velocity, dampCam);

        followCam.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }
}


