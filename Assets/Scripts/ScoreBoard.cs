using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TMP_Text))]
public class ScoreBoard : MonoBehaviour
{
    TMP_Text text;
    
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    public void UpdateScoreBoard(int score)
    {
        text.text = score.ToString();
    }
}
