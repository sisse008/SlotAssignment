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

    public IEnumerator LoadAssetAndInitObject()
    {
        var op = addressableAsset.LoadAssetAsync<AudioClip>();
        yield return op;
        if (op.Result != null)
        {
            audioSourceInstance.clip = op.Result;
        }
        else
            throw new Exception("op.Result is null. audio clip. " );
    }
}

[Serializable]
public class AssetDependentSprite
{
    public Image imageInstance;
    public AssetReferenceSprite addressableAsset;

    public IEnumerator LoadAssetAndInitObject()
    {
        var op = addressableAsset.LoadAssetAsync<Sprite>();
        yield return op;
        if (op.Result != null)
        {
            imageInstance.sprite = op.Result;
        }
        else
            throw new Exception("op.Result is null. image: " + imageInstance.name);
    }
}


[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
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

    [SerializeField] AssetDependentSprite[] assetDependentImages;

    [SerializeField] AssetDependentAudioClip winningSoundAudioClip;


    private void Awake()
    {
        LoadMainSceneAssets();
    }
    public void LoadMainSceneAssets()
    {
        StartCoroutine(LoadAssets());
    }

    IEnumerator LoadAssets()
    {
        yield return LoadSprites();
        yield return LoadAudioSource();
    }

    IEnumerator LoadSprites()
    {
        foreach (AssetDependentSprite spriteAsset in assetDependentImages)
        {
            if(spriteAsset.addressableAsset != null)
                yield return spriteAsset.LoadAssetAndInitObject();
        }
    }

    IEnumerator LoadAudioSource()
    {
        yield return winningSoundAudioClip.LoadAssetAndInitObject();
    }











   /* private void AddressablesManager_Complete(AsyncOperationHandle<IResourceLocator> obj)
    {
        reelPrefab.addressableAsset.InstantiateAsync().Completed += (go) =>
        {
            reelPrefab.reelPrefab = go.Result.GetComponent<ReelController>();
        };
    }
   */
}
