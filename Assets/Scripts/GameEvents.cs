using System;
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
    public void Invoke_OnStartSorting(SorthingMethod sorthingMethod)
    {
        onStartSorting?.Invoke(sorthingMethod);
    }
    
    public event Action onSortingComplete;
    public void Invoke_OnSortingComplete()
    {
        onSortingComplete?.Invoke();
    }

    public event Action onRobotStateChange;
    
    public void Invoke_OnRobotStateChange()
    {
        onRobotStateChange?.Invoke();
    }
    
    public event Action onProcedureComplete;

    public void Invoke_OnProcedureComplete()
    {
        onProcedureComplete?.Invoke();
    }
}
