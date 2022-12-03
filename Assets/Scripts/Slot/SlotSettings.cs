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

    [SerializeField] List<ReelSettings> reels;
    public List<ReelSettings> Reels => new List<ReelSettings>(reels);
}
