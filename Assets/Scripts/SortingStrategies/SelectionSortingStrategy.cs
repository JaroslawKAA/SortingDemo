using System.Linq;
using Robot;
using UnityEngine;

namespace SortingStrategies
{
    public class SelectionSortingStrategyBase : SortingStrategyBase
    {
        private int _index;
        private GameObject minimalBallSlot;

        public SelectionSortingStrategyBase(GameObject instance) : base(instance)
        {
            _index = 0;
        }

        public override void LoadSortingStrategy()
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
            if (_index >= GameManager.S.slots.Length)
                return;
            
            minimalBallSlot = GameManager.S.slots
                .Skip(_index)
                .OrderBy(o => o.transform
                    .GetChild(0)
                    .GetComponent<Ball>()
                    .Id)
                .First();
            
            StateMachine.GoTo(minimalBallSlot.transform);
        }

        private void GetMinimalBall()
        {
            if (minimalBallSlot.transform.childCount > 0)
            {
                StateMachine.GetBall(minimalBallSlot, Hands.Left); 
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void GoToRightBallPlace()
        {
            StateMachine.GoTo(GameManager.S.slots[_index].transform);
        }

        private void GetSecondBall()
        {
            if (GameManager.S.slots[_index].transform.childCount > 0)
            {
                StateMachine.GetBall(GameManager.S.slots[_index], Hands.Right);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void PutFirstBall()
        {
            if (RobotController.leftHandObject != null)
            {
                StateMachine.PutBall(GameManager.S.slots[_index], Hands.Left);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        private void GoToEmptySlot()
        {
            StateMachine.GoTo(minimalBallSlot.transform);
        }

        private void PutSecondBall()
        {
            if (RobotController.rightHandObject != null)
            {
                StateMachine.PutBall(minimalBallSlot, Hands.Right);
            }
            else
            {
                StateMachine.NextProcedure();
            }
        }

        protected override void OnProcedureComplete()
        {
            base.OnProcedureComplete();
            _index++;
        }
    }
}