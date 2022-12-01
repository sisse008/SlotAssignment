using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{

    public SpinButtonController slotButton;

    [SerializeField] float AutoSpinDuration = 5f;

    private void OnEnable()
    {
        slotButton.SpinPressed += Spin;
        slotButton.StopPressed += Stop;
        slotButton.AutoSpinPressed += AutoSpin;
    }

    private void OnDisable()
    {
        slotButton.SpinPressed -= Spin;
        slotButton.StopPressed -= Stop;
        slotButton.AutoSpinPressed -= AutoSpin;
    }

    private void Start()
    {
        Stop();
    }

    public enum SlotMode
    {
        IDLE,
        SPINNING
    };

    [SerializeField] SlotMode currentSlotMode;
    public SlotMode CurrentSlotMode => currentSlotMode;

    public void Spin()
    {
        currentSlotMode = SlotMode.SPINNING;
        slotButton.ChangeToStopState();
    }

    public void Stop()
    {
        currentSlotMode = SlotMode.IDLE;
        slotButton.ChangeToSpinState();
    }

    public void AutoSpin()
    {
        StartCoroutine(SpinForSeconds(AutoSpinDuration));
        slotButton.ChangeToAutoState();
    }

    IEnumerator SpinForSeconds(float spinTime)
    {
        Spin();
        yield return new WaitForSeconds(spinTime);
        Stop();
    }

}
