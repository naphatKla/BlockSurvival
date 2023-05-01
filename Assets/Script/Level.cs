using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Level : MonoBehaviour
{
    [SerializeField] public float playerLevel;
    [SerializeField] public float playerLevelUp;

    [Header("PlayerStatus Ui")]
    [SerializeField] private TextMeshProUGUI playerStatusText;
    [SerializeField] private GameObject PlayerStatus;
    [SerializeField] private Button StatusBottom;
    [SerializeField] private TextMeshProUGUI playerStatusPointText;
    [SerializeField] private TextMeshProUGUI playerNextLevelText;
    [SerializeField] private TextMeshProUGUI playerDamageStatusText;
    [SerializeField] private Button playerDamageLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerHealthStatusPointText;
    [SerializeField] private Button playerHealthLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerAttackSpeedStatusPointText;
    [SerializeField] private Button playerAttackSpeedLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerSpeedStatusPointText;
    [SerializeField] private Button playerSpeedLevelUpButtom;
    [SerializeField] private TextMeshProUGUI enemyKillText;
    [SerializeField] private Image levelUpPauseUi;
    [SerializeField] private TextMeshProUGUI levelUpPauseText;

    [Header("GunType Ui")] 
    [SerializeField] private Image GunTypeUi;
    [SerializeField] private TextMeshProUGUI gunTypeText;
    [Header("Assault Rifle")]
    [SerializeField] private Button gunTypeAssaultRifleButton;
    [Header("Shot Gun")]
    [SerializeField] private Button gunTypeShotGunButton;
    [Header("Sniper")]
    [SerializeField] private Button gunTypeSniperButton;

    [Header("GunType Stats Upgrade Damage")] 
    private bool _isHighDamage;
    private bool _isVeryHighDamage;

    [Header("GunType Stats Upgrade Attack Speed")]
    private bool isVeryHighAttackSpeed;
    
    
    [Header("Player Status")]
    [SerializeField] public float playerLevelUpPoint;
    private Bullet _bullet;
    private CombatSystem _combatSystem;
    private Player _player;
    public float playerDamage = 1;
    public float playerHealth = 100;
    public float playerAttackSpeed = 1;
    public float enemyKill;
    private float playerNextLevelUpExp = 20f;
    private float playerSpeed;
    public float gunTypeSelectPoint;


    private float _currentSpeed;
    void Start()
    {
        _player = GetComponent<Player>();
        _combatSystem = GetComponent<CombatSystem>();

        StatusBottom.onClick.AddListener(() => 
        {
            PlayerStatus.SetActive(!PlayerStatus.activeSelf);
            StatusBottom.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStatus.activeSelf ? "Close" : "Open";
        });
        
        playerDamageLevelUpButtom.onClick.AddListener(() =>
            {
                if (playerLevelUpPoint > 0 && !_isHighDamage && !_isVeryHighDamage)
                {
                    _player.playerDamage += 1f;
                    playerLevelUpPoint -= 1;
                }

                if (playerLevelUpPoint > 0 && _isHighDamage)
                {
                    _player.playerDamage += 2f;
                    playerLevelUpPoint -= 1;
                }
                
                if (playerLevelUpPoint > 0 && _isVeryHighDamage)
                {
                    _player.playerDamage += 4f;
                    playerLevelUpPoint -= 1;
                }
            }
        );
        
        playerHealthLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                _player.maxHealth += 10;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerAttackSpeedLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0 && playerAttackSpeed != _player._playerMaxAttackSpeed && !isVeryHighAttackSpeed)
            {
                _player.playerAttackSpeed -= 0.03f;
                playerLevelUpPoint -= 1;
            }
            
            if (playerLevelUpPoint > 0 && playerAttackSpeed != _player._playerMaxAttackSpeed && isVeryHighAttackSpeed)
            {
                _player.playerAttackSpeed -= 0.07f;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerSpeedLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                _player.walkSpeed += 0.4f;
                _player.sprintSpeed += 0.5f;
                playerLevelUpPoint -= 1;
            }
        });
        
        gunTypeAssaultRifleButton.onClick.AddListener(() =>
        {
            if (gunTypeSelectPoint > 0)
            {
                _player._playerMaxAttackSpeed = 0.05f;
                _player.playerAttackSpeed -= 0.2f;
                _bullet.bulletSpeed += 10f;
                _combatSystem.isGunTypeAssaultRifle = true;
                gunTypeSelectPoint -= 1;
            }
            
        });
        
        gunTypeShotGunButton.onClick.AddListener(() =>
        {
            if (gunTypeSelectPoint > 0)
            {
                _player._playerMaxAttackSpeed = 0.3f;
                _player.playerAttackSpeed += 0.5f;
                _combatSystem.isGunTypeShotgun = true;
                gunTypeSelectPoint -= 1;
            }
            
        });
        
        gunTypeSniperButton.onClick.AddListener(() =>
        {
            if (gunTypeSelectPoint > 0)
            {
                _player._playerMaxAttackSpeed = 0.5f;
                _player.playerAttackSpeed += 1f;
                _combatSystem.bulletSpeed += 10f;
                _combatSystem.isGunTypeSniper = true;
                gunTypeSelectPoint -= 1;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLevelUp >= playerNextLevelUpExp)
        {
            LevelUp();
        }
        LevelUpPauseUi();
        GunTypePauseUi();
        GunTypeCheck();
        enemyKillText.text = $"Enemy Kill: {enemyKill}";
        playerSpeedStatusPointText.text = $"Player Speed: {_currentSpeed:F2}";
        playerAttackSpeedStatusPointText.text = $"Player Attack Speed: {playerAttackSpeed:F2}";
        playerHealthStatusPointText.text = $"Player Health: {playerHealth:F2}";
        playerStatusPointText.text = $"Status point: {playerLevelUpPoint}";
        playerDamageStatusText.text = $"Player Damage: {playerDamage:F2}";
        playerNextLevelText.text = $"Next levelup {playerLevelUp}/{playerNextLevelUpExp:f0}";
        playerStatusText.text = $"Player Level: {playerLevel}";
        _currentSpeed = _player.walkSpeed;
        playerAttackSpeed = _player.playerAttackSpeed;
        playerHealth = _player.maxHealth;
        playerDamage = _player.playerDamage;

    }

    public void LevelUp()
    {
        if (playerLevelUp >= playerNextLevelUpExp)
        {
            playerLevel += 1;
            playerLevelUpPoint += 1;
            playerLevelUp = 0;
            playerNextLevelUpExp += playerNextLevelUpExp / 2 ;
        }
        if (playerLevel == 5)
        {
            gunTypeSelectPoint += 1;
        }
        
        
    }

    private void LevelUpPauseUi()
    {
        if (playerLevelUpPoint > 0 || gunTypeSelectPoint > 0) 
        {
            levelUpPauseUi.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            levelUpPauseUi.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        
    }

    private void GunTypePauseUi()
    {
        if (gunTypeSelectPoint >= 1)
        {
            gunTypeText.text = $"Gun Type Point: {gunTypeSelectPoint}";
            GunTypeUi.gameObject.SetActive(true);
        }
        else
        {
            GunTypeUi.gameObject.SetActive(false);
        }
    }

    public void GunTypeCheck()
    {
        if (_combatSystem.isGunTypeAssaultRifle)
        {
            isVeryHighAttackSpeed = true;
        }
        
        if (_combatSystem.isGunTypeShotgun)
        {
            _isHighDamage = true;
        }
        
        if (_combatSystem.isGunTypeSniper)
        {
            _isVeryHighDamage = true;
        }
    }



}

