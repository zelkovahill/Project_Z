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
            if (_playerInput.IsSquat)
            {
                _playerStateMachine.TransitionToState(_playerStateMachine.GetSquatState());
            }

            if (_playerInput.IsSprint)
            {
                _playerStateMachine.TransitionToState(_playerStateMachine.GetSprintState());
            }
            else
            {
                _playerStateMachine.TransitionToState(_playerStateMachine.GetMoveState());
            }

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

        if ((_playerInput.Horizontal != 0 || _playerInput.Vectical != 0) && _playerInput.IsSprint)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetSprintState());
        }
    }
}


public class PlayerSprintState : PlayerState
{
    public PlayerSprintState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        _playerLocomotion.Move();

        if ((_playerInput.Horizontal != 0 || _playerInput.Vectical != 0) && !_playerInput.IsSprint)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetMoveState());
        }

        if (_playerInput.Horizontal == 0 && _playerInput.Vectical == 0)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }
    }
}

public class PlayerSquatState : PlayerState
{
    public PlayerSquatState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        if (!_playerInput.IsSquat)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }

        if (_playerInput.Horizontal != 0 || _playerInput.Vectical != 0)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetSquatMoveState());
        }

    }
}

public class PlayerSquatMoveState : PlayerState
{
    public PlayerSquatMoveState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Update()
    {
        _playerLocomotion.Move();


    }

}

