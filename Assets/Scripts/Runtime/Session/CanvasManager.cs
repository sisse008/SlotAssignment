using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] PopupController popup;
    [SerializeField] MainCanvas mainCanvas;
    [SerializeField] LoadingCanvasController loadingCanvas;

    private static CanvasManager instance = null;
    public static CanvasManager Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<CanvasManager>();
            return instance;
        }
    }

    private void OnEnable()
    {
        popup.PopupDisplayedAction += mainCanvas.ToggleCanvas;
        loadingCanvas.LoadingDisplayedAction += mainCanvas.ToggleCanvas;
        loadingCanvas.LoadingDisplayedAction += popup.ToggleCanvas;
    }

    private void OnDisable()
    {
        popup.PopupDisplayedAction -= mainCanvas.ToggleCanvas;
        loadingCanvas.LoadingDisplayedAction -= mainCanvas.ToggleCanvas;
        loadingCanvas.LoadingDisplayedAction -= popup.ToggleCanvas;
    }


    public void ShowWinningPopup(string text = "")
    {
        popup.EnablePopup("$" + text);
    }
}
