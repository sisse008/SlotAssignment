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
    public void Activate(string message)
    {
        text.text = message;
    }

    public void Deactivate()
    {
        text.text = "";
    }
}
