using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;
    public GameObject pauseMenu;

    public float countdownTime;
    private float timerValue1 = 300f; //5 mins
    private float timerValue3 = 450f; //7.5 mins
    private float timerValue2 = 600f; //10 mins

    private void Awake()
    {
        Invoke("PlayMusic", 5f); //plays music after 5 seconds
        SetCountdownLength(); //reset value
    }

    private void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
        }
        else
        {
            PlayMusic();
            SetCountdownLength(); //reset value
        }
    }

    private void SetCountdownLength() //chooses countdown length at random
    {
        float[] possibleFloats = new float[] { timerValue1, timerValue2, timerValue3 };

        int randomIndex = Random.Range(0, possibleFloats.Length);
        countdownTime = possibleFloats[randomIndex];
    }

    private void PlayMusic() //check if any music is playing, if not then play something random
    {
        AudioSource[] audioSources = new AudioSource[] { music1, music2, music3 };
        int randomMusic = Random.Range(0, audioSources.Length);

        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                return;
            }
            else
            {
                audioSources[randomMusic].Play(); //play random track
            }
        }
    }

    public void StopMusic()
    {
        pauseMenu.GetComponent<AudioManager_v2>().SnapshotOff(); //fade out music
    }
}