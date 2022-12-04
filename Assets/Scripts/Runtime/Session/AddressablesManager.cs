using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[Serializable]
public class AssetDependentAudioClip
{
    public AudioSource audioSourceInstance;
    public AssetReferenceAudioClip addressableAsset;
}

[Serializable]
public class AssetDependentTexture2D
{
    public Image imageInstance;
    public AssetReferenceTexture2D addressableAsset;
}

[Serializable]
public class AssetDependentReel
{
    public ReelController reelPrefab;
    public AssetReference addressableAsset;
}

[Serializable]
public class AssetReferenceAudioClip : AssetReference
{
    public AssetReferenceAudioClip(string guid) : base(guid) { }
}
public class AddressablesManager : MonoBehaviour
{

    private static AddressablesManager instance;

    public static AddressablesManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<AddressablesManager>();
            return instance;
        }
    }

    public ReelController ReelsPrefab => reelPrefab.reelPrefab;

    [SerializeField] AssetDependentReel reelPrefab;

    [SerializeField] AssetDependentTexture2D[] images;

    [SerializeField] AssetDependentAudioClip winningSoundAudioClip;

    private void OnEnable()
    {
        Addressables.InitializeAsync().Completed += AddressablesManager_Complete;
    }

    private void AddressablesManager_Complete(AsyncOperationHandle<IResourceLocator> obj)
    {
        reelPrefab.addressableAsset.InstantiateAsync().Completed += (go) =>
        {
            reelPrefab.reelPrefab = go.Result.GetComponent<ReelController>();
        };
    }
}
