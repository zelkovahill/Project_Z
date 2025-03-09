using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [Header("[이동 설정]")]
    [SerializeField, Tooltip("걷기 속도")] private float _walkSpeed = 2.5f;
    [SerializeField, Tooltip("달리기 속도")] private float _sprintSpeed = 5.0f;

    [Header("[회전 설정]")]
    [SerializeField, Tooltip("회전 속도")] private float _rotationSpeed = 15.0f;

    private Camera _camera;
    private PlayerInput _playerInput;
    private CharacterController _characterController;

    private void Awake()
    {
        _camera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    public void Move()
    {
        float currentSpeed = _playerInput.IsSprint ? _sprintSpeed : _walkSpeed;

        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * _playerInput.Vectical + right * _playerInput.Horizontal).normalized;

        _characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        if (_playerInput.IsSprint)
        {
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (moveDirection.sqrMagnitude > 0.01f)
            {
                Quaternion toRotation = Quaternion.LookRotation(forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}
