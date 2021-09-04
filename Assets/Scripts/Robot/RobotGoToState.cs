using System;
using Robot;
using UnityEngine;

/// <summary>
/// Go to selected place.
/// </summary>
public class RobotGoToState : RobotStateBase
{
    private Transform _target;
    private RobotMotor _motor;
    public bool TargetAchieved { get; private set; }
    public event Action onTargetAchieved;
    
    public RobotGoToState(GameObject instance) : base(instance)
    {
        _motor = instance.GetComponent<RobotMotor>();
        onTargetAchieved += InvokeNextProcedure;
    }

    private void InvokeNextProcedure()
    {
        _stateMachine.NextProcedure();
    }

    public override void OnEnter()
    {
        Debug.Log("Enter GoToState");
        TargetAchieved = false;
    }

    public override void OnUpdate()
    {
        if (TargetAchieved) return;
        
        if (_target.transform.position.x > _instance.transform.position.x)
        {
            _motor.MoveRight();
            if (_instance.transform.position.x >= _target.transform.position.x)
            {
                ClampXPosition();
                TargetAchieved = true;
                onTargetAchieved?.Invoke();
            }
        }
        else
        {
            _motor.MoveLeft();
            if (_instance.transform.position.x <= _target.transform.position.x)
            {
                ClampXPosition();
                TargetAchieved = true;
                onTargetAchieved?.Invoke();
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Exit GoToState");
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void ClampXPosition()
    {
        _instance.transform.position = new Vector3(
            _target.position.x,
            _instance.transform.position.y,
            _instance.transform.position.z
        );
    }
}
