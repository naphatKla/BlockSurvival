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

    [SerializeField] private TextMeshProUGUI playerStatusText;
    [SerializeField] private Button openStatusBottom;
    [SerializeField] private TextMeshProUGUI buttomOpenStatusText;
    [SerializeField] private GameObject PlayerStatus;
    [SerializeField] private Button StatusBottom;
    [SerializeField] private TextMeshProUGUI playerStatusPointText;
    
    [SerializeField] private TextMeshProUGUI playerDamageStatusText;
    [SerializeField] private Button playerDamageLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerHealthStatusPointText;
    [SerializeField] private Button playerHealthLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerATKStatusPointText;
    [SerializeField] private Button playerATKLevelUpButtom;
    [SerializeField] private TextMeshProUGUI playerSpeedStatusPointText;
    [SerializeField] private Button playerSpeedLevelUpButtom;
    [SerializeField] private TextMeshProUGUI enemyKillText;
    
    
    [SerializeField] public float playerLevelUpPoint;
    private Bullet _bullet;
    private CombatSystem _combatSystem;
    public float playerDamage = 1;
    public float playerHealth = 100;
    public float playerATK;
    public float enemyKill;
    


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
        
        playerATKLevelUpButtom.onClick.AddListener(() =>
        {
            if (playerLevelUpPoint > 0)
            {
                playerATK -= 0.5f;
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
        enemyKillText.text = $"Enemy Kill: {enemyKill}";
        playerSpeedStatusPointText.text = $"Player Speed {_currentSpeed}";
        playerATKStatusPointText.text = $"Player ATK {playerATK}";
        playerHealthStatusPointText.text = $"Player Health {playerHealth}";
        playerStatusPointText.text = $"Status point: {playerLevelUpPoint}";
        playerDamageStatusText.text = $"Player Damage {playerDamage}";
        playerStatusText.text = $"Player Level {playerLevel}";
        _currentSpeed = playerSpeed;
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
    }
    
    
    
}
