using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown debugDropdown;

    int dropdownSelectedVal = 0;
    public int NumOfMatchesToForce => int.Parse(debugDropdown.options[dropdownSelectedVal].text);
    public bool Debug => NumOfMatchesToForce > -1;

    private static DebugManager instance = null;
    public static DebugManager Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<DebugManager>();
            return instance;
        }
    }

    private void OnEnable()
    {
        debugDropdown.onValueChanged.AddListener((val)=> dropdownSelectedVal=val);
    }
    private void OnDisable()
    {
        debugDropdown.onValueChanged.RemoveAllListeners();
    }
}
