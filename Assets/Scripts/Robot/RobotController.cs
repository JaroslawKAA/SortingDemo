using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Robot
{
    public class RobotController : MonoBehaviour
    {
        [Header("Settings")] public GameObject rightHandPivot;
        public GameObject leftHandPivot;
        public GameObject rightHandObject;
        public GameObject leftHandObject;
        
        private SortingStrategyBase SortingStrategy { get; set; }
        private bool _isSorting;
        private RobotStateMachine _stateMachine;

        private void Awake()
        {
            Assert.IsNotNull(rightHandPivot);
            Assert.IsNotNull(leftHandPivot);
        }

        // Start is called before the first frame update
        void Start()
        {
            GameEvents.S.onStartSorting += StartSorting;
            _stateMachine = GetComponent<RobotStateMachine>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isSorting) return;
        }
    
        private void StartSorting(SorthingMethod sortingMethod)
        {
            SortingStrategy = SortingStrategyFactory.GetStrategy(sortingMethod, gameObject);
            _isSorting = true;
            
            SortingStrategy.Sort();
        }
    }
}
