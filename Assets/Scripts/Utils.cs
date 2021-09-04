using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static System.Random Random { get; private set; }

    static Utils()
    {
        Random = new System.Random();
    }
}
