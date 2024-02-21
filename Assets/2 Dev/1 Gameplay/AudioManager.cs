using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public static AudioManager Instance;
    
    public Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound _sound = Array.Find(musicSounds, x => x.name == name);

        if (_sound == null)
        {
            Debug.LogError("Sound Not Found");
        }
        else
        {
            musicSource.clip = _sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound _sound = Array.Find(sfxSounds, x => x.name == name);

        if (_sound == null)
        {
            Debug.LogError("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(_sound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public bool IsMusicEnabled()
    {
        return musicSource.mute;
    }

    public bool AreSFXEnabled()
    {
        return sfxSource.mute;
    }

    public void MusicVolume(float iVolume)
    {
        musicSource.volume = iVolume;
    }
    public void SFXVolume(float iVolume)
    {
        sfxSource.volume = iVolume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
    public float GetSFXVolume()
    {
        return sfxSource.volume;
    }
}
