using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOnAwake : MonoBehaviour
{
    public GameObject menuScript;

    void Awake()
    {
        menuScript.GetComponent<PauseMenuButtonSelected>().PauseMenuSelect();   
    }
}    