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

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        OnGamePause?.Invoke();
    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        OnGameResume?.Invoke();
    }

    public void TogglePause()
    {
        Debug.Log("PAUSE toggle");
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
