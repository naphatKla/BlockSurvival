using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private float playerSpeed;
    [SerializeField] public float playerLevel;
    [SerializeField] public float playerLevelUp;

    [SerializeField] private TextMeshProUGUI playerStatusText;
    [SerializeField] private GameObject playerStatus;
    [SerializeField] private Button statusBottom;
    [SerializeField] private TextMeshProUGUI playerStatusPointText;
    [SerializeField] private TextMeshProUGUI playerNextLevelText;
    [SerializeField] private TextMeshProUGUI playerDamageStatusText;
    [SerializeField] private Button playerDamageLevelUpButton;
    [SerializeField] private TextMeshProUGUI playerHealthStatusPointText;
    [SerializeField] private Button playerHealthLevelUpButton;
    [SerializeField] private TextMeshProUGUI playerAttackSpeedStatusPointText;
    [SerializeField] private Button playerAttackSpeedLevelUpButton;
    [SerializeField] private TextMeshProUGUI playerSpeedStatusPointText;
    [SerializeField] private Button playerSpeedLevelUpButton;
    [SerializeField] private TextMeshProUGUI enemyKillText;
    
    
    [SerializeField] public float playerLevelUpPoint;
    private Bullet _bullet;
    public float playerDamage = 1;
    public float playerHealth = 100;
    public float playerAttackSpeed = 1;
    public float enemyKill;
    private float _playerNextLevelUpExp = 10f;


    private float _currentSpeed;
    void Start()
    {
        statusBottom.onClick.AddListener(() => 
        {
            playerStatus.SetActive(!playerStatus.activeSelf);
            statusBottom.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerStatus.activeSelf ? "Close" : "Open";
        });
        
        playerDamageLevelUpButton.onClick.AddListener(() =>
            {
                if (playerLevelUpPoint > 0)
                {
                    playerDamage += 1;
                    playerLevelUpPoint -= 1;
                }
            }
        );
        
        playerHealthLevelUpButton.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerHealth += 10;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerAttackSpeedLevelUpButton.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerAttackSpeed -= 0.2f;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerSpeedLevelUpButton.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerSpeed += 0.5f;
                playerLevelUpPoint -= 1;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (playerLevelUp >= _playerNextLevelUpExp)
        {
            LevelUp();
        }
        enemyKillText.text = $"Enemy Kill: {enemyKill}";
        playerSpeedStatusPointText.text = $"Player Speed: {_currentSpeed}";
        playerAttackSpeedStatusPointText.text = $"Player Attack Speed: {playerAttackSpeed}";
        playerHealthStatusPointText.text = $"Player Health: {playerHealth}";
        playerStatusPointText.text = $"Status point: {playerLevelUpPoint}";
        playerDamageStatusText.text = $"Player Damage: {playerDamage}";
        playerNextLevelText.text = $"Next levelup {playerLevelUp}/{_playerNextLevelUpExp}";
        playerStatusText.text = $"Player Level: {playerLevel}";
        _currentSpeed = playerSpeed;
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
    }

    public void LevelUp()
    {
        if (playerLevelUp >= _playerNextLevelUpExp)
        {
            playerLevel += 1;
            playerLevelUpPoint += 1;
            playerLevelUp = 0;
            _playerNextLevelUpExp += 2;
        }
    }
}
