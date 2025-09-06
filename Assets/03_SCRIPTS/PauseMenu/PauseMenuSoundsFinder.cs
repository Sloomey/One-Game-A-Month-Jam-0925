using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSoundsFinder : MonoBehaviour
{
    public void PlayUIClick()
    {
        SoundsManager_UI.instance.Play("UI_Click_1");
    }

    public void PlayUIPause()
    {
        SoundsManager_UI.instance.Play("UI_Pause");
    }

    public void PlayUIResume()
    {
        SoundsManager_UI.instance.Play("UI_Resume");
    }
}