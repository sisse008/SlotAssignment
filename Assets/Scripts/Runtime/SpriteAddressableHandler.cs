using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(Image))]
public class SpriteAddressableHandler : MonoBehaviour, IAddressableDependent<Image>
{
    Image image;
    [SerializeField] AssetReference sprite;


    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        GetAsset(image);
    }

    private void OnDisable()
    {
        //TODO: unload assets
    }

    public void GetAsset(Image image)
    {
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
