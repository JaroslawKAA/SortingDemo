using System;
using System.Collections;
using System.Collections.Generic;
using Robot;
using UnityEngine;

public abstract class SortingStrategyBase
{
    public event Action onSortingComplete;
    
    private GameObject _instance;

    protected GameObject Instance
    {
        get => _instance;
        private set => _instance = value;
    }

    private RobotStateMachine _stateMachine;

    protected RobotStateMachine StateMachine
    {
        get => _stateMachine;
        private set
        {
            _stateMachine = value;
        }
    }

    public SortingStrategyBase(GameObject instance)
    {
        Instance = instance;
        StateMachine = instance.GetComponent<RobotStateMachine>();
    }

    public abstract void Sort();

    public void Invoke_OnSortingComplete()
    {
        onSortingComplete?.Invoke();
    }
}
