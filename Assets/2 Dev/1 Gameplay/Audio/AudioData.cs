using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] [Range(0, 1)] private float relativeVolume;

    public AudioClip Clip => clip;
    public float RelativeVolume => relativeVolume;
}