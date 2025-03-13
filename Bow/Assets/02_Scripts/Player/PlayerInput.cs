using NUnit.Framework;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("[입력키 설정]")]
    [SerializeField, Tooltip("앞 키")] private KeyCode forwardKey = KeyCode.W;
    [SerializeField, Tooltip("뒤 키")] private KeyCode backKey = KeyCode.S;
    [SerializeField, Tooltip("왼쪽 키")] private KeyCode leftKey = KeyCode.A;
    [SerializeField, Tooltip("오른쪽 키")] private KeyCode rightKey = KeyCode.D;
    [SerializeField, Tooltip("달리기 키")] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField, Tooltip("구르기 키")] private KeyCode rollKey = KeyCode.Space;
    [SerializeField, Tooltip("앉기 키")] private KeyCode squatKey = KeyCode.C;
    // [SerializeField, Tooltip("점프 키")] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField, Tooltip("에임 키")] private KeyCode JoomKey = KeyCode.Mouse1;
    [SerializeField, Tooltip("공격 키")] private KeyCode attackKey = KeyCode.Mouse0;

    [Header("[마우스 설정]")]
    [Tooltip("마우스 감도")] public float mouseSensitivity = 1.0f;

    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";

    // 입력값
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public float Horizontal { get; private set; }
    public float Vectical { get; private set; }
    public bool IsSprint { get; private set; }
    public bool IsJump { get; private set; }
    public bool IsSquat { get; private set; }
    public bool IsJoom { get; private set; }
    public bool IsRoll { get; private set; }
    public bool IsAttack { get; private set; }
    public bool cursorLocked { get; private set; } = true;
    public bool cursorInputForLock { get; private set; } = true;



    private void Update()
    {
        CameraInput();
        MoveInput();
        SprintInput();
        // JumpInput();
        JoomInput();
        SquatInput();
        RollInput();
        AttackInput();
    }

    private void CameraInput()
    {
        MouseX = Input.GetAxisRaw(MOUSE_X) * mouseSensitivity;
        MouseY = Input.GetAxisRaw(MOUSE_Y) * mouseSensitivity;
    }

    private void MoveInput()
    {
        Horizontal = 0;
        Vectical = 0;

        if (Input.GetKey(forwardKey))
        {
            Vectical = 1;
        }

        if (Input.GetKey(backKey))
        {
            Vectical = -1;
        }

        if (Input.GetKey(leftKey))
        {
            Horizontal = -1;
        }

        if (Input.GetKey(rightKey))
        {
            Horizontal = 1;
        }
    }

    private void SquatInput()
    {
        if (Input.GetKeyDown(squatKey))
        {
            IsSquat = !IsSquat;
        }
    }

    private void SprintInput()
    {
        IsSprint = Input.GetKey(sprintKey);
    }

    private void AttackInput()
    {
        IsAttack = Input.GetKeyDown(attackKey);
    }

    // private void JumpInput()
    // {
    //     IsJump = Input.GetKeyDown(jumpKey);
    // }

    private void RollInput()
    {
        IsRoll = Input.GetKeyDown(rollKey);
    }

    private void JoomInput()
    {
        IsJoom = Input.GetKey(JoomKey);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}
