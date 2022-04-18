using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public SoundPool[] soundPools;

    public static AudioManager instance;

    void Awake()
    {
        // Make sure there is only one AudioManager
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (SoundPool sp in soundPools)
        {
            foreach (Sound s in sp.sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlayFromPool(string poolName)
    {
        SoundPool sp = Array.Find(soundPools, soundPool => soundPool.poolName == poolName);
        if (sp == null)
        {
            Debug.LogWarning("SoundPool: " + poolName + " not found!");
            return;
        }

        if(sp.sounds.Length == 0)
        {
            Debug.LogWarning("SoundPool: " + poolName + " is empty!");
            return;
        }

        Sound s = sp.sounds[UnityEngine.Random.Range(0, sp.sounds.Length)];

        s.source.Play();
    }
}
