using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds = new List<Sound>();

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.priority = s.priority;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Stop();
    }
}
