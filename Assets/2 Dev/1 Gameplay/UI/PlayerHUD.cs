using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    #region Singleton

    public static PlayerHUD Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Init();
    }

    #endregion

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI FPSText;
    [SerializeField] private TextMeshProUGUI BulletCountText;
    [SerializeField] private TextMeshProUGUI RamUsageText;

    private void Init()
    {
        SetHealth(1f);
        SetScore(0);
    }

    public void SetHealth(float normalizedHealth)
    {
        healthBar.fillAmount = normalizedHealth;
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void SetFPS(float fps)
    {
        FPSText.text = $"FPS: {fps:0.0}";
    }

    public void SetBulletCount(int maxBullet)
    {
        BulletCountText.text = $"Bullet Count: {maxBullet}";
    }

    public void SetRamUsage(string getMemoryUsage)
    {
        RamUsageText.text = getMemoryUsage;
    }
}