using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Reel Settings", menuName = "ScriptableObjects/Reel/New Reel Settings")]

public class ReelSettings : ScriptableObject
{
    [SerializeField] List<Image> symbols;
    public List<Image> Symbols => Tools.ShuffledList(symbols);
}
