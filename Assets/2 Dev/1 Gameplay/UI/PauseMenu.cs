using DG.Tweening;
using System;
using System.ComponentModel;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject otherCanvas;

    [SerializeField] private string gameSceneName = "MainMenu";
    [SerializeField] private AudioData clickSoundData;

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

    public void Options()
    {
        AudioManager.Instance.PlayShootSFX(clickSoundData);
        optionsCanvas.SetActive(true);
        otherCanvas.SetActive(false);
    }

    public void TogglePause()
    {
        AudioManager.Instance.PlayShootSFX(clickSoundData);
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlayShootSFX(clickSoundData);
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
}
