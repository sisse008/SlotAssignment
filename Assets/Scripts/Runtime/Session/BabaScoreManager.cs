using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BabaScoreManager : MonoBehaviour
{
    [SerializeField] protected ScoreBoard scoreBoard;

    [SerializeField] protected int score;

    protected virtual void UpdateScore(int score)
    {
        this.score = score;
        scoreBoard.UpdateScoreBoard(score);
    }

    public void IncreaseScore(int gain) => UpdateScore(score += gain);
    

    public void DecreaseScore(int loss) => UpdateScore(score-= loss);
    
}
