using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAddressableDependent<T> 
{
    void GetAsset();

    void ReleaseAsset();
    IEnumerator GetAssetAndSetField(T field);
}
