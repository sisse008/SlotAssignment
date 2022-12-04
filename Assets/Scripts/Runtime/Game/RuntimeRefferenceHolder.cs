using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeRefferenceHolder : MonoBehaviour
{
    private static RuntimeRefferenceHolder instance = null;
    public static RuntimeRefferenceHolder Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<RuntimeRefferenceHolder>();
            return instance;
        }
    }

    public ReelController reelPrefab;
}
