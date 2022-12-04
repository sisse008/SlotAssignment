using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ReelController : MonoBehaviour
{
    [SerializeField] RectTransform symbolsHolder;
    [SerializeField] RectTransform centerPosition;

    [SerializeField] List<SlotSymbol> symbols;

    Vector2 initHolderPosition;

    public int winningSymbol = 0;

    public bool Stopped { get; private set; }

    float symbolHeight = 1.69f;
    float height => symbolHeight * (symbols==null ? 0 : symbols.Count-1);
    float offset => symbolHeight * 0.8f;

    private void OnEnable()
    {
        initHolderPosition = symbolsHolder.anchoredPosition;
    }

    SlotSymbol GetSymbolFromId(int id)
    {
        foreach (SlotSymbol symbol in symbols)
        {
            if (id == symbol.Id)
                return symbol;
        }
        return null;
    }
    public void InitReel(ReelSettings settings)
    {
        winningSymbol = 0;
        symbols.Clear();
       
        if (settings == null || settings.Symbols.Count == 0)
        {
            Image[] children = GetComponentsInChildren<Image>();
            symbols = children.Select(z => z.GetComponent<SlotSymbol>()).ToList();
            return;
        }        
        foreach (Image image in settings.Symbols)
        {
            Image s = Instantiate(image,symbolsHolder);
            symbols.Add(s.GetComponent<SlotSymbol>());
        }
    }

    Coroutine spinCoroutine;
    public void Spin(float speed)
    {
        spinCoroutine = StartCoroutine(SpinEndless(speed));
    }

    public void SpinAuto(float speed, float numOfCycles, int forceWinningId = 0)
    {
        spinCoroutine = StartCoroutine(SpinForXCyclesAndStop(speed, numOfCycles, forceWinningId));
    }

    IEnumerator SpinForXCyclesAndStop(float speed, float numOfCycles, int forceWinningId = 0)
    {
        yield return SpinForXCycles(speed, numOfCycles);
        Stop(forceWinningId);
    }

    IEnumerator SpinForXCycles(float speed, float numOfCycles)
    {
        int cycles = 0;
        while (cycles < numOfCycles)
        {
            yield return SpinOneCycle(speed);
            cycles++;
            yield return null;
        }
       // Debug.Log("cycles = " + cycles);
    }

    IEnumerator SpinEndless(float speed)
    {
       while (true)
       {
            yield return SpinOneCycle(speed);
            yield return null;
        }
    }

    IEnumerator SpinOneCycle(float speed)
    {
        Stopped = false;
        while(symbolsHolder.anchoredPosition.y < initHolderPosition.y + height - offset)
        {
            symbolsHolder.anchoredPosition += new Vector2(0, 0.1f) * speed;
            yield return null;
        }
       
        symbolsHolder.anchoredPosition = initHolderPosition;        
    }

    public void Stop(int forceWinningId = 0)
    {
        if (spinCoroutine == null)
            return;

        StopCoroutine(spinCoroutine);
        Stopped = true;
        ClampPosition(forceWinningId);
    }

    void ClampPosition(int forceWinningId = 0)
    {
        SlotSymbol symbol = forceWinningId <= 0? GetOverlappingSymbol() :
            GetSymbolFromId(forceWinningId);
        float distance = symbol.transform.position.y -
            centerPosition.transform.position.y;

        symbolsHolder.transform.position -= new Vector3(0,distance,0);

        winningSymbol = symbol.GetComponent<SlotSymbol>().Id;
    }

    SlotSymbol GetOverlappingSymbol()
    {
        Collider2D center = centerPosition.GetComponent<Collider2D>();
        Collider2D[] overlappingSymbols = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();
        int colliderCount = center.OverlapCollider(contactFilter.NoFilter(), overlappingSymbols);
        if (colliderCount == 0)
            return null;
        return overlappingSymbols[0].GetComponent<SlotSymbol>();
    }
}
