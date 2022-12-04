using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

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

    public int score;

    private void Start()
    {
        score = 100000;
        Application.targetFrameRate = 30;
    }

    public void UpdateScore(int newScore)
    {
        score = newScore;
    }
}
