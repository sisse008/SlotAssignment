using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(Image))]
public class SpriteAddressableHandler : MonoBehaviour, IAddressableDependent<Image>
{
    Image image;
    [SerializeField]public AssetReference sprite;


    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        GetAsset();
    }

    private void OnDisable()
    {
        ReleaseAsset();
    }

    public void ReleaseAsset()
    {
        var asset = image.sprite;
        image.sprite = null;
        Addressables.Release(asset);
    }
    public void GetAsset()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return;
        }
        StartCoroutine(GetAssetAndSetField(image));
    }

    public IEnumerator GetAssetAndSetField(Image image)
    {
        var op = sprite.LoadAssetAsync<Sprite>();
        yield return op;
        if (op.Result != null)
        {
            image.sprite = op.Result;
        }
    }
}
