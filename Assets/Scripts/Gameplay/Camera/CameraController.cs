using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] private IPlayer _player;

    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);

    private void LateUpdate()
    {
        Vector3 playerPosition = new Vector3(_player.Position.x, _player.Position.y, 0f);
        Vector3 desiredPosition = playerPosition + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
}