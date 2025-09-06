using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuButtonSelected : MonoBehaviour
{       
    public GameObject pauseMenuParent;
    public GameObject optionsMenuParent;
    public GameObject mainMenuConfirmParent;
    public GameObject quitGameConfirmParent;

    //first set menus active/inactive
    //clear the Event System's current selected object and then assign the new one.
    //set the currentSelectedButton
    public void PauseMenuSelect()
    {
        pauseMenuParent.SetActive(true);
        optionsMenuParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void MainMenuConfirm()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        mainMenuConfirmParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PauseMenuFromMainMenuConfirm()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        mainMenuConfirmParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGameConfirm()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        quitGameConfirmParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void PauseMenuFromQuitGameConfirm()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        quitGameConfirmParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    

    public void PauseMenuFromRestartLevelConfirm()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OptionsMenuSelect()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        pauseMenuParent.SetActive(false); //new
        optionsMenuParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void VideoMenuSelect()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        optionsMenuParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    //Back button
    public void OptionsMenuSelectFromVideo()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        optionsMenuParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void AudioMenuSelect()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        optionsMenuParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    //Back button
    public void OptionsMenuSelectFromAudio()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        optionsMenuParent.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ControlsMenuSelect()
    {
        GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager_UI>().Play("UI_Click_1");
        optionsMenuParent.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }


    
}