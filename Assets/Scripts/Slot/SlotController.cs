using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{

    public SpinButtonController slotButton;

    public SlotSettings settings;

    public Transform reelsHolder;

    private void OnEnable()
    {
        slotButton.SpinPressed += Spin;
        slotButton.StopPressed += Stop;
        slotButton.AutoSpinPressed += AutoSpin;
        InitSlot();
    }

    private void OnDisable()
    {
        slotButton.SpinPressed -= Spin;
        slotButton.StopPressed -= Stop;
        slotButton.AutoSpinPressed -= AutoSpin;
    }
    private void InitSlot()
    {
        InitReels();
    }

    private void InitReels()
    {
        for (int i = 0; i < settings.NumOfReels; i++)
        {
            Instantiate(AssetsManager.Instance.reelPrefabAsset, reelsHolder);
        }
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
        StartCoroutine(SpinForSeconds(settings.AutoSpinDuration));
        slotButton.ChangeToAutoState();
    }

    IEnumerator SpinForSeconds(float spinTime)
    {
        Spin();
        yield return new WaitForSeconds(spinTime);
        Stop();
    }

}
