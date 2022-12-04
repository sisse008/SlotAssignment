using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsManager : MonoBehaviour
{
    //TODO: load assets from bundle
    public ReelController reelPrefabAsset;

    private static AssetsManager instance = null;

    public static AssetsManager Instance
    {
        get
        {
            if (instance)
                return instance;
            instance = FindObjectOfType<AssetsManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (instance && instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
