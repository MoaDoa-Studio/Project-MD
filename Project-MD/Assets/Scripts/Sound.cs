using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{

    public string sound_Name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;

    public bool loop;

    public AudioSource audioSource;
}
