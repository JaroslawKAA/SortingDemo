using Robot;
using UnityEngine;

public class RobotGetBallState : RobotStateBase
{
    private GameObject _targetBall;
    private Hands _handToGetBall;
    private Animator _animator;
    private RobotController _controller;
    private bool _haveBall;

    private static readonly int RightHand = Animator.StringToHash("RightHand");
    private static readonly int GetBall = Animator.StringToHash("GetBall");

    public RobotGetBallState(GameObject instance) : base(instance)
    {
        _animator = _instance.GetComponent<Animator>();
        _controller = _instance.GetComponent<RobotController>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetBool(RightHand, _handToGetBall == Hands.Right);
        _animator.SetBool(GetBall, true);
        Debug.Log("Enter GetBallState");
    }

    public override void OnUpdate()
    {
        if (_haveBall && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            _stateMachine.NextProcedure();
            return;
        }
        
        if (_haveBall)
            return;
        
        if (_handToGetBall == Hands.Right)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("GetBallRight")
                && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            {
                _targetBall.transform.parent = _controller.rightHandPivot.transform;
                _targetBall.transform.localPosition = new Vector3();
                _controller.rightHandObject = _targetBall;
                _haveBall = true;
                _animator.SetBool(GetBall, false);
            }
        }
        else
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("GetBallLeft")
                && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .5f)
            {
                _targetBall.transform.parent = _controller.leftHandPivot.transform;
                _targetBall.transform.localPosition = new Vector3();
                _controller.leftHandObject = _targetBall;
                _haveBall = true;
                _animator.SetBool(GetBall, false);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        _animator.SetBool(GetBall, false);
        _haveBall = false;
        Debug.Log("Exit GetBallState");
    }

    public void SetTarget(GameObject slot, Hands hand)
    {
        _targetBall = slot.transform.GetChild(0).gameObject;
        _handToGetBall = hand;
    }
}