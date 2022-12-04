using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    
    [SerializeField] SlotSettings settings;

    [SerializeField] Transform reelsHolder;

    [SerializeField] List<ReelController> reels = new List<ReelController>();
    public int NumberOfReels => reels.Count;

#if UNITY_EDITOR
    public ReelController debugReel;
#endif

    public UnityAction OnAutoSpinAction;
    public UnityAction OnAutoSpinEnded;
    public UnityAction<int> OnWinAction;

    private void InitSlot()
    {
        InitReels();
        currentSlotMode = SlotMode.IDLE;
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
       
        for(int i=0; i<settings.NumberOfReels;i++)
        {
            ReelController reel = Instantiate(RuntimeRefferenceHolder.Instance.reelPrefab,
                reelsHolder);
            reel.InitReel(settings.ReelsSettings);
            reels.Add(reel);
        }
    }

    private void Start()
    {
        InitSlot();
    }

    public enum SlotMode
    {
        IDLE,
        SPINNING
    };

    [SerializeField] SlotMode currentSlotMode;
    public SlotMode CurrentSlotMode => currentSlotMode;

    public IEnumerator Spin(bool auto = false, int[] row = null)
    {
        currentSlotMode = SlotMode.SPINNING;
        if (auto == false)
        {
            yield return SpinReels();
        }
        else 
        {
            yield return SpinReelsAuto(rowToForce:row);
        }
    }

    public void Stop(int[] winningRow = null)
    {
        currentSlotMode = SlotMode.IDLE;
       
        StartCoroutine(StopReels(action:() => CheckForWin(), resultsToForce: winningRow));    
    }

    IEnumerator SpinReels()
    {
        foreach (ReelController reel in reels)
        {
            reel.Spin(settings.SpinSpeed);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator StopReels(Action action = null, int[] resultsToForce = null)
    {
        for(int i=0; i< NumberOfReels; i++)
        {
            if(resultsToForce == null || resultsToForce.Length != NumberOfReels)
                reels[i].Stop();
            else
                reels[i].Stop(resultsToForce[i]);

            yield return new WaitForSeconds(0.5f);
        }
        action?.Invoke();
    }
    public void CheckForWin()
    {
        Dictionary<int, int> counter = new Dictionary<int, int>();
        foreach (ReelController reel in reels)
        {
            if (counter.ContainsKey(reel.winningSymbol))
                counter[reel.winningSymbol]++;
            else
                counter[reel.winningSymbol] = 1;
        }
        foreach (KeyValuePair<int, int> entry in counter)
        {
            if (entry.Value >= 3)
                OnWinAction?.Invoke(entry.Value);
        }
    }

    IEnumerator SpinReelsAuto(int[] rowToForce = null)
    {
        for (int i = 0; i < NumberOfReels; i++)
        {
            if (rowToForce == null || rowToForce.Length != NumberOfReels)
               reels[i].SpinAuto(settings.SpinSpeed, settings.NumOfCyclesAutoSpin);
            else
               reels[i].SpinAuto(settings.SpinSpeed, settings.NumOfCyclesAutoSpin, rowToForce[i]);

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitUntil(() => reels.All(x => x.Stopped)); //will not get true winning row if dont wait. 
        OnAutoSpinEnded?.Invoke();
    }
}
