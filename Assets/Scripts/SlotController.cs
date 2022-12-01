using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    public enum SlotMode
    {
        IDLE,
        SPINNING
    };
    [SerializeField] SlotMode currentSlotMode;
    public SlotMode CurrentSlotMode => currentSlotMode;

    public void Spin()
    {
        currentSlotMode = SlotMode.SPINNING;
    }

    public void Stop()
    {
        currentSlotMode = SlotMode.IDLE;
    }
}
