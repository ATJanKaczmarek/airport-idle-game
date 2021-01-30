using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup mixerGroup;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            DontDestroyOnLoad(this);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    //private void Start()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex == 0)
    //        Play("");
    //}

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }

        s.source.Play();
    }
}
