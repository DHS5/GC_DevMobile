using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private GameObject optionsCanvas;

    public void StartGame()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
    public void Options()
    {
        AudioManager.Instance.PlaySFX("ClickButton");
        optionsCanvas?.SetActive(true);
        gameObject.SetActive(false);
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
