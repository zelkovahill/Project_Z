using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }

    private PlayerIdleState _idleState;
    private PlayerMoveState _moveState;

    public PlayerIdleState GetIdleState() => _idleState;
    public PlayerMoveState GetMoveState() => _moveState;

    private void Awake()
    {
        _idleState = new PlayerIdleState(this);
        _moveState = new PlayerMoveState(this);
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
