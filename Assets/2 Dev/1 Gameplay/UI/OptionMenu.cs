using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private GameObject previousMenu;

    [SerializeField] private AudioData toggleAudio;
    [SerializeField] private AudioData sliderAudio;

    private void Start()
    {
        musicSlider.value = AudioManager.Instance.GetMusicVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
        musicToggle.isOn = AudioManager.Instance.IsMusicEnabled();
        sfxToggle.isOn = AudioManager.Instance.AreSFXEnabled();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.PlayUISFX(toggleAudio);
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.PlayUISFX(toggleAudio);
        AudioManager.Instance.ToggleSFX();
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.PlayUISFX(sliderAudio);
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.Instance.PlayUISFX(sliderAudio);
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    public void exitOptions()
    {
        AudioManager.Instance.PlayUISFX(toggleAudio);
        previousMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}