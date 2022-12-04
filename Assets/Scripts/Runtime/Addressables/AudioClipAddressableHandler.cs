using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


[RequireComponent(typeof(AudioSource))]
public class AudioClipAddressableHandler : MonoBehaviour, IAddressableDependent<AudioSource>
{
    AudioSource audioSource;
    [SerializeField] AssetReference audioClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        
    }
    public void GetAsset()
    {
        StartCoroutine(GetAssetAndSetField(audioSource));
    }

    public IEnumerator GetAssetAndSetField(AudioSource audioSource)
    {
        var op = audioClip.LoadAssetAsync<AudioClip>();
        yield return op;
        if (op.Result != null)
        {
            audioSource.clip = op.Result;
        }
    }
}
