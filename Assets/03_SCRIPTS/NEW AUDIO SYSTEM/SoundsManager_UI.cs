using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundsManager_UI : MonoBehaviour
{
    #region Singleton
    public static SoundsManager_UI instance;
    
    public Sound[] sounds;
    public AudioMixerGroup sfxAudioMixer;
    
    [SerializeField] private bool ignorePause = true;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Debug.LogWarning("More than one instance of SoundsManager_UI found!"); 
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = sfxAudioMixer;
            s.source.ignoreListenerPause = ignorePause; //still plays audio during pause menu
            s.source.playOnAwake = false;
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
    #endregion
}