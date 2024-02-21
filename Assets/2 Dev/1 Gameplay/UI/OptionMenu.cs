using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private GameObject previousMenu;

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

    public void SetMusicVolume()
    {
        //Debug.Log(musicSlider.value);
        AudioManager.Instance.MusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.Instance.SFXVolume(sfxSlider.value);
    }

    public void exitOptions()
    {
        previousMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
