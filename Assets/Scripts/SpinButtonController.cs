using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class SpinButtonController : BabaButton
{
    public SlotController controlledSlot;

    public enum MainButtonState
    {
        SPIN,
        STOP,
        //TODO: AUTO_SPIN
    };

    [SerializeField] MainButtonState currentButtonState;
    MainButtonState CurrentButtonState => currentButtonState;

    private void OnEnable()
    {
        shortPress += ShortPress;
    }
    private void OnDisable()
    {
        shortPress -= ShortPress;
    }
    void ShortPress()
    {
        MainButtonState _currentButtonState = currentButtonState;

        if (_currentButtonState == MainButtonState.SPIN)
        {
            controlledSlot.Spin();
            ChangeButtonState(MainButtonState.STOP);
        }
        else if (_currentButtonState == MainButtonState.STOP)
        {
            controlledSlot.Stop();
            ChangeButtonState(MainButtonState.SPIN);
        }
    }
    void ChangeButtonState(MainButtonState newState)
    {
        currentButtonState = newState;
        //TODO: change sprite
    }
   

}
