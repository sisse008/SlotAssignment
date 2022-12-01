using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class BabaButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    protected Action shortPress;
    protected Action longPress;
    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        shortPress();
        //TODO: add long press
        
    }
}
