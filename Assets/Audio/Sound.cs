using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(.1f, 3)]
    public float pitch = 1;
    [Range(0, 1)]
    public float volume = 0.5f;

    [Range(0, 256)]
    public int priority;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
