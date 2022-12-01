using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Slot Settings", menuName = "ScriptableObjects/Slot/NewSlotSettings")]
public class SlotSettings : ScriptableObject
{

    [SerializeField] float autoSpinDuration = 5f;
    public float AutoSpinDuration => autoSpinDuration;

    [SerializeField] float spinSpeed;
    public float SpinSpeed => spinSpeed;

    [SerializeField] float numOfCyclesAutoSpin;
    public float NumOfCyclesAutoSpin => numOfCyclesAutoSpin;

    [Range(2,5)]
    [SerializeField] int numOfReels;
    public int NumOfReels => numOfReels;
}
