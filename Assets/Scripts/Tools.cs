using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    public static float ClosestPointDistanceFromUnder(List<Vector3> points, Vector3 center)
    {
        float distance = float.MaxValue;
        foreach (Vector3 point in points)
        {
            if (point.y > center.y)
                continue;
            if (Mathf.Abs(center.y - point.y) < distance)
                distance = Mathf.Abs(center.y - point.y);
        }

        return distance;
    }
}
