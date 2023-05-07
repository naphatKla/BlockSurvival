using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<ChangeSceneButton> changeSceneButtons;
    [SerializeField] private Image transitionIn;
    [SerializeField] private Image transitionOut;
    private Animator _transitionInanimator;
    private Animator _transitionOutanimator;
    public enum SceneName
    {
        MainMenu,
        Gameplay,
        Exit
    }

    [Serializable] public struct ChangeSceneButton
    {
        public Button button;
        public SceneName sceneName;
    }
    void Start()
    {
        _transitionInanimator = transitionIn.GetComponent<Animator>();
        _transitionOutanimator = transitionOut.GetComponent<Animator>();
        
        changeSceneButtons.ForEach(button => button.button.onClick.AddListener(() =>
        {
            StartCoroutine(ChangeScene(button.sceneName));
        }));

        StartCoroutine(StartScene());
    }

    private IEnumerator ChangeScene(SceneName sceneName)
    {
        Time.timeScale = 1;
        transitionOut.gameObject.SetActive(true);
        yield return new WaitUntil(() => _transitionOutanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        
        if (sceneName.Equals(SceneName.Exit))
        {
            Application.Quit();
            yield break;
        }
        SceneManager.LoadScene(sceneName.ToString());
    }

    private IEnumerator StartScene()
    {
        transitionIn.gameObject.SetActive(true);
        transitionOut.gameObject.SetActive(false);
        
        yield return new WaitUntil(() => _transitionInanimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f);
        
        transitionIn.gameObject.SetActive(false);
    }
}
