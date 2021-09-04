using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    /// <summary>
    /// Singleton
    /// </summary>
    public static GameEvents S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            throw new ArgumentException("Try to create second singleton object.");
        }
    }

    public event Action<SorthingMethod> onStartSorting;

    /// <summary>
    /// Run StartSorting event.
    /// </summary>
    public void StartSorting(SorthingMethod sorthingMethod)
    {
        onStartSorting?.Invoke(sorthingMethod);
    }
}
