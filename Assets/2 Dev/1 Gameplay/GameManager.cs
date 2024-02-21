using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] private Player player;
     
    public Player Player => player;
    public bool GamePaused;

    public static GameManager Instance { get; private set; }
    
    public static event Action OnGameOver;

    int score;
    
    // TODO Add a next level instead of game over

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        DOVirtual.DelayedCall(3, OnGameEnd);
    }
    
    private void OnGameEnd()
    {
        // TODO
    }

    private void OnEnable()
    {
        PauseMenu.OnGamePause += GamePause;
        PauseMenu.OnGameResume += GameResume;
    }
    private void OnDisable()
    {
        PauseMenu.OnGamePause -= GamePause;
        PauseMenu.OnGameResume -= GameResume;
    }

    private void GamePause()
    {
        GamePaused = true;
    }

    private void GameResume()
    {
        GamePaused = false;
    }

    public void AddScore(int amount) => score += amount;
    public int GetScore() => score;
}