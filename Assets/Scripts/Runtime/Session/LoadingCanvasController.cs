using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadingCanvasController : CanvasController
{
    public Animator loading;
    public UnityAction<bool> LoadingDisplayedAction;
    public void EnableLoadingCanvas()
    {
        ShowCanvas();
        loading.SetBool("load", true);
        LoadingDisplayedAction?.Invoke(true);
    }

    public void DisableLoadingCanvas()
    {
        HideCanvas();
        loading.SetBool("load", false);
        LoadingDisplayedAction?.Invoke(false);
    }
}
