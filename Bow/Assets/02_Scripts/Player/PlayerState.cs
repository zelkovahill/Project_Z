using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerLocomotion _playerLocomotion;
    protected PlayerInput _playerInput;

    public PlayerState(PlayerStateMachine playerStateMachine)
    {
        this._playerStateMachine = playerStateMachine;
        this._playerLocomotion = playerStateMachine.GetComponent<PlayerLocomotion>();
        this._playerInput = playerStateMachine.GetComponent<PlayerInput>();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        if (_playerInput.Horizontal != 0 || _playerInput.Vectical != 0)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetMoveState());
        }
    }
}

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        _playerLocomotion.Move();

        if (_playerInput.Horizontal == 0 && _playerInput.Vectical == 0)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }
    }
}


