using Robot;
using UnityEngine;

public abstract class RobotStateBase
{
    protected GameObject _instance;
    protected RobotStateMachine _stateMachine;

    public RobotStateBase(GameObject instance)
    {
        _instance = instance;
        _stateMachine = instance.GetComponent<RobotStateMachine>();
    }
    
    /// <summary>
    /// Execute on enter to current state
    /// </summary>
    public virtual void OnEnter()
    {
    }

    /// <summary>
    /// Execute on exit from current state
    /// </summary>
    public virtual void OnExit()
    {
    }

    // Update is called once per frame
    public abstract void OnUpdate();
}
