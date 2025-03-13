using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("[카메라 설정]")]
    [SerializeField, Tooltip("카메라 타겟 객체")] private Transform _cameraTarget;
    [SerializeField, Tooltip("카메라 회전 상단 제한 각도")] private float _topClampAngle = 70.0f;
    [SerializeField, Tooltip("카메라 회전 하단 제한 각도")] private float _bottomClampAngle = -30.0f;

    [Header("[줌 설정]")]
    [SerializeField, Tooltip("플레이어 줌 카메라 오브젝트")] private GameObject _playerZoomCamera;

    [Header("[에임 설정]")]
    [SerializeField, Tooltip("에임 레이어 마스크")] private LayerMask _aimLayerMask;
    [SerializeField, Tooltip("에임 오브젝트")] private GameObject _aimObject;
    [SerializeField, Tooltip("에임 거리")] private float _aimDistance = 20f;


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

    public void ZoomIn()
    {
        _playerZoomCamera.SetActive(true);
    }

    public void ZoomOut()
    {
        _playerZoomCamera.SetActive(false);
    }

    public void AimingCamera()
    {
        Vector3 targetPosition;
        Transform camTransform = Camera.main.transform;
        RaycastHit hit;

        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, Mathf.Infinity, _aimLayerMask))
        {
            targetPosition = hit.point;
            _aimObject.transform.position = hit.point;
        }
        else
        {
            targetPosition = camTransform.position + camTransform.forward * _aimDistance;
            _aimObject.transform.position = camTransform.position + camTransform.forward * _aimDistance;
        }

        Vector3 targetAim = targetPosition;
        targetAim.y = _cameraTarget.position.y;
        Vector3 aimDirection = (targetAim - _cameraTarget.position).normalized;

        transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 50f);
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
