using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


[Serializable]
public class AssetDependentSlotSymbol
{
    public SlotSymbol slotSymbolPrefab;
    public AssetReferenceSlotSymbol addressableAsset;

    public IEnumerator LoadAssetAndInitObject()
    {
        var op = addressableAsset.LoadAssetAsync<SlotSymbol>();
        yield return op;
        if (op.Result != null)
        {
            slotSymbolPrefab = op.Result;
        }
        else
            throw new Exception("op.Result is null. slot symbol. " );
    }
        
}


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
public class AssetDependentReel
{
    public ReelController reelPrefab;
    public AssetReferenceReelController addressableAsset;

    public IEnumerator LoadAssetAndInitObject()
    {
        var op = addressableAsset.LoadAssetAsync<ReelController>();
        yield return op;
        if (op.Result != null)
        {
            reelPrefab = op.Result;
        }
        else
            throw new Exception("op.Result is null. reel. ");
    }
}


[Serializable]
public class AssetReferenceReelController : AssetReferenceT<ReelController>
{
    public AssetReferenceReelController(string guid) : base(guid) { }
}


[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid) { }
}

[Serializable]
public class AssetReferenceSlotSymbol : AssetReferenceT<SlotSymbol>
{
    public AssetReferenceSlotSymbol(string guid) : base(guid) { }
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

    public ReelController ReelsPrefab => assetDependentReelPrefab.reelPrefab;

    [SerializeField] AssetDependentReel assetDependentReelPrefab;

    [SerializeField] AssetDependentSprite[] assetDependentImages;

    [SerializeField] AssetDependentAudioClip winningSoundAudioClip;

    [SerializeField] AssetDependentSlotSymbol[] assetDependentSlotSymbols;

    public void LoadMainSceneAssets()
    {
        StartCoroutine(LoadAssets());
    }

    IEnumerator LoadAssets()
    {
        yield return LoadSprites();
        yield return LoadReelPrefab();
        yield return LoadAudioSource();
        yield return LoadSlotSymbols();
    }

    IEnumerator LoadSprites()
    {
        foreach (AssetDependentSprite spriteAsset in assetDependentImages)
        {
            yield return spriteAsset.LoadAssetAndInitObject();
        }
    }

    IEnumerator LoadReelPrefab()
    {
        yield return assetDependentReelPrefab.LoadAssetAndInitObject();
    }

    IEnumerator LoadAudioSource()
    {
        yield return winningSoundAudioClip.LoadAssetAndInitObject();
    }

    IEnumerator LoadSlotSymbols()
    {
        foreach (AssetDependentSlotSymbol slotsymbol in assetDependentSlotSymbols)
        {
            yield return slotsymbol.LoadAssetAndInitObject();
        }
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
