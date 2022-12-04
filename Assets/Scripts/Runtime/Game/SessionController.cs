using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Linq;

public class SessionController : MonoBehaviour
{
    [SerializeField] SlotController slot;
    [SerializeField] SpinButtonController slotButton;
    [SerializeField] WarningMassageController messageController;
    [SerializeField] ScoreManager scoreManager;


    public static int spinCost { get; } = 1000;
    public static int spinPrize { get; } = 5000;
    public static int MaxMatchesPossible => 5;
    public bool AllowSpin => scoreManager.Score >= spinCost;

    public static string NoFundsMessage => "No Sufficient Funds To Play";

    public static string AutoSpinRleaseMessage => "Rleace Spin Button To AutoSpin";

    private void OnEnable()
    {
        slotButton.SpinPressed += SpinSlot;
        slotButton.StopPressed += StopSlot;
        slotButton.AutoSpinPressed += AutoSpinSlot;
        slotButton.SpinButtonHold += ShowAutospinMessage;

        slot.OnWinAction += OnSlotWin;

        slot.OnAutoSpinEnded += StopAutoSpinning;
 
    }

    private void OnDisable()
    {
        slotButton.SpinPressed -= SpinSlot;
        slotButton.StopPressed -= StopSlot;
        slotButton.AutoSpinPressed -= AutoSpinSlot;

        slot.OnWinAction -= OnSlotWin;

        slot.OnAutoSpinEnded -= StopAutoSpinning;
        slotButton.SpinButtonHold -= ShowAutospinMessage;

    }

    private void Start()
    {
        InitNewSession();
    }

  
    private void InitNewSession()
    {
        slotButton.ChangeToSpinState();
    }

    void OnSlotWin(int matches)
    {
        scoreManager.IncreaseScore(matches * spinPrize);
        if (matches == MaxMatchesPossible)
            CanvasManager.Instance.ShowWinningPopup((matches * spinPrize).ToString());
    }
    void ShowAutospinMessage()
    {
        messageController.ShowMessage(AutoSpinRleaseMessage);
    }
    bool IsSpinAllowed()
    {
        if (!AllowSpin)
        {
            messageController.ShowMessage(NoFundsMessage);
            return false;
        }
        return true;
    }

    void SpinSlot()
    {
        if (IsSpinAllowed())
        { 
            StartCoroutine(slot.Spin());
            slotButton.ChangeToStopState();
            scoreManager.DecreaseScore(spinCost);
        }
    }

    void AutoSpinSlot()
    {
        if (IsSpinAllowed())
        {
            StartCoroutine(slot.Spin(true, GetRandomWinningRowForDebugging()));
            slotButton.ChangeToAutoState();
            scoreManager.DecreaseScore(spinCost);
        }
    }

    void StopAutoSpinning()
    {
        slotButton.ChangeToSpinState();
        slot.CheckForWin();
    }
    void StopSlot()
    {
        slotButton.ChangeToSpinState();
        slot.Stop(GetRandomWinningRowForDebugging());
    }

    private int[] GetRandomWinningRowForDebugging()
    {
        int[] rowToForce = null;
        if (DebugManager.Instance.Debug)
            rowToForce = GenerateWinningRow(DebugManager.Instance.NumOfMatchesToForce);
        return rowToForce;
    }

    private int[] GenerateWinningRow(int numOfMatchesToForce)
    {
        int winningID = Random.Range(1, 9);
        List<int> reelPositions = Enumerable.Range(1, 5).ToList();
        reelPositions = Tools.ShuffledList(reelPositions);
        int[] row = new int[slot.NumberOfReels];
        for (int i = 0; i < slot.NumberOfReels; i++)
            row[i] = Tools.RandomNumberFromRangeExcept(1,9,winningID);

        for (int i = 0; i < numOfMatchesToForce; i++)
            row[reelPositions[i] -1] = winningID;

        return row;
    }
}
