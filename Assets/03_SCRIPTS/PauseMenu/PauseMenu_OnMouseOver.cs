using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu_OnMouseOver : MonoBehaviour, IPointerEnterHandler
{
    public GameObject menuScript;

    //setting the selected game object to this and enabling bg image.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(EventSystem.current.currentSelectedGameObject != this.gameObject)
        {       
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }
}