using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Slot Settings", menuName = "ScriptableObjects/Slot/New Slot Settings")]
public class SlotSettings : ScriptableObject
{
    [SerializeField] float spinSpeed = 2f;
    public float SpinSpeed => spinSpeed;

    [SerializeField] float numOfCyclesAutoSpin = 10;
    public float NumOfCyclesAutoSpin => numOfCyclesAutoSpin;

    [SerializeField] ReelSettings reels;

    public ReelSettings ReelsSettings => reels; 

    [Range(2,5)]
    [SerializeField] int numberOfReels;
    public int NumberOfReels => numberOfReels;
}
