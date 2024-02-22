using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private AudioData clickSoundData;

    public void StartGame()
    {
        AudioManager.Instance.PlayUISFX(clickSoundData);
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void Options()
    {
        AudioManager.Instance.PlayUISFX(clickSoundData);
        optionsCanvas.SetActive(true);
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