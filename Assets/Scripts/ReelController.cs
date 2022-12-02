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

    float height => 1.69f * (symbols==null ? 0 : symbols.Count);
    float offset => 1.69f / 2f;

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
    public void Spin(float speed)
    {
       // Debug.Log("spin");
        spinCoroutine = StartCoroutine(SpinLoop(speed));
    }

    IEnumerator SpinLoop(float speed)
    {
       while (true)
       {   
            if (symbolsHolder.anchoredPosition.y < initHolderPosition.y + height - offset)
            {
                symbolsHolder.anchoredPosition += new Vector2(0, 0.1f)*speed;
            }
            else 
            {
                symbolsHolder.anchoredPosition = initHolderPosition;
            }
            
            yield return null;
       }
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
        float minDis = float.MaxValue;
        bool clamp = false;
        string name;
        foreach (RectTransform symbol in symbols)
        {
            //TODO: bug!!!!!
            if (symbol.anchoredPosition.y > centerPosition.anchoredPosition.y)
                continue;

            float distance = centerPosition.anchoredPosition.y - symbol.anchoredPosition.y;
            if (distance < minDis)
            {
                name = symbol.name;
                minDis = distance;
                clamp = true;
            }
        }
        if (clamp)
            symbolsHolder.anchoredPosition += new Vector2(0, minDis);
    }
}
