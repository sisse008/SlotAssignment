using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] PopupController popup;
    [SerializeField] MainCanvas mainCanvas;

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
    }

    private void OnDisable()
    {
        popup.PopupDisplayedAction -= mainCanvas.ToggleCanvas;
    }

    public void ShowWinningPopup()
    {
        popup.EnablePopup();
    }
}
