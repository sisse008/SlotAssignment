using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : BabaScoreManager
{

    public int Score => score;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        UpdateScore(GameManager.Instance.score);
    }
    protected override void UpdateScore(int score)
    {
        GameManager.Instance.score = score;
        base.UpdateScore(score);
    }
}
