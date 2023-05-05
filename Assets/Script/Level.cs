using System;
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
    #region Declare Variables
    public float playerLevel;
    public float playerExp;
    
    [Space]
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerLevelUp playerLevelUp;
    [SerializeField] private ClassTypeSelect classTypeSelect;
    [SerializeField] private Block blockImage;
    
    [Header("Player Status")]
    [SerializeField] public float playerLevelUpPoint;
    private CombatSystem _combatSystem;
    private Player _player;
    public float playerAttackSpeed = 1;
    public float enemyKill;
    private float _playerNextLevelUpExp = 20f;

    [Header("Loot Chest")]
    [SerializeField] private Image lootChestUi;
    [SerializeField] private Button healthButton;
    [SerializeField] private Button damageButton;
    [SerializeField] private Button attackSpeedButton;
    [SerializeField] private Button speedButton;
    [SerializeField] private Button dashButton;
    private LootChest _lootChest;
    public bool isPickedLootChest;
    private float _currentSpeed;
    
    [Serializable] public struct PlayerStatus
    {
        public Image playerStatusUI;
        public Button statusButton;
        public TextMeshProUGUI statusText;
        public TextMeshProUGUI enemyKillText;
    }
    [Serializable] public struct PlayerLevelUp
    {
        [Header("Player Data")]
        public PlayerData playerData;
        
        [Header("UI")]
        public Image levelUpUI;
        public Button healthButton;
        public Button damageButton;
        public Button attackSpeedButton;
        public Button speedButton;
        public Button continueButton;
        [Space]
        public TextMeshProUGUI levelUpPointText;
        public TextMeshProUGUI healthText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI attackSpeedText;
        public TextMeshProUGUI speedText;
    }
    [Serializable] public struct ClassTypeSelect
    {
        [Header("Gun Data")]
        public GunTypeData defaultData;
        public GunTypeData assaultRifleData;
        public GunTypeData shotgunData;
        public GunTypeData sniperData;
        public GunTypeData missileData;
        public GunTypeData swordData;
        
        [Header("UI")]
        public Image classSelectUI;
        public Button assaultRifleButton;
        public Button shotGunButton;
        public Button sniperButton;
        public Button missileButton;
        public Button swordButton;
    }
    [Serializable] public struct Block
    {
        public List<Image> hpBlockImages;
        public List<Image> damageBlockImages;
        public List<Image> attackSpeedBlockImages;
        public List<Image> speedBlockImages;
        public List<Image> dashBlockImages;
    }
    #endregion

    #region Unity Method
    void Start()
    {
        _player = GetComponent<Player>();
        _combatSystem = GetComponent<CombatSystem>();

        playerStatus.statusButton.onClick.AddListener(() =>
        {
            GameObject status = playerStatus.playerStatusUI.gameObject;
            status.SetActive(!status.activeSelf);
            playerStatus.statusButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = status.activeSelf ? "Close" : "Open";
        });
        
        ClassSelectButtonAssign();
        PlayerLevelUpButtonAssign();
        LootChestButtonAssign();
    }
    
    void Update()
    {
        
        LootChest();
    }
    
        private IEnumerator LevelUpUI()
    {
        yield return new WaitUntil(() => classTypeSelect.classSelectUI.IsActive() == false);
        
        while (playerLevelUpPoint > 0)
        {
            CheckPlayerStats();
            playerLevelUp.levelUpUI.gameObject.SetActive(true);
            Time.timeScale = 0;
            yield return null;
        }
        
        playerLevelUp.levelUpUI.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator ClassSelectUI()
    {
        classTypeSelect.classSelectUI.gameObject.SetActive(true);

        while (classTypeSelect.classSelectUI.IsActive())
        {
            Time.timeScale = 0;
            yield return null;
        }
        
        Time.timeScale = 1;
    }
    #endregion
    

    public void LevelUp()
    {
        if (playerExp >= _playerNextLevelUpExp)
        {
            playerLevel += 1;
            playerLevelUpPoint += 1;
            playerExp = 0;
            _playerNextLevelUpExp += _playerNextLevelUpExp / 2 ;
        }

        if (playerLevel.Equals(5))
            StartCoroutine(ClassSelectUI());
        
        
        if(playerLevelUpPoint > 0)
            StartCoroutine(LevelUpUI());
    }
    
    private void LootChest()
    {
        if (isPickedLootChest)
        {
            CheckPlayerStats();
            lootChestUi.gameObject.SetActive(true);
        }
        else
        {
            lootChestUi.gameObject.SetActive(false);
        }
    }
    
    public void LevelGain(float exp)
    {
        playerExp += exp;
        
        if (playerExp >= _playerNextLevelUpExp)
            LevelUp();
    }

    private void CheckPlayerStats()
    {
        if (_player._health > playerLevelUp.playerData.maxHpLimit && !blockImage.hpBlockImages[0].IsActive())
        {
            foreach (var blockImage in blockImage.hpBlockImages)
                blockImage.gameObject.SetActive(true);
        }
        
        if(_player.playerDamage > GetGunTypeData(_combatSystem.playerClass).maxDamage && !blockImage.damageBlockImages[0].IsActive())
        {
            foreach (var blockImage in blockImage.damageBlockImages)
                blockImage.gameObject.SetActive(true);
        }
        
        if(_player.playerAttackSpeed < GetGunTypeData(_combatSystem.playerClass).maxAttackSpeed && !blockImage.attackSpeedBlockImages[0].IsActive())
        {
            foreach (var blockImage in blockImage.attackSpeedBlockImages)
                blockImage.gameObject.SetActive(true);
        }
        
        if(_player.sprintSpeed > playerLevelUp.playerData.sprintSpeedLimit && !blockImage.speedBlockImages[0].IsActive())
        {
            foreach (var blockImage in blockImage.speedBlockImages)
                blockImage.gameObject.SetActive(true);
        }
    }
    
    public GunTypeData GetGunTypeData(CombatSystem.PlayerClass playerClass)
    {
        switch (playerClass)
        {
            case CombatSystem.PlayerClass.AssaultRifle:
                return classTypeSelect.assaultRifleData;
            case CombatSystem.PlayerClass.Shotgun:
                return classTypeSelect.shotgunData;
            case CombatSystem.PlayerClass.Sniper:
                return classTypeSelect.sniperData;
            case CombatSystem.PlayerClass.Missile:
                return classTypeSelect.missileData;
            case CombatSystem.PlayerClass.Sword:
                return classTypeSelect.swordData;
            default:
                return classTypeSelect.defaultData;
        }
    }
    
        private void LootChestButtonAssign()
    {
        healthButton.onClick.AddListener(() =>
        {
            if (isPickedLootChest)
            {
                _player.maxHealth += 150f;
                _player._health += 150f;
                isPickedLootChest = false;
            }
        });

        damageButton.onClick.AddListener(() =>
        {
            if (isPickedLootChest)
            {
                _player.playerDamage += 20f;
                isPickedLootChest = false;
            }
        });

        attackSpeedButton.onClick.AddListener(() =>
        {
            if (isPickedLootChest && playerAttackSpeed ! > _player._playerMaxAttackSpeed)
            {
                _player.playerAttackSpeed -= 0.3f;
                isPickedLootChest = false;
            }
        });

        speedButton.onClick.AddListener(() =>
        {
            if (isPickedLootChest)
            {
                _player.walkSpeed += 3;
                _player.sprintSpeed += 4;
                _player.sprintStaminaDrain -= 1f;
                isPickedLootChest = false;
            }
        });

        dashButton.onClick.AddListener(() =>
        {
            if (isPickedLootChest)
            {
                _player.dashSpeed += 4;
                _player.dashStaminaDrain -= 1f;
                isPickedLootChest = false;
            }
        });
    }

    private void PlayerLevelUpButtonAssign()
    {
        playerLevelUp.healthText.text = $"+ {playerLevelUp.playerData.upgradeHealth}";
        playerLevelUp.damageText.text = $"+ {GetGunTypeData(_combatSystem.playerClass).upgradeDamage}";
        playerLevelUp.attackSpeedText.text = $"+ {GetGunTypeData(_combatSystem.playerClass).upgradeAttackSpeed}";
        playerLevelUp.speedText.text = $"+ {GetGunTypeData(_combatSystem.playerClass).upgradeAttackSpeed}";
        
        playerLevelUp.healthButton.onClick.AddListener(() =>
        {
            _player.maxHealth += playerLevelUp.playerData.upgradeHealth;
            _player._health += playerLevelUp.playerData.upgradeHealth;
            playerLevelUpPoint--;
        });

        playerLevelUp.damageButton.onClick.AddListener(() =>
        {
            _player.playerDamage += GetGunTypeData(_combatSystem.playerClass).upgradeDamage;
            playerLevelUpPoint--;
        });

        playerLevelUp.attackSpeedButton.onClick.AddListener(() =>
        {
            _player.playerAttackSpeed -= GetGunTypeData(_combatSystem.playerClass).upgradeAttackSpeed;
            playerLevelUpPoint--;
        });

        playerLevelUp.speedButton.onClick.AddListener(() =>
        {
            _player.walkSpeed += playerLevelUp.playerData.upgradeSprintSpeed;
            _player.sprintSpeed += playerLevelUp.playerData.upgradeSprintSpeed;
            _player.sprintStaminaDrain -= 1f;
            playerLevelUpPoint--;
        });
        
        playerLevelUp.continueButton.onClick.AddListener(() =>
        {
            playerLevelUp.levelUpUI.gameObject.SetActive(false);
        });
    }

    private void ClassSelectButtonAssign()
    {
        classTypeSelect.assaultRifleButton.onClick.AddListener(() =>
        {
            _player._playerMaxAttackSpeed = classTypeSelect.assaultRifleData.maxAttackSpeed;
            _player.playerAttackSpeed = classTypeSelect.assaultRifleData.attackSpeed;
            _combatSystem.bulletSpeed = classTypeSelect.assaultRifleData.bulletSpeed;
            _combatSystem.playerClass = CombatSystem.PlayerClass.AssaultRifle;
            classTypeSelect.classSelectUI.gameObject.SetActive(false);
        });

        classTypeSelect.shotGunButton.onClick.AddListener(() =>
        {
            _player._playerMaxAttackSpeed = classTypeSelect.shotgunData.maxAttackSpeed;
            _player.playerAttackSpeed = classTypeSelect.shotgunData.attackSpeed;
            _combatSystem.bulletSpeed = classTypeSelect.shotgunData.bulletSpeed;
            _combatSystem.playerClass = CombatSystem.PlayerClass.Shotgun;
            classTypeSelect.classSelectUI.gameObject.SetActive(false);
        });

        classTypeSelect.sniperButton.onClick.AddListener(() =>
        {
            _player._playerMaxAttackSpeed = classTypeSelect.sniperData.maxAttackSpeed;
            _player.playerAttackSpeed = classTypeSelect.sniperData.attackSpeed;
            _combatSystem.bulletSpeed = classTypeSelect.sniperData.bulletSpeed;
            _combatSystem.playerClass = CombatSystem.PlayerClass.Sniper;
            classTypeSelect.classSelectUI.gameObject.SetActive(false);
        });

        classTypeSelect.missileButton.onClick.AddListener(() =>
        {
            _player._playerMaxAttackSpeed = classTypeSelect.missileData.maxAttackSpeed;
            _player.playerAttackSpeed = classTypeSelect.missileData.attackSpeed;
            _combatSystem.bulletSpeed = classTypeSelect.missileData.bulletSpeed;
            _combatSystem.playerClass = CombatSystem.PlayerClass.Missile;
            classTypeSelect.classSelectUI.gameObject.SetActive(false);
        });

        classTypeSelect.swordButton.onClick.AddListener(() =>
        {
            _player._playerMaxAttackSpeed = classTypeSelect.swordData.maxAttackSpeed;
            _player.playerAttackSpeed = classTypeSelect.swordData.attackSpeed;
            _combatSystem.bulletSpeed = classTypeSelect.swordData.bulletSpeed;
            _combatSystem.playerClass = CombatSystem.PlayerClass.Sword;
            classTypeSelect.classSelectUI.gameObject.SetActive(false);
        });
    }

}

