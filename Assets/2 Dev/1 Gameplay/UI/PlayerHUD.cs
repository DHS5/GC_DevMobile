using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] TextMeshProUGUI scoreText;

    void Update()
    {
        healthBar.fillAmount = GameManager.Instance.player.GetHealthNormalized();
        scoreText.text = $"Score: {GameManager.Instance.GetScore()}";
    }
}