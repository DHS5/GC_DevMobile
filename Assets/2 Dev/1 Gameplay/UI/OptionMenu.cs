using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        musicToggle.isOn = AudioManager.Instance.IsMusicEnabled();
        sfxToggle.isOn = AudioManager.Instance.AreSFXEnabled();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.MusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SFXVolume(volume);
    }

    public void exitOptions()
    {
        mainMenu?.SetActive(true);
        gameObject.SetActive(false);
    }
}
