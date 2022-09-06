using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch = 0.1f;

    [HideInInspector]
    public AudioSource source;

    public bool loop = false;

    public bool playOnAwake = false;

    public float minDistance = 0.2f;
    public float maxDistance = 1f;

}


public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [SerializeField] private Sound[] sounds;

    [SerializeField] private float volume = 1f;
    [SerializeField] private float soundTrackVolume = 1f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SetVolume();


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume * volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.playOnAwake)
            {
                s.source.Play();
            }
        }
    }

    public void Play(string name, bool fade = false, float seconds = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong name while playing sound: " + name);
            return;
        }

        if (!s.source.isPlaying)
        {

            s.source.volume = s.volume * volume;
            if (fade)
            {
                s.source.loop = s.loop;
                s.source.volume = s.volume * volume;
                FadeSound(seconds, s.source, s.volume);
            }
            s.source.Play();
        }
    }

    public void ForcePlay(string name, bool fade = false, float seconds = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong name while playing sound: " + name);
            return;
        }



        s.source.volume = s.volume * volume;
        if (fade)
        {
            s.source.loop = s.loop;
            s.source.volume = s.volume * volume;
            FadeSound(seconds, s.source, s.volume);
        }
        s.source.Play();
    }

    public void ForceLoopPlay(string name, GameObject gameObject, bool fade = false, float seconds = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong name while playing sound: " + name);
            return;
        }


        if (gameObject.GetComponent<AudioSource>() == null)
            s.source = gameObject.AddComponent<AudioSource>();
        else
            s.source = gameObject.GetComponent<AudioSource>();

        if (s.source.clip != s.clip)
        {
            s.source.clip = s.clip;
            s.source.volume = s.volume * soundTrackVolume;
            s.source.loop = s.loop;
            s.source.Play();
        }
    }

    public void PlayInObject(string name, GameObject gameObject)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong name while playing sound: " + name);
            return;
        }

        if (gameObject.GetComponent<AudioSource>() == null)
            s.source = gameObject.AddComponent<AudioSource>();
        else
            s.source = gameObject.GetComponent<AudioSource>();
        s.source.spatialBlend = 1f;
        s.source.clip = s.clip;
        s.source.volume = s.volume;

        s.source.pitch = s.pitch;
        s.source.maxDistance = 10f;
        s.source.loop = s.loop;

        s.source.minDistance = s.minDistance;
        s.source.maxDistance = s.maxDistance;

        s.source.Play();
    }


    /*public void JustFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Wrong name while playing sound: " + name);
            return;
        }

        s.source.loop = s.loop;
        if (s.name == "Soundtrack")
            s.source.volume = s.volume * soundTrackVolume * 50f;
        else
            s.source.volume = s.volume * volume * 50f;
        FadeSound(1f, s.source, s.volume);
    }*/


    [SerializeField] private float fadeTime = 1f; // fade time in seconds
    void FadeSound(float seconds, AudioSource source, float vol)
    {
        if (fadeTime == 0)
        {
            source.volume = 0;
            return;
        }
        StartCoroutine(delayFade(seconds, source, vol));
    }

    public void SetVolume()
    {
        volume = PlayerPrefs.GetFloat("SFXvolume", 1f);
        soundTrackVolume = PlayerPrefs.GetFloat("Musicvolume", 1f);
    }


    IEnumerator delayFade(float seconds, AudioSource source, float vol)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(_FadeSound(source, vol));
    }

    IEnumerator _FadeSound(AudioSource source, float vol)
    {
        float t = vol;
        while (t > 0)
        {
            yield return null;
            t -= 0.0005f;
            source.volume = t / fadeTime;
        }
        source.loop = false;
        yield break;
    }


}
