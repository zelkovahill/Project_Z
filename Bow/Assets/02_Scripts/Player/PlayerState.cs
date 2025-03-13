using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine _playerStateMachine;
    protected PlayerLocomotion _playerLocomotion;
    protected PlayerInput _playerInput;
    protected PlayerCamera _playerCamera;
    protected PlayerUI _playerUI;

    public PlayerState(PlayerStateMachine playerStateMachine)
    {
        this._playerStateMachine = playerStateMachine;
        this._playerLocomotion = playerStateMachine.GetComponent<PlayerLocomotion>();
        this._playerInput = playerStateMachine.GetComponent<PlayerInput>();
        _playerCamera = playerStateMachine.GetComponent<PlayerCamera>();
        _playerUI = playerStateMachine.GetComponent<PlayerUI>();
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

        if (_playerInput.IsJoom)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetJoomState());
        }

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

        if (_playerInput.IsRoll)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetRollState());
        }

        if (_playerInput.IsJoom)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetJoomState());
        }

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

public class PlayerJoomState : PlayerState
{
    public PlayerJoomState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        _playerUI.AimOn();
        _playerCamera.ZoomIn();

    }

    public override void Update()
    {
        _playerCamera.AimingCamera();
        _playerLocomotion.Move();

        if (!_playerInput.IsJoom)
        {
            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }
    }

    public override void Exit()
    {
        _playerUI.AimOff();
        _playerCamera.AimingCamera();
        _playerCamera.ZoomOut();
    }
}

public class PlayerRollState : PlayerState
{
    private float _rollDuration = 0.8f;
    float _rollTime;
    private Vector3 _rollDirection;
    private bool _isRolling;

    public PlayerRollState(PlayerStateMachine playerStateMachine) : base(playerStateMachine) { }

    public override void Enter()
    {
        _rollTime = 0;
        _isRolling = true;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        _rollDirection = (forward * _playerInput.Vectical + right * _playerInput.Horizontal).normalized;

        if (_rollDirection.sqrMagnitude < 0.01f)
        {
            _rollDirection = _playerStateMachine.transform.forward;
        }

        _playerStateMachine.transform.rotation = Quaternion.LookRotation(_rollDirection);

    }

    public override void Update()
    {
        if (!_isRolling)
        {
            return;
        }

        _rollTime += Time.deltaTime;

        _playerLocomotion.Roll(_rollDirection);

        if (_rollTime > _rollDuration)
        {

            _playerStateMachine.TransitionToState(_playerStateMachine.GetIdleState());
        }
    }

    public override void Exit()
    {
        _isRolling = false;
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

