using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class WarningMassageController : MonoBehaviour
{

    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    // Start is called before the first frame update
    void Activate(string message)
    {
        text.text = message;
    }

    void Deactivate()
    {
        text.text = "";
    }

    public void ShowMessage(string message, int second = 2)
    {
        StartCoroutine(RuntimeTools.DoForXSeconds(second, () => Activate(message),
            () => Deactivate()));
    }
}
