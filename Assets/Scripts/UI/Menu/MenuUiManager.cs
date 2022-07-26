using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUiManager : MonoBehaviour
{
    public Button playBtn, quitBtn;

    private void OnEnable()
    {
        playBtn.transform.localScale = Vector2.zero;
        quitBtn.transform.localScale = Vector2.zero;

        PlayerMenuAnimationController.PlayerEntered += OnPlayerEntry;
    }
    private void OnDisable()
    {
        PlayerMenuAnimationController.PlayerEntered -= OnPlayerEntry;
    }

    void OnPlayerEntry()
    {
        SetupButtons();

        playBtn.transform.DOScale(1f, 0.5f);
        quitBtn.transform.DOScale(1f, 0.5f);
    }

    void SetupButtons()
    {
        playBtn.OnClick(OnClickPlay);
        quitBtn.OnClick(OnClickQuit);
    }

    void OnClickPlay()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitUntil(() => async.isDone);
    }

    void OnClickQuit()
    {
        Application.Quit();
    }
}