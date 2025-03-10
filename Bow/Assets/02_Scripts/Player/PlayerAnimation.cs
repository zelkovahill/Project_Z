using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _animator;
    private PlayerStateMachine _playerStateMachine;
    private PlayerState _currentState => _playerStateMachine.CurrentState;

    private static readonly int HORIZONTAL_HASH = Animator.StringToHash("Horizontal");
    private static readonly int VERTICAL_HASH = Animator.StringToHash("Vertical");
    private static readonly int SQUAT_HASH = Animator.StringToHash("Squat");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {

        ResetBool();

        switch (_currentState)
        {
            case PlayerIdleState idleState:
                _animator.SetFloat(HORIZONTAL_HASH, 0, 0.2f, Time.deltaTime);
                _animator.SetFloat(VERTICAL_HASH, 0, 0.2f, Time.deltaTime);
                break;

            case PlayerMoveState moveState:
                _animator.SetFloat(HORIZONTAL_HASH, _playerInput.Horizontal / 2, 0.2f, Time.deltaTime);
                _animator.SetFloat(VERTICAL_HASH, _playerInput.Vectical / 2, 0.2f, Time.deltaTime);
                break;

            case PlayerSprintState sprintState:
                _animator.SetFloat(HORIZONTAL_HASH, 0, 0.2f, Time.deltaTime);
                _animator.SetFloat(VERTICAL_HASH, 2, 0.2f, Time.deltaTime);
                break;

            case PlayerSquatState squatState:
                _animator.SetBool(SQUAT_HASH, true);
                break;
        }
    }

    private void ResetBool()
    {
        _animator.SetBool(SQUAT_HASH, false);
    }
}
