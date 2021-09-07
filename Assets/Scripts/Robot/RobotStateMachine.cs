using System.Collections.Generic;
using UnityEngine;

namespace Robot
{
    public class RobotStateMachine : MonoBehaviour
    {

        private RobotStateBase _currentState;

        private RobotStateBase CurrentState
        {
            get => _currentState;
            set
            {
                _currentState?.OnExit();
                _currentState = value;
                _currentState.OnEnter();
                GameEvents.S.Invoke_OnRobotStateChange();
            }
        }

        private RobotGoToState GoToState { get; set; }
        private RobotGetBallState GetBallState { get; set; }
        private RobotPutBallState PutBallState { get; set; }

        private bool _runSorting;
        private Animator _animator;

        public delegate void SortingStep();

        private List<SortingStep> SortingProcedure { get; set; }

        private int procedureIndex = 0;

        public void IncrementProcedureIndex()
        {
            procedureIndex++;
            if (procedureIndex >= SortingProcedure.Count)
            {
                GameEvents.S.Invoke_OnProcedureComplete();
                procedureIndex = 0;
            }
        }

        public void AddSortingStep(SortingStep step)
        {
            SortingProcedure.Add(step);
        }

        // Start is called before the first frame update
        void Start()
        {
            GoToState = new RobotGoToState(gameObject);
            GetBallState = new RobotGetBallState(gameObject);
            PutBallState = new RobotPutBallState(gameObject);
            CurrentState = GoToState;

            SortingProcedure = new List<SortingStep>();

            _animator = GetComponent<Animator>();

            GameEvents.S.onStartSorting += StartSorting;
            GameEvents.S.onSortingComplete += StopSorting;
        }

        // Update is called once per frame

        void Update()
        {
            if (!_runSorting) return;

            CurrentState.OnUpdate();
        }

        public void GoTo(Transform slot)
        {
            CurrentState = GoToState;
            GoToState.SetTarget(slot);
        }

        private void StartSorting(SorthingMethod obj)
        {
            SortingProcedure[0]?.Invoke();
            _runSorting = true;
        }

        public void GetBall(GameObject slotToReplace, Hands hand)
        {
            GetBallState.SetTarget(slotToReplace, hand);
            CurrentState = GetBallState;
        }

        public void PutBall(GameObject slotToPlace, Hands hand)
        {
            PutBallState.SetTarget(slotToPlace, hand);
            CurrentState = PutBallState;
        }

        public void NextProcedure()
        {
            IncrementProcedureIndex();
            if (procedureIndex < SortingProcedure.Count)
            {
                SortingProcedure[procedureIndex]?.Invoke();
            }
        }

        public void StopSorting()
        {
            _runSorting = false;
            SortingProcedure = new List<SortingStep>();
            procedureIndex = 0;
            _animator.SetBool("GetBall", false);
        }
        
    }
}