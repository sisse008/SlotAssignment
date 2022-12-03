using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [SerializeField] RectTransform symbolsHolder;
    [SerializeField] RectTransform centerPosition;

    [SerializeField] List<RectTransform> symbols;

    Vector2 initHolderPosition;

    float symbolHeight = 1.69f;
    float height => symbolHeight * (symbols==null ? 0 : symbols.Count-1);
    float offset => symbolHeight * 0.8f;

    private void OnEnable()
    {
        initHolderPosition = symbolsHolder.anchoredPosition;
    }
    public void InitReel(ReelSettings settings)
    {
        symbols.Clear();
       
        if (settings == null || settings.Symbols.Count == 0)
        {
            Image[] children = GetComponentsInChildren<Image>();
            symbols = children.Select(z => z.GetComponent<RectTransform>()).ToList();
            return;
        }        
        foreach (Image image in settings.Symbols)
        {
            Image s = Instantiate(image,symbolsHolder);
            symbols.Add(s.GetComponent<RectTransform>());
        }
    }

    Coroutine spinCoroutine;
    public void Spin(float speed, bool setNumOfCycles, float numOfCycles = 0)
    {
        if (setNumOfCycles)
        {
            spinCoroutine = StartCoroutine(SpinForXCycles(speed, numOfCycles));

        }
        else 
        {
            spinCoroutine = StartCoroutine(SpinLoop(speed));
        }
       
    }

    IEnumerator SpinForXCycles(float speed, float numOfCycles)
    {
        int cycles = 0;
        while (cycles < numOfCycles)
        {
            yield return SpinOneCycle(speed);
            cycles++;
        }
    }

    IEnumerator SpinLoop(float speed)
    {
       while (true)
       {
            yield return SpinOneCycle(speed);
       }
    }

    IEnumerator SpinOneCycle(float speed)
    {
        while(symbolsHolder.anchoredPosition.y < initHolderPosition.y + height - offset)
        {
            symbolsHolder.anchoredPosition += new Vector2(0, 0.1f) * speed;
            yield return null;
        }
       
        symbolsHolder.anchoredPosition = initHolderPosition;
        
        yield return null;
    }

    public void Stop()
    {
        if (spinCoroutine == null)
            return;

        StopCoroutine(spinCoroutine);

        ClampPosition();
    }

    void ClampPosition()
    {
        Collider2D center = centerPosition.GetComponent<Collider2D>();
        Collider2D[] overlappingSymbols = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = center.OverlapCollider(contactFilter.NoFilter(), overlappingSymbols);
        if (colliderCount == 0)
            return;
        float distance = overlappingSymbols[0].transform.position.y -
            centerPosition.transform.position.y;

        symbolsHolder.transform.position -= new Vector3(0,distance,0);
      
    }
}
