using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private Sound[] _sounds;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (var s in _sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s.Clip;
            s._source.volume = s.Volume;
            s._source.pitch = s.Pitch;
            s._source.loop = s.Loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s._source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s._source.Stop();
    }

}
