using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    internal AudioSource _source;

    [SerializeField] private string name;
    public string Name { get => name; set => name = value; }

    [SerializeField] private AudioClip _clip;
    public AudioClip Clip { get => _clip; set => _clip = value; }

    [SerializeField]
    [Range(0f, 1f)]
    private float _volume;
    public float Volume { get => _volume; set => _volume = value; }

    [SerializeField]
    [Range(0.1f, 3f)]
    private float _pitch;
    public float Pitch { get => _pitch; set => _pitch = value; }

    [SerializeField] private bool loop;
    public bool Loop { get => loop; set => loop = value; }



}
