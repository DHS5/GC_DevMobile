using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private AudioData themeMusic;
    [SerializeField] private AudioSource musicSource;

    [Space(5f)]

    [SerializeField] private AudioSource shootSource;
    [SerializeField] private AudioSource damageSource;
    [SerializeField] private AudioSource explosionSource;

    [Header("Default values")]
    [SerializeField] private bool _musicOn = true;
    [SerializeField] private bool _soundOn = true;
    [Space(10f)]
    [SerializeField][Range(0f, 1f)] private float _musicVolume = 0.5f;
    [SerializeField][Range(0f, 1f)] private float _soundVolume = 0.5f;

    private AudioData _currentMusic;
    private AudioData _currentShootSound;
    private AudioData _currentDamageSound;
    private AudioData _currentExplosionSound;


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
        musicSource.mute = _musicOn;

        shootSource.mute = _soundOn;
        damageSource.mute = _soundOn;
        explosionSource.mute = _soundOn;
    }

    private void Start()
    {
        PlayMusic(themeMusic);
    }

    public void PlayMusic(AudioData data)
    {
        _currentMusic = data;
        musicSource.clip = data.Clip;
        musicSource.Play();
    }

    public void PlayShootSFX(AudioData data)
    {
        if (_currentShootSound == data) return;

        _currentShootSound = data;
        shootSource.clip = data.Clip;
        shootSource.Play();
        shootSource.volume = _soundVolume * _currentShootSound.Volume();
    }
    public void PlayDamageSFX(AudioData data)
    {
        if (_currentDamageSound == data) return;

        _currentDamageSound = data;
        damageSource.clip = data.Clip;
        damageSource.Play();
        damageSource.volume = _soundVolume * _currentDamageSound.Volume();
    }
    public void PlayExplosionSFX(AudioData data)
    {
        if (_currentExplosionSound == data) return;

        _currentExplosionSound = data;
        explosionSource.clip = data.Clip;
        explosionSource.Play();
        explosionSource.volume = _soundVolume * _currentExplosionSound.Volume();
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
        musicSource.volume = _musicVolume * _currentMusic.Volume();
    }
    public void SetSFXVolume(float iVolume)
    {
        _soundVolume = iVolume;
        shootSource.volume = _soundVolume * _currentShootSound.Volume();
        damageSource.volume = _soundVolume * _currentDamageSound.Volume();
        explosionSource.volume = _soundVolume * _currentExplosionSound.Volume();
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
