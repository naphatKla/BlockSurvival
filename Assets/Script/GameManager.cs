using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCountText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject howToPlayMenu;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private List<GameObject> otherUI;

    public int enemySpawned;
    public int enemyLeft;
    public float timeInGame;
    private Player _player;
    public bool isEnd;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        enemyLeft = 0;
        enemySpawned = 0;
        resumeButton.onClick.AddListener(() =>
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
        
        howToPlayButton.onClick.AddListener(() =>
        {
            howToPlayMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        });
        Invoke(nameof(HowToPlayPopUp),1.25f);
    }
    
    void Update()
    {
        SetTimeInGameText();
        PauseMenuHandle();

        if(isEnd) return;
        
        if( enemyLeft <= 0 && timeInGame >= 900)
        {
            isEnd = true;
            StartCoroutine(EndScenePopUp(winMenu));
        }
        
        if (_player.health <= 0)
        {
            isEnd = true;
            StartCoroutine(EndScenePopUp(loseMenu));
        }
        
    }
    
    private void SetTimeInGameText()
    {
        timeInGame += Time.deltaTime;
        timeCountText.text = "" + Mathf.FloorToInt(timeInGame / 60) + ":" + Mathf.FloorToInt(timeInGame % 60).ToString("00");
    }

    private void HowToPlayPopUp()
    {
        StartCoroutine(HowToPlayPop());
    }
    private void PauseMenuHandle()
    {
        if(timeInGame < 1.25f) return;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        if (pauseMenu.gameObject.activeSelf)
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
            return;
        }
        
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        
    }

    IEnumerator HowToPlayPop()
    {
        Animator animator = howToPlayMenu.GetComponent<Animator>();
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            foreach (var ui in otherUI)
            {
                ui.SetActive(false);
            }
            howToPlayMenu.SetActive(true);
            Time.timeScale = 1;
            yield return null;
        }
        
        while(howToPlayMenu.activeSelf && isEnd == false)
        {
            Time.timeScale = 0;
            yield return null;
        }
    }
    IEnumerator EndScenePopUp(GameObject endScene)
    {
        while (endScene.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            foreach (var ui in otherUI)
            {
                ui.SetActive(false);
            }
            endScene.SetActive(true);
            Time.timeScale = 1;
            yield return null;
        }
        Time.timeScale = 0;
    }
}
