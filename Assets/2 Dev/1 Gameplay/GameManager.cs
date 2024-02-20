using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject PlayerGameObject;

    public static GameManager Instance { get; private set; }

    public Player player { get; set; }
    int score;
    float restartTimer = 3f;

    public bool IsGameOver() => player.GetHealthNormalized() <= 0;
    // TODO Add a next level instead of game over

    void Awake()
    {
        Instance = this;
        player = PlayerGameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (IsGameOver())
        {
            restartTimer -= Time.deltaTime;

            if (gameOverUI.activeSelf == false)
            {
                gameOverUI.SetActive(true);
            }

            if (restartTimer <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void AddScore(int amount) => score += amount;
    public int GetScore() => score;
}