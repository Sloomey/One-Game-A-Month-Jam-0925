using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    
    private SoundsManager soundsManager;
    
    public static bool GameIsPaused = false;
    public AudioMixer audioMixer;
    public GameObject bgImage_black;
    public GameObject bgImage_paused;
    public GameObject bgImage_options;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject mainMenuConfirm;
    public GameObject quitGameConfirm;
    public Button mainMenuConfirmButton;
    public GameObject player;

    public bool canPause = true;
    public bool enableMovementOnResume = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        soundsManager = SoundsManager.instance;
    }

    void Update()
    {
        if (playerControls.Player.PauseMenu.triggered) 
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Resume() 
    {
        SoundsManager_UI.instance.Play("resume");
        bgImage_black.SetActive(false);
        bgImage_paused.SetActive(false);
        bgImage_options.SetActive(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenuConfirm.SetActive(false);
        quitGameConfirm.SetActive(false);
        
        if(enableMovementOnResume) { player.GetComponent<PlayerActions_v2>().canMove = true; }

        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause() 
    {
        if (canPause)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            bgImage_black.SetActive(true);
            bgImage_paused.SetActive(true);
            player.GetComponent<PlayerActions_v2>().canMove = false;

            SoundsManager_UI.instance.Play("pause");
            AudioListener.pause = true;
            GameIsPaused = true;

            Cursor.lockState = CursorLockMode.Confined; //so mouse doesnt disappear when we click
            Cursor.visible = true;
        }
    }
    
    public void Open_PauseMenu()
    {
        SoundsManager_UI.instance.Play("pageback");
        
        pauseMenu.SetActive(true);
        bgImage_paused.SetActive(true);
        bgImage_options.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenuConfirm.SetActive(false);
        quitGameConfirm.SetActive(false);
    }

    public void Open_OptionsMenu()
    {
        SoundsManager_UI.instance.Play("pagenext");
        
        optionsMenu.SetActive(true);
        bgImage_options.SetActive(true);
        bgImage_paused.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void Open_MainMenuConfirm()
    {
        SoundsManager_UI.instance.Play("pagenext");
        mainMenuConfirm.SetActive(true);
    }
    
    public void Open_QuitGameConfirm()
    {
        SoundsManager_UI.instance.Play("pagenext");
        quitGameConfirm.SetActive(true);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenu_FadeOut());
    }

    IEnumerator LoadMainMenu_FadeOut()
    {
        mainMenuConfirmButton.enabled = false;
        Time.timeScale = 1;
        SoundsManager_UI.instance.Play("click");
        GetComponent<AudioManager_v2>().SnapshotOff(); //fade out audio
        
        yield return new WaitForSeconds(1f);
        
        AudioListener.pause = false;
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame() //NOTE: this doesn't work with webgl build, only standalone.
    {      
        Application.Quit();
    }
    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}