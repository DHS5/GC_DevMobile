using DG.Tweening;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    public UnityEvent OnPause;
    public UnityEvent OnResume;

    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        OnPause.Invoke();
    }

    private void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        OnResume.Invoke();
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
