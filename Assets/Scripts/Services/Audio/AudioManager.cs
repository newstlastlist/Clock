using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(Sounds type)
    {
        var sound = Array.Find(_sounds , sound => sound.type == type);
        if (sound != null)
            sound.source.Play();
    }
    
    public enum Sounds
    {
        TimerFinish
    }
}
