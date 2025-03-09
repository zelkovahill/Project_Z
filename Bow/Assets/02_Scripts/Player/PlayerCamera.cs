using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("[카메라 설정]")]
    [SerializeField, Tooltip("카메라 타겟 객체")] private Transform _cameraTarget;
    [SerializeField, Tooltip("카메라 회전 상단 제한 각도")] private float _topClampAngle = 70.0f;
    [SerializeField, Tooltip("카메라 회전 하단 제한 각도")] private float _bottomClampAngle = -30.0f;

    private float _cameraTargetYaw;
    private float _cameraTargetPitch;
    private const float THRESHOLD = 0.01f;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        _cameraTargetYaw += _playerInput.MouseX * _playerInput.mouseSensitivity;
        _cameraTargetPitch -= _playerInput.MouseY * _playerInput.mouseSensitivity;

        _cameraTargetYaw = ClampAngle(_cameraTargetYaw, -360, 360);
        _cameraTargetPitch = ClampAngle(_cameraTargetPitch, _bottomClampAngle, _topClampAngle);

        _cameraTarget.rotation = Quaternion.Euler(_cameraTargetPitch, _cameraTargetYaw, 0);

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }

        if (angle > 360)
        {
            angle -= 360;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
