using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [SerializeField] RectTransform symbolsHolder;
    [SerializeField] RectTransform centerPosition;

    [SerializeField] List<Image> symbols;
    public void InitReel(ReelSettings settings)
    {
        symbols.Clear();
        foreach (Image image in settings.Symbols)
        {
            Image s = Instantiate(image,symbolsHolder);
            symbols.Add(s);
        }
    }

    Coroutine spinCoroutine;
    public void Spin(float speed)
    {
       // Debug.Log("spin");
        spinCoroutine = StartCoroutine(SpinLoop(speed));
    }

    IEnumerator SpinLoop(float speed)
    {
       while (true)
        {   
            if (symbolsHolder.anchoredPosition.y < 11.7)
            {
                symbolsHolder.anchoredPosition += new Vector2(0, 0.1f)*speed;
            }
            else 
            {
                symbolsHolder.anchoredPosition = new Vector3(0, -1.94f);
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
