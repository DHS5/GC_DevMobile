using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Player player;

    public Player Player => player;

    public static GameManager Instance { get; private set; }

    public static event Action OnGameOver;

    private int score;

    // TODO Add a next level instead of game over

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        if (gameOverUI) gameOverUI.SetActive(true);
        Time.timeScale = 0;
        Clean();
        DOVirtual.DelayedCall(3, OnGameEnd);
    }

    private void Clean()
    {
        BulletManager.Clear();
        EnemyManager.Clear();
    }

    private void OnGameEnd()
    {
        UpdateManager.Clear();
        
        SceneManager.LoadScene("MainMenu");
    }


    public void AddScore(int amount)
    {
        score += amount;
        PlayerHUD.Instance.SetScore(score);
    }

    public int GetScore()
    {
        return score;
    }
}