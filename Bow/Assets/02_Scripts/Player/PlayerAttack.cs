using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Tooltip("화살 프리팹")] private GameObject _arrowPrefab;
    [SerializeField, Tooltip("화살 발사 위치")] private Transform _arrowSpawnPoint;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Shoot()
    {
        GameObject arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position + _arrowSpawnPoint.forward, transform.rotation);
        arrow.GetComponent<Rigidbody>().AddForce(_arrowSpawnPoint.forward * 100f);
    }
}
