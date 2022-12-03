using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    [SerializeField] SlotController slot;
    [SerializeField] ScoreBoard scoreBoard;

    public static int spinCost { get; } = 1000;
    public static bool AllowSpin => GameManager.Instance.Score >= spinCost;

    private void OnEnable()
    {
        slot.OnSpinAction += ReduceScore;
        GameManager.Instance.ScoreUpdated += scoreBoard.UpdateScoreBoard;  
    }

    private void OnDisable()
    {
        slot.OnSpinAction -= ReduceScore;
        if (GameManager.Instance)
            GameManager.Instance.ScoreUpdated -= scoreBoard.UpdateScoreBoard;
    }

    void ReduceScore()
    {
        GameManager.Instance.ReduceScore(spinCost);
        scoreBoard.UpdateScoreBoard(GameManager.Instance.Score);
    }
}
