using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    public Transform symbolsHolder;
    public void InitReel(ReelSettings settings)
    {
        foreach (Image image in settings.Symbols)
        {
            Instantiate(image,symbolsHolder);
        }
    }

    public void Spin()
    {
        
    }

    public void Stop()
    { 

    }
}
