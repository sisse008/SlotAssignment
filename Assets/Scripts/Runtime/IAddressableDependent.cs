using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAddressableDependent<T> 
{
    void GetAsset(T field);

    IEnumerator GetAssetAndSetField(T field);
}
