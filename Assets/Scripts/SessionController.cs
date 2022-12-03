using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SessionController : MonoBehaviour
{
    [SerializeField] SlotController slot;
    [SerializeField] ScoreBoard scoreBoard;
    [SerializeField] GameObject notFundsMessage;

    public static int spinCost { get; } = 1000;
    public static bool AllowSpin => GameManager.Instance.Score >= spinCost;

    private void OnEnable()
    {
        slot.OnSpinAction += ReduceScore;
        slot.OnCantSpinAction += ShowNoFundsMessage;
        GameManager.Instance.ScoreUpdated += scoreBoard.UpdateScoreBoard;  
    }

    private void OnDisable()
    {
        slot.OnSpinAction -= ReduceScore;
        slot.OnCantSpinAction -= ShowNoFundsMessage;

        if (GameManager.Instance)
            GameManager.Instance.ScoreUpdated -= scoreBoard.UpdateScoreBoard;
    }

    void ShowNoFundsMessage()
    {
        StartCoroutine(RuntimeTools.DoForXSeconds(2, () => notFundsMessage.SetActive(true),
            () => notFundsMessage.SetActive(false)));
    }

   
    void ReduceScore()
    {
        GameManager.Instance.ReduceScore(spinCost);
        scoreBoard.UpdateScoreBoard(GameManager.Instance.Score);
    }
}
