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

    [SerializeField]List<ReelController> reels = new List<ReelController>();


#if UNITY_EDITOR
    public ReelController debugReel;
#endif

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
    private void InitSlot()
    {
        InitReels();
    }

    private void InitReels()
    {
        reels.Clear();

#if UNITY_EDITOR
        if (debugReel && debugReel.isActiveAndEnabled)
        {
            reels.Add(debugReel);
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

    public void Spin()
    {
        currentSlotMode = SlotMode.SPINNING;

        StartCoroutine(SpinReels());
        slotButton.ChangeToStopState();
    }

    public void Stop()
    {
        currentSlotMode = SlotMode.IDLE;
        StartCoroutine(SpinReels(false));

        slotButton.ChangeToSpinState();
    }

    IEnumerator SpinReels(bool spin = true)
    {
        foreach (ReelController reel in reels)
        {
            if (spin)
                reel.Spin(settings.SpinSpeed);
            else
                reel.Stop();
            yield return new WaitForSeconds(0.5f);
        }
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
