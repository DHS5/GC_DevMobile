using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    private bool isGodMod = false;
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
        Optimization.GCCollect();
        UpdateManager.IsActive = false;
    }

    private void Resume()
    {
        isPaused = false;
        GamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        Optimization.GCCollect();
        OnGameResume?.Invoke();
        UpdateManager.IsActive = true;
    }

    public void Options()
    {
        AudioManager.Instance.PlayShootSFX(clickSoundData);
        optionsCanvas.SetActive(true);
        otherCanvas.SetActive(false);
    }

    public void TogglePause()
    {
        AudioManager.Instance.PlayUISFX(clickSoundData);
        if (isPaused)
            Resume();
        else
            Pause();
    }
    public void ToggleGodMod()
    {
        AudioManager.Instance.PlayUISFX(clickSoundData);
        if (!isGodMod)
            ActivateGodMod();
        else
            DesactivateGodMod();
    }

    private void ActivateGodMod()
    {
        isGodMod = true;
        GameManager.Instance.isPlayerGodMode = true;
    }

    private void DesactivateGodMod()
    {
        isGodMod = false;
        GameManager.Instance.isPlayerGodMode = false;
    }


    public void ExitGame()
    {
        AudioManager.Instance.PlayUISFX(clickSoundData);
        GameManager.Instance.QuitGame();
        SceneManager.LoadScene(gameSceneName);
    }
}