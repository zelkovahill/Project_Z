using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("에임 리그")] private Rig aimRig;

    private PlayerInput _playerInput;
    private Animator _animator;
    private PlayerStateMachine _playerStateMachine;
    private PlayerState _currentState => _playerStateMachine.CurrentState;

    private static readonly int HORIZONTAL_HASH = Animator.StringToHash("Horizontal");
    private static readonly int VERTICAL_HASH = Animator.StringToHash("Vertical");
    private static readonly int SQUAT_HASH = Animator.StringToHash("Squat");
    private static readonly int JOOM_HASH = Animator.StringToHash("Joom");
    private static readonly int ROLL_HASH = Animator.StringToHash("Roll");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {

        ResetBool();
        float targetLayerWeight = 0f;
        float rigWeight = 0f;

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

            case PlayerJoomState joomState:
                _animator.SetFloat(HORIZONTAL_HASH, _playerInput.Horizontal / 2, 0.2f, Time.deltaTime);
                _animator.SetFloat(VERTICAL_HASH, _playerInput.Vectical / 2, 0.2f, Time.deltaTime);
                _animator.SetBool(JOOM_HASH, true);
                targetLayerWeight = 1f;
                rigWeight = 1f;
                break;

            case PlayerRollState rollState:
                _animator.SetBool(ROLL_HASH, true);
                break;

        }

        _animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), targetLayerWeight, Time.deltaTime * 50f));
        SetRigWeight(Mathf.Lerp(aimRig.weight, rigWeight, Time.deltaTime * 50f));
    }

    private void ResetBool()
    {
        _animator.SetBool(SQUAT_HASH, false);
        _animator.SetBool(JOOM_HASH, false);
        _animator.SetBool(ROLL_HASH, false);
    }

    private void SetRigWeight(float weight)
    {
        aimRig.weight = weight;
    }


}
