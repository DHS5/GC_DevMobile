using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("References")] [SerializeField]
    private AudioData themeMusic;

    [SerializeField] private AudioSource musicSource;

    [Space(5f)][SerializeField] private AudioSource UISource;
    [SerializeField] private AudioSource shootSource;
    [SerializeField] private AudioSource damageSource;
    [SerializeField] private AudioSource explosionSource;

    [Header("Clips")]
    [SerializeField] private AudioData shootAudioData;
    [SerializeField] private AudioData damageAudioData;
    [SerializeField] private AudioData explosionAudioData;

    [Header("Default values")] [SerializeField]
    private bool _musicOn = true;

    [SerializeField] private bool _soundOn = true;

    [Space(10f)] [SerializeField] [Range(0f, 1f)]
    private float _musicVolume = 0.5f;

    [SerializeField] [Range(0f, 1f)] private float _soundVolume = 0.5f;

    private AudioData _currentUISound;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        musicSource.mute = !_musicOn;

        shootSource.mute = !_soundOn;
        UISource.mute = !_soundOn;
        damageSource.mute = !_soundOn;
        explosionSource.mute = !_soundOn;

        SetSFXVolume(_soundVolume);
        SetMusicVolume(_musicVolume);
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = themeMusic.Clip;
        musicSource.Play();
    }

    public void PlayShootSFX()
    {
        shootSource.PlayOneShot(shootAudioData.Clip);
    }

    public void PlayUISFX(AudioData data)
    {
        if (_currentUISound == data)
        {
            UISource.Play();
            return;
        }

        _currentUISound = data;
        UISource.clip = data.Clip;
        UISource.Play();
        UISource.volume = _soundVolume * _currentUISound.Volume();
    }

    public void PlayDamageSFX()
    {
        damageSource.PlayOneShot(damageAudioData.Clip);
    }

    public void PlayExplosionSFX()
    {
        explosionSource.PlayOneShot(explosionAudioData.Clip);
    }

    public void ToggleMusic()
    {
        _musicOn = !_musicOn;
        musicSource.mute = !_musicOn;
    }

    public void ToggleSFX()
    {
        _soundOn = !_soundOn;
        shootSource.mute = !_soundOn;
        damageSource.mute = !_soundOn;
        explosionSource.mute = !_soundOn;
        UISource.mute = !_soundOn;
    }

    public bool IsMusicEnabled()
    {
        return _musicOn;
    }

    public bool AreSFXEnabled()
    {
        return _soundOn;
    }

    public void SetMusicVolume(float iVolume)
    {
        _musicVolume = iVolume;
        musicSource.volume = _musicVolume * themeMusic.Volume();
    }

    public void SetSFXVolume(float iVolume)
    {
        _soundVolume = iVolume;
        shootSource.volume = _soundVolume * shootAudioData.Volume();
        damageSource.volume = _soundVolume * damageAudioData.Volume();
        explosionSource.volume = _soundVolume * explosionAudioData.Volume();
        UISource.volume = _soundVolume * _currentUISound.Volume();
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public float GetSFXVolume()
    {
        return _soundVolume;
    }
}