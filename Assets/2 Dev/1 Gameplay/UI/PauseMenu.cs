using DG.Tweening;
using System;
using System.ComponentModel;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public static event Action OnGamePause;
    public static event Action OnGameResume;

    public static bool GamePaused { get; private set; }

    private void Pause()
    {
        isPaused = true;
        GamePaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        OnGamePause?.Invoke();
    }

    private void Resume()
    {
        isPaused = false;
        GamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        OnGameResume?.Invoke();
    }

    public void TogglePause()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
