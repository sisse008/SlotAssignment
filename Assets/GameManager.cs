using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameData data;

    public int Score => data.score;
    private void Start()
    {
        data = new GameData();
    }

    class GameData
    {
        public int score { get; private set; }
        public GameData()
        {
            score = 100000;
        }

        public void UpdateScore(int points)
        {
            score += points;
        }
    }
}
