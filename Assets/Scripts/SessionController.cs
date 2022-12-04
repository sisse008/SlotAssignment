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
    [SerializeField] ScoreBoard scoreBoard;
    [SerializeField] WarningMassageController messageController;

    public static int spinCost { get; } = 1000;
    public static int spinPrize { get; } = 5000;
    public static bool AllowSpin => GameManager.Instance.Score >= spinCost;
    public static int MaxMatchesPossible => 5;

    public static string NoFundsMessage => "No Sufficient Funds To Play";
    //TODO
    public static string AutoSpinRleaseMessage => "Rleace Spin Button To AutoSpin";

    private void OnEnable()
    {
        slotButton.SpinPressed += SpinSlot;
        slotButton.StopPressed += StopSlot;
        slotButton.AutoSpinPressed += AutoSpinSlot;

        slot.OnSpinAction += ReduceScore;
        slot.OnWinAction += OnSlotWin;

        slot.OnAutoSpinEnded += StopSlot;

      

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

        slot.OnAutoSpinEnded -= StopSlot;

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
        scoreBoard.UpdateScoreBoard(GameManager.Instance.Score);
    }

    void ShowNoFundsMessage()
    {
        StartCoroutine(RuntimeTools.DoForXSeconds(2, () => messageController.Activate(NoFundsMessage),
            () => messageController.Deactivate()));
    }

    void OnSlotWin(int matches)
    {
        GameManager.Instance.IncreaseScore(matches*spinPrize);
        if (matches == MaxMatchesPossible)
            CanvasManager.Instance.ShowWinningPopup();

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
        int[] rowToForce = null;
        if (DebugManager.Instance.Debug)
            rowToForce = GenerateWinningRow(DebugManager.Instance.NumOfMatchesToForce);
        slot.Stop(rowToForce);
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
