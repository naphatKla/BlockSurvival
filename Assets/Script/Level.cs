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
    [SerializeField] private TextMeshProUGUI gunTypeUi;
    [Header("Assault Rifle")]
    [SerializeField] private Button gunTypeAssaultRifleButton;
    [Header("Shot Gun")]
    [SerializeField] private Button gunTypeShotGunButton;
    
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
                if (playerLevelUpPoint > 0)
                {
                    _player.playerDamage += 1f;
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
            if (playerLevelUpPoint > 0 && playerAttackSpeed != 0.1f)
            {
                _player.playerAttackSpeed -= 0.02f;
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
            //_combatSystem.isGunTypeAssaultRifle = true;
            gunTypeSelectPoint -= 1;
        });
        
        gunTypeShotGunButton.onClick.AddListener(() =>
        {
            //_combatSystem.isGunTypeShotGun = true;
            gunTypeSelectPoint -= 1;
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
        enemyKillText.text = $"Enemy Kill: {enemyKill}";
        playerSpeedStatusPointText.text = $"Player Speed: {_currentSpeed}";
        playerAttackSpeedStatusPointText.text = $"Player Attack Speed: {playerAttackSpeed}";
        playerHealthStatusPointText.text = $"Player Health: {playerHealth}";
        playerStatusPointText.text = $"Status point: {playerLevelUpPoint}";
        playerDamageStatusText.text = $"Player Damage: {playerDamage}";
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
            gunTypeUi.text = $"Gun Type Point: {gunTypeSelectPoint}";
            gunTypeUi.gameObject.SetActive(true);
        }
        else
        {
            gunTypeUi.gameObject.SetActive(false);
        }
    }



}

