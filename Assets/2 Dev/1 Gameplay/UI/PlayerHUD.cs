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
}