using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionController : MonoBehaviour
{
    [SerializeField] SlotController slot;
    [SerializeField] SpinButtonController slotButton;
    [SerializeField] ScoreBoard scoreBoard;
    [SerializeField] GameObject notFundsMessage;

    public static int spinCost { get; } = 1000;
    public static int spinPrize { get; } = 5000;
    public static bool AllowSpin => GameManager.Instance.Score >= spinCost;
   

    private void OnEnable()
    {
        slotButton.SpinPressed += SpinSlot;
        slotButton.StopPressed += StopSlot;
        slotButton.AutoSpinPressed += AutoSpinSlot;

        slot.OnSpinAction += ReduceScore;
        slot.OnWinAction += OnSlotWin;

        if (GameManager.Instance)
            GameManager.Instance.ScoreUpdated += scoreBoard.UpdateScoreBoard;  
    }

    private void OnDisable()
    {
        slotButton.SpinPressed -= SpinSlot;
        slotButton.StopPressed -= StopSlot;
        slotButton.AutoSpinPressed -= AutoSpinSlot;


        slot.OnSpinAction -= ReduceScore;
        slot.OnWinAction -= OnSlotWin;

        if (GameManager.Instance)
            GameManager.Instance.ScoreUpdated -= scoreBoard.UpdateScoreBoard;
    }

    private void Start()
    {
        InitNewSession();
    }
    private void InitNewSession()
    {
        slotButton.ChangeToSpinState();
    }

    void ShowNoFundsMessage()
    {
        StartCoroutine(RuntimeTools.DoForXSeconds(2, () => notFundsMessage.SetActive(true),
            () => notFundsMessage.SetActive(false)));
    }

    void OnSlotWin(int matches)
    {
        GameManager.Instance.IncreaseScore(matches*spinPrize);
    }
    void ReduceScore()
    {
        GameManager.Instance.ReduceScore(spinCost);
    }

    void SpinSlot()
    {
        if (!AllowSpin)
        {
            ShowNoFundsMessage();
            return;
        }
        StartCoroutine(slot.Spin());
        slotButton.ChangeToStopState();
    }

    void AutoSpinSlot()
    {
        if (!AllowSpin)
        {
            ShowNoFundsMessage();
            return;
        }
        StartCoroutine(slot.Spin(true));
        slotButton.ChangeToAutoState();
    }

    void StopSlot()
    {
        slotButton.ChangeToSpinState();
        slot.Stop();
    }
}
