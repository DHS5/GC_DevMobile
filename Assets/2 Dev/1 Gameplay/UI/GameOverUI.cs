using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private AudioData audioData;

    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start()
    {
        SetScore(GameManager.Instance.GetScore());
    }

    public void SetScore(int score)
    {
        scoreText.text = $"Total Score: \n{score}";
        AudioManager.Instance.PlayShootSFX(audioData);
    }

}
