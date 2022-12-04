using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupController : CanvasController
{

    public UnityAction<bool> PopupDisplayedAction;
    [SerializeField] AudioSource popupSound;

    public void EnablePopup()
    {
        ShowCanvas();
        popupSound.Play();
        PopupDisplayedAction?.Invoke(true);
    }

    public void DisablePopup()
    {
        HideCanvas();
        popupSound.Stop();
        PopupDisplayedAction?.Invoke(false);
    }
}
