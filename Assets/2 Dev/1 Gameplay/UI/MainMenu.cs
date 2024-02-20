using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si nous sommes dans un build exécutable, quittez l'application
        Application.Quit();
#endif
    }
}
