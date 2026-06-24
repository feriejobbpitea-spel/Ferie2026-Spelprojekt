using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{
    public static T GetRandom<T>(this List<T> value) 
    {
        return value[Random.Range(0, value.Count)];
    }
}
