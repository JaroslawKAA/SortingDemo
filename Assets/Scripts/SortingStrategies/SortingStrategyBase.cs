using System;
using Robot;
using UnityEngine;

public abstract class SortingStrategyBase
{
    private RobotController _robotController;

    protected RobotController RobotController
    {
        get => _robotController;
        private set => _robotController = value;
    }

    private RobotStateMachine _stateMachine;

    protected RobotStateMachine StateMachine
    {
        get => _stateMachine;
        private set { _stateMachine = value; }
    }

    public SortingStrategyBase(GameObject instance)
    {
        StateMachine = instance.GetComponent<RobotStateMachine>();
        RobotController = instance.GetComponent<RobotController>();
        GameEvents.S.onProcedureComplete += OnProcedureComplete;
    }

    public abstract void LoadSortingStrategy();

    protected virtual void OnProcedureComplete()
    {
        CheckCompleteCondition();
    }
    
    private void CheckCompleteCondition()
    {
        int i = 1;
        foreach (GameObject slot in GameManager.S.slots)
        {
            if (slot.transform.GetChild(0).GetComponent<Ball>().Id != i)
            {
                return;
            }

            i++;
        }
            
        Debug.LogWarning("Sorting Complete");
        StateMachine.StopSorting();
        GameEvents.S.Invoke_OnSortingComplete();
    }
}