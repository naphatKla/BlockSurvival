using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCountText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    public int enemySpawned;
    public int enemyLeft;
    public float timeInGame;

    void Start()
    {
        enemyLeft = 0;
        enemySpawned = 0;
        resumeButton.onClick.AddListener(() =>
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
    }
    
    void Update()
    {
        SetTimeInGameText();
        PauseMenuHandle();
    }
    
    private void SetTimeInGameText()
    {
        timeInGame += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(timeInGame / 60) + ":" + Mathf.FloorToInt(timeInGame % 60).ToString("00");
    }

    private void PauseMenuHandle()
    {
        if(timeInGame < 2) return;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
