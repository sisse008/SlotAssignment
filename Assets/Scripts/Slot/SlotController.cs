using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    [SerializeField] SpinButtonController slotButton;

    [SerializeField] SlotSettings settings;

    [SerializeField] Transform reelsHolder;

    [SerializeField]List<ReelController> reels = new List<ReelController>();

    

#if UNITY_EDITOR
    public ReelController debugReel;
#endif


    public UnityAction OnSpinAction;
    private void OnEnable()
    {
        slotButton.SpinPressed += SpinEndless;
        slotButton.StopPressed += Stop;
        slotButton.AutoSpinPressed += AutoSpin; 
    }

    private void OnDisable()
    {
        slotButton.SpinPressed -= SpinEndless;
        slotButton.StopPressed -= Stop;
        slotButton.AutoSpinPressed -= AutoSpin;
    }
    private void InitSlot()
    {
        InitReels();
        slotButton.SetHoldThreshHold(settings.AutoHoldThreshhold);
    }

    private void InitReels()
    {
        reels.Clear();

#if UNITY_EDITOR
        if (debugReel && debugReel.isActiveAndEnabled)
        {
            reels.Add(debugReel);
            debugReel.InitReel(null);
            return;
        }
#endif
       
        foreach (ReelSettings reelSetting in settings.Reels)
        {
            ReelController reel = Instantiate(AssetsManager.Instance.reelPrefabAsset, reelsHolder);
            reel.InitReel(reelSetting);
            reels.Add(reel);
        }
    }

    private void Start()
    {
        InitSlot();
        Stop();
    }

    public enum SlotMode
    {
        IDLE,
        SPINNING
    };

    [SerializeField] SlotMode currentSlotMode;
    public SlotMode CurrentSlotMode => currentSlotMode;

    void SpinEndless()
    {
        if (!SessionController.AllowSpin)
            return;
        StartCoroutine(Spin());
        slotButton.ChangeToStopState();
    } 

    IEnumerator Spin(bool auto = false)
    {
        currentSlotMode = SlotMode.SPINNING;
        OnSpinAction?.Invoke();
        if (auto == false)
        {
            yield return SpinReels();
        }
        else 
        {
            yield return SpinReelsAuto();
            Stop();
        }
    }

    void Stop()
    {
        currentSlotMode = SlotMode.IDLE;
       
        StartCoroutine(StopReels());

        slotButton.ChangeToSpinState();
    }

    IEnumerator SpinReels()
    {
        foreach (ReelController reel in reels)
        {
            reel.Spin(settings.SpinSpeed);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator StopReels()
    {
        foreach (ReelController reel in reels)
        {
            reel.Stop();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpinReelsAuto()
    {
        foreach (ReelController reel in reels)
        {
            reel.SpinAuto(settings.SpinSpeed, settings.NumOfCyclesAutoSpin);
            yield return new WaitForSeconds(0.5f);
        }
    }
    void AutoSpin()
    {
        if (!SessionController.AllowSpin)
            return;
        StartCoroutine(Spin(true));
        slotButton.ChangeToAutoState();
    }
}
