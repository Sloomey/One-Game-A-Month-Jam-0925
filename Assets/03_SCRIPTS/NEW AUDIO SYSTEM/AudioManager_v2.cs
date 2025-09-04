using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager_v2 : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    private AudioMixerSnapshot soundOff; 
    private AudioMixerSnapshot soundOn;
    private AudioMixerSnapshot sfxOff; 
    private AudioMixerSnapshot sfxOn;

    public bool customSnapshotActive = false;

	void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        soundOff = mixer.FindSnapshot("off");
        soundOn = mixer.FindSnapshot("on");
        
        sfxOff = mixer.FindSnapshot("sfxOff");
        sfxOn = mixer.FindSnapshot("sfxOn");

        if (!customSnapshotActive)
        {
            SnapshotOn(); //fade in the MasterVolume mixer
        }
        else
        {
            SnapshotOn_SFX(); //fade in only sfxEnvironment
        }
	}

    public void SetSFXVolume(float sfxValue)
    {
        mixer.SetFloat("sfxVol", Mathf.Log10(sfxValue) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxValue);
    }

    public void SetMusicVolume(float musicValue)
    {
        mixer.SetFloat("musicVol", Mathf.Log10(musicValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
    }

    public void SetMasterVolume(float masterValue)
    {
        mixer.SetFloat("masterVolAdjustable", Mathf.Log10(masterValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", masterValue);
    }

    public void SnapshotOff()
    {
        soundOff.TransitionTo(1f); //was 1.5
    }

    public void SnapshotOn()
    {
        soundOn.TransitionTo(1f);
    }

    public void SnapshotOff_SFX() //only fades sfx environment mixer, not master
    {
        sfxOff.TransitionTo(1f);
    }
    
    public void SnapshotOn_SFX()
    {
        sfxOn.TransitionTo(1f);
    }
}