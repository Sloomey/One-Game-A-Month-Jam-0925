using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundsManager : MonoBehaviour
{
    #region Singleton
    public static SoundsManager instance;
    
    public Sound[] sounds;
    public AudioMixerGroup sfxAudioMixer;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of SoundsManager found!"); 
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = sfxAudioMixer;
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    
    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
    #endregion
}