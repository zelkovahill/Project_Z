using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    private PlayerIdleState _idleState;
    private PlayerMoveState _moveState;
    private PlayerSprintState _sprintState;
    private PlayerSquatState _squatState;
    private PlayerSquatMoveState _squatMoveState;

    public PlayerIdleState GetIdleState() => _idleState;
    public PlayerMoveState GetMoveState() => _moveState;
    public PlayerSprintState GetSprintState() => _sprintState;
    public PlayerSquatState GetSquatState() => _squatState;
    public PlayerSquatMoveState GetSquatMoveState() => _squatMoveState;

    private void Awake()
    {
        _idleState = new PlayerIdleState(this);
        _moveState = new PlayerMoveState(this);
        _sprintState = new PlayerSprintState(this);
        _squatState = new PlayerSquatState(this);
    }

    private void Start()
    {
        TransitionToState(_idleState);
    }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void TransitionToState(PlayerState newState)
    {
        if (CurrentState == newState)
        {
            return;
        }

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
