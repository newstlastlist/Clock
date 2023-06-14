using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;
    private void Awake()
    {
        foreach (Sound s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        DontDestroyOnLoad(gameObject);
    }
   
    public void Play(Sounds type)
    {
        var s = Array.Find(_sounds , s => s.type == type);

        if (s != null)
            s.source.Play();
    }
    
    public enum Sounds
    {
        TimerFinish
    }
}
