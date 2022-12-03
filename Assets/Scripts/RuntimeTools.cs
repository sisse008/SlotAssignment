using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RuntimeTools : MonoBehaviour
{
    public static IEnumerator DoForXSeconds(int seconds, Action _do, Action _undo)
    {
        _do.Invoke();
        yield return new WaitForSeconds(seconds);
        _undo.Invoke();
    }
}
