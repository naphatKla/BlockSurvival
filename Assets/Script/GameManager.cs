using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCountText;
    public float timeInGame;

    void Start()
    {
        
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
