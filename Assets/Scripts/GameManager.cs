using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public UnityAction<int> ScoreUpdated;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }
    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] int score;
    public int Score => score;

    private void Start()
    {
        UpdateScore(100000);
    }

    void UpdateScore(int newScore)
    {
        score = newScore;
        ScoreUpdated?.Invoke(score);
    }

    public void ReduceScore(int cost)
    {
        UpdateScore(score - cost);
    }

    public void IncreaseScore(int gain)
    {
        UpdateScore(score + gain);
    }
}
