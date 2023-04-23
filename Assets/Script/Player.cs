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
    [SerializeField] private Button openStatusBottom;
    [SerializeField] private TextMeshProUGUI buttomOpenStatusText;
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
    
    
    [SerializeField] public float playerLevelUpPoint;
    private Bullet _bullet;
    private CombatSystem _combatSystem;
    public float playerDamage = 1;
    public float playerHealth = 100;
    public float playerAttackSpeed = 1;
    public float enemyKill;
    private float playerNextLevelUpExp = 10f;


    private float _currentSpeed;
    void Start()
    {

        StatusBottom.onClick.AddListener(() => 
        {
            PlayerStatus.SetActive(!PlayerStatus.activeSelf);
            StatusBottom.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = PlayerStatus.activeSelf ? "Close" : "Open";
        });
        
        playerDamageLevelUpButtom.onClick.AddListener(() =>
            {
                if (playerLevelUpPoint > 0)
                {
                    playerDamage += 1;
                    playerLevelUpPoint -= 1;
                }
            }
        );
        
        playerHealthLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerHealth += 10;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerAttackSpeedLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerAttackSpeed -= 0.2f;
                playerLevelUpPoint -= 1;
            }
        });
        
        playerSpeedLevelUpButtom.onClick.AddListener(() =>
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
        if (playerLevelUp >= playerNextLevelUpExp)
        {
            LevelUp();
        }
        enemyKillText.text = $"Enemy Kill: {enemyKill}";
        playerSpeedStatusPointText.text = $"Player Speed: {_currentSpeed}";
        playerAttackSpeedStatusPointText.text = $"Player Attack Speed: {playerAttackSpeed}";
        playerHealthStatusPointText.text = $"Player Health: {playerHealth}";
        playerStatusPointText.text = $"Status point: {playerLevelUpPoint}";
        playerDamageStatusText.text = $"Player Damage: {playerDamage}";
        playerNextLevelText.text = $"Next levelup {playerLevelUp}/{playerNextLevelUpExp}";
        playerStatusText.text = $"Player Level: {playerLevel}";
        _currentSpeed = playerSpeed;
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
    }

    public void LevelUp()
    {
        if (playerLevelUp >= playerNextLevelUpExp)
        {
            playerLevel += 1;
            playerLevelUpPoint += 1;
            playerLevelUp = 0;
            playerNextLevelUpExp += 2;
        }
        
    }


}
