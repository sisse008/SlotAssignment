using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupController : CanvasController
{

    public UnityAction<bool> PopupDisplayedAction;
    [SerializeField] AudioSource popupSound;

    [SerializeField] TMP_Text message;

    public void EnablePopup(string text = "")
    {
        ShowCanvas();
        message.text = text;
        popupSound.Play();
        PopupDisplayedAction?.Invoke(true);
    }

    public void DisablePopup()
    {
        HideCanvas();
        popupSound.Stop();
        message.text = string.Empty;
        PopupDisplayedAction?.Invoke(false);
    }
}
