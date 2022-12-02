using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Slot Settings", menuName = "ScriptableObjects/Slot/New Slot Settings")]
public class SlotSettings : ScriptableObject
{

    [SerializeField] float autoSpinDuration = 5f;
    public float AutoSpinDuration => autoSpinDuration;

    [SerializeField] float spinSpeed;
    public float SpinSpeed => spinSpeed;

    [SerializeField] float numOfCyclesAutoSpin;
    public float NumOfCyclesAutoSpin => numOfCyclesAutoSpin;

    [SerializeField] List<ReelSettings> reels;
    public List<ReelSettings> Reels => new List<ReelSettings>(reels);
}
