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
    


    private float _currentSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StatusBottom.onClick.AddListener(() => PlayerStatus.SetActive(true));
        playerStatusText.text = $"Player Level {playerLevel}";
        _currentSpeed = playerSpeed;
        Vector2 playerVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _currentSpeed;
        playerRigidbody2D.velocity = playerVelocity;
        
    }
    
    
}
