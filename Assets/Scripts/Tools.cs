using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Tools 
{
    static System.Random rng = new System.Random();
    public static List<T> ShuffledList<T>(List<T> list)
    {
        List<T> shuffled = new List<T>(list);

        int n = shuffled.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = shuffled[k];
            shuffled[k] = shuffled[n];
            shuffled[n] = value;
        }
        return shuffled;
    }

    public static int RandomNumberFromRangeExcept(int min, int max ,int numToExclude)
    {
        int rand = Random.Range(min, max);
        while(rand == numToExclude)
            rand = Random.Range(min, max);
        return rand;
    }
}
