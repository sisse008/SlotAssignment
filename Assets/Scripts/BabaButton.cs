using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BabaButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    protected UnityAction shortPress;
    protected UnityAction longPressUp;
    protected UnityAction longPressHold;

    [SerializeField] float holdTimeThreshHold = 2f;

    Coroutine counter;

    bool longPress;

    public void OnPointerDown(PointerEventData eventData)
    {
        counter = StartCoroutine(CountSeconds()) ;
    }
    IEnumerator CountSeconds()
    {
        float secs=0;
        longPress = false;
        while (true)
        {
            secs += Time.deltaTime;
            if (secs > holdTimeThreshHold && !longPress)
            {
                longPress = true;
                longPressHold?.Invoke();
            }   
            yield return null;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(counter);
        if (longPress)
            longPressUp?.Invoke();
        else
            shortPress?.Invoke();

       
    }
}
