using UnityEngine;
using Zenject;
using Kingdom.Core.Domain.Interfaces;

public class CameraController : MonoBehaviour
{
    [Inject] private IPlayer _player;
    
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);
    
    private void LateUpdate()
    {
        Vector3 desiredPosition = _player.Position * _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothedPosition;
    }
} 