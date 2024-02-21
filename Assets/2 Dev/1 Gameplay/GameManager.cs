using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] private Player player;
     
    public Player Player => player;

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
        if (gameOverUI) gameOverUI.SetActive(true);
        Time.timeScale = 0;
        DOVirtual.DelayedCall(3, OnGameEnd);
    }
    
    private void OnGameEnd()
    {
        // TODO
    }


    public void AddScore(int amount) => score += amount;
    public int GetScore() => score;
}