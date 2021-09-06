using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortingStrategyBase : SortingStrategyBase
{
    private int index = 0;
    
    public BubbleSortingStrategyBase(GameObject instance) : base(instance)
    {
    }
    
    public override void LoadSortingStrategy()
    {
        while (index < GameManager.S.slots.Length - 1)
        {
            StateMachine.AddSortingStep(GoToFirstSlot);
            StateMachine.AddSortingStep(GetFirstBall);
            StateMachine.AddSortingStep(GoToSecondSlot);
            StateMachine.AddSortingStep(GetSecondBall);
            StateMachine.AddSortingStep(GoToFirstSlot);
            StateMachine.AddSortingStep(PutLowerBall);
            StateMachine.AddSortingStep(GoToSecondSlot);
            StateMachine.AddSortingStep(PutSecondBall);
            
            index++;
        }

        index = 0;
    }

    private void GoToFirstSlot()
    {
        StateMachine.GoTo(GameManager.S.slots[index].transform);
    }
    
    private void GetFirstBall()
    {
        if (GameManager.S.slots[index].transform.childCount > 0)
        {
            StateMachine.GetBall(GameManager.S.slots[index], Hands.Left);
        }
        else
        {
            StateMachine.NextProcedure();
        }
    }

    private void GoToSecondSlot()
    {
        StateMachine.GoTo(GameManager.S.slots[index + 1].transform);
    }

    private void GetSecondBall()
    {
        if (GameManager.S.slots[index + 1].transform.childCount > 0)
        {
            StateMachine.GetBall(GameManager.S.slots[index + 1], Hands.Right);
        }
        else
        {
            StateMachine.NextProcedure();
        }
    }

    private void PutLowerBall()
    {
        if (RobotController.leftHandObject.GetComponent<Ball>().Id
            > RobotController.rightHandObject.GetComponent<Ball>().Id)
        {
            StateMachine.PutBall(GameManager.S.slots[index], Hands.Right);
        }
        else
        {
            StateMachine.PutBall(GameManager.S.slots[index], Hands.Left);
        }
    }

    private void PutSecondBall()
    {
        if (RobotController.leftHandObject != null)
        {
            StateMachine.PutBall(GameManager.S.slots[index + 1], Hands.Left);
        }
        else
        {
            StateMachine.PutBall(GameManager.S.slots[index + 1], Hands.Right);
        }

        index++;
    }

    protected override void OnProcedureComplete()
    {
        base.OnProcedureComplete();
        index = 0;
    }
}
