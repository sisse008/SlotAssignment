using System.Collections;
using System.Collections.Generic;
using System;


public static class Tools 
{
    static Random rng = new Random();
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
}
