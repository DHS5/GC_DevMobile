using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        healthBar.fillAmount = GameManager.Instance.Player.GetHealthNormalized();
        scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
    }
}