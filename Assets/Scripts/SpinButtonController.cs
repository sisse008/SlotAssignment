using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SpinButtonController : BabaButton
{

    public UnityAction SpinPressed;
    public UnityAction StopPressed;
    public UnityAction AutoSpinPressed;


    [SerializeField] TMP_Text text;
    enum MainButtonState
    {
        SPIN,
        STOP,
        AUTO_SPIN
    };

    private static Dictionary<MainButtonState, string> buttonTexts = new Dictionary<MainButtonState, string>()
    {
        {MainButtonState.SPIN, "Spin" },
        {MainButtonState.STOP, "Stop" },
        {MainButtonState.AUTO_SPIN, "AutoSpin"}
    };
    [SerializeField] MainButtonState currentButtonState;
    MainButtonState CurrentButtonState => currentButtonState;

    private void OnEnable()
    {
        shortPress += OnShortPress;
        longPressUp += OnLongPressUp;
      
    }
    private void OnDisable()
    {
        shortPress -= OnShortPress;
        longPressUp -= OnLongPressUp;
       
    }
    void OnLongPressUp()
    {
        if (currentButtonState == MainButtonState.STOP)
            return;
        AutoSpinPressed?.Invoke();
      
    }
    void OnShortPress()
    {
        MainButtonState _currentButtonState = currentButtonState;

        if (_currentButtonState == MainButtonState.SPIN)
        {
            SpinPressed?.Invoke();
        }
        else if (_currentButtonState == MainButtonState.STOP)
        {
            StopPressed?.Invoke();
        }
    }
    void ChangeButtonState(MainButtonState newState)
    {
        currentButtonState = newState;
        text.text = buttonTexts[newState];
    }

    public void ChangeToStopState() => ChangeButtonState(MainButtonState.STOP);
  
    public void ChangeToSpinState() => ChangeButtonState(MainButtonState.SPIN);
  
    public void ChangeToAutoState() => ChangeButtonState(MainButtonState.AUTO_SPIN);
 
}
