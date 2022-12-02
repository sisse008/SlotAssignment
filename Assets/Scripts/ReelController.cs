using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [SerializeField] Transform symbolsHolder;
    [SerializeField] Transform centerPosition;
    public void InitReel(ReelSettings settings)
    {
        foreach (Image image in settings.Symbols)
        {
            Instantiate(image,symbolsHolder);
        }
    }

    Coroutine spinCoroutine;
    public void Spin()
    {
       // Debug.Log("spin");
        spinCoroutine = StartCoroutine(SpinLoop());
    }

    IEnumerator SpinLoop()
    {
       while (true)
        {
            
            if (symbolsHolder.localPosition.y < 11.7)
            {
                symbolsHolder.localPosition += new Vector3(0, 0.1f, 0);
            }
            else 
            {
                symbolsHolder.localPosition = new Vector3(0, -1.94f, 0);
            }
            
            yield return null;
        }
      
    }

    public void Stop()
    {
        if (spinCoroutine == null)
            return;

        //stop animation
        StopCoroutine(spinCoroutine);
        
        //find symbol with closest (from below) position

        //move symbols holder until symbol is same position as center position
    }
}
