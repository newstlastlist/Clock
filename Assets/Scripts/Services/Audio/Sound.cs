using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioManager.Sounds type;
    public AudioClip clip;
    public bool loop;
    [Range (0f, 1f)] public float volume = 1f;
    [Range (0.1f, 3f)] public float pitch = 1f;

    [HideInInspector] public AudioSource source;
}
