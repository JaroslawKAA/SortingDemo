using System.Linq;
using Robot;
using UnityEngine;

namespace SortingStrategies
{
    public class SelectionSortingStrategyBase : SortingStrategyBase
    {
        private int index = 0;
        private GameObject slotToReplace;
        private RobotController _robotController;

        public SelectionSortingStrategyBase(GameObject instance) : base(instance)
        {
            StateMachine.onProcedureComplete += OnProcedureComplete;
            _robotController = instance.GetComponent<RobotController>();
        }

        public override void Sort()
        {
            StateMachine.AddSortingStep(GoToMinimalBall);
            StateMachine.AddSortingStep(GetMinimalBall);
            StateMachine.AddSortingStep(GoToRightBallPlace);
            StateMachine.AddSortingStep(GetSecondBall);
            StateMachine.AddSortingStep(PutFirstBall);
            StateMachine.AddSortingStep(GoToEmptySlot);
            StateMachine.AddSortingStep(PutSecondBall);
        }
        
        private void GoToMinimalBall()
        {
            if (index >= GameManager.S.slots.Length)
                return;
            
            GameObject minimumBall = GameManager.S.slots
                .Skip(index)
                .OrderBy(o => o.transform
                    .GetChild(0)
                    .GetComponent<Ball>()
                    .Id)
                .First();
            slotToReplace = minimumBall;
            
            StateMachine.GoTo(slotToReplace.transform);
        }

        private void GetMinimalBall()
        {
            if (slotToReplace.transform.childCount > 0)
            {
                StateMachine.GetBall(slotToReplace, Hands.Left); 
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void GoToRightBallPlace()
        {
            StateMachine.GoTo(GameManager.S.slots[index].transform);
        }

        private void GetSecondBall()
        {
            if (GameManager.S.slots[index].transform.childCount > 0)
            {
                StateMachine.GetBall(GameManager.S.slots[index], Hands.Right);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void PutFirstBall()
        {
            if (_robotController.leftHandObject != null)
            {
                StateMachine.PutBall(GameManager.S.slots[index], Hands.Left);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void GoToEmptySlot()
        {
            StateMachine.GoTo(slotToReplace.transform);
        }

        private void PutSecondBall()
        {
            if (_robotController.rightHandObject != null)
            {
                StateMachine.PutBall(slotToReplace, Hands.Right);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void OnProcedureComplete()
        {
            index++;
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
            Invoke_OnSortingComplete();
        }
    }
}