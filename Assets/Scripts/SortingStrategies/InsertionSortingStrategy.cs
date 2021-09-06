using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsertionSortingStrategy : SortingStrategyBase
{
    private int i;
    private int j;
    private int[] _ballsMock;
    private List<int> jState;
    private int operationIndex;
    
    public InsertionSortingStrategy(GameObject instance) : base(instance)
    {
        _ballsMock = GameManager.S.slots
            .Select(s => s.transform.GetChild(0).GetComponent<Ball>().Id)
            .ToArray();
        jState = new List<int>();
    }

    public override void LoadSortingStrategy()
    {
        for (i = 0; i < _ballsMock.Length - 1; i++)
        {
            for (j = i + 1; j > 0; j--)
            {
                if (_ballsMock[j - 1] > _ballsMock[j])
                {
                    int temp = _ballsMock[j - 1];
                    _ballsMock[j - 1] = _ballsMock[j];
                    _ballsMock[j] = temp;
                    
                    jState.Add(j);
                    StateMachine.AddSortingStep(GoToBallToReplace);
                    jState.Add(j);
                    StateMachine.AddSortingStep(GetBallToReplace);
                    jState.Add(j);
                    StateMachine.AddSortingStep(GoToPreviousBall);
                    jState.Add(j);
                    StateMachine.AddSortingStep(GetPreviousBall);
                    jState.Add(j);
                    StateMachine.AddSortingStep(PutBallToReplace);
                    jState.Add(j);
                    StateMachine.AddSortingStep(GoToBallToReplace);
                    jState.Add(j);
                    StateMachine.AddSortingStep(PutPreviousBall);
                }
            }
        }
    }

    private void GoToBallToReplace()
    {
        StateMachine.GoTo(GameManager.S.slots[jState[operationIndex++]].transform);
    }
    
    private void GetBallToReplace()
    {
        StateMachine.GetBall(GameManager.S.slots[jState[operationIndex++]], Hands.Right);
    }

    private void GoToPreviousBall()
    {
        StateMachine.GoTo(GameManager.S.slots[jState[operationIndex++] - 1].transform);
    }

    private void GetPreviousBall()
    {
        StateMachine.GetBall(GameManager.S.slots[jState[operationIndex++] - 1], Hands.Left);
    }

    private void PutBallToReplace()
    {
        StateMachine.PutBall(GameManager.S.slots[jState[operationIndex++] - 1], Hands.Right);
    }

    private void PutPreviousBall()
    {
        StateMachine.PutBall(GameManager.S.slots[jState[operationIndex++]], Hands.Left);
    }
}
