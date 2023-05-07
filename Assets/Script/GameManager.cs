using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCountText;
    public int enemySpawned;
    public int enemyLeft;
    public float timeInGame;

    void Start()
    {
        enemyLeft = 0;
        enemySpawned = 0;
    }
    
    void Update()
    {
        SetTimeInGameText();
    }
    
    private void SetTimeInGameText()
    {
        timeInGame += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(timeInGame / 60) + ":" + Mathf.FloorToInt(timeInGame % 60).ToString("00");
    }
}
