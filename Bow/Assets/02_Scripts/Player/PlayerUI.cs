using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField, Tooltip("플레이어 에임 UI")] private GameObject _aimUI;


    public void AimOn()
    {
        _aimUI.SetActive(true);
    }

    public void AimOff()
    {
        _aimUI.SetActive(false);
    }
}
