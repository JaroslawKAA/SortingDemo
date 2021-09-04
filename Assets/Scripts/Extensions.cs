using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    
    
    public static List<T> Shuffle<T>(this List<T> list)
    {
        return list
            .OrderBy(o => Utils.Random.Next())
            .ToList();
    }
}
