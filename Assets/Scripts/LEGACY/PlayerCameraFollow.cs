using UnityEngine;

namespace LEGACY
{
    public class PlayerCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _smoothSpeed = 0.05f;
        [SerializeField] private Vector3 _offset;

        private void LateUpdate()
        {
            if (!_target) return;

            Vector3 desiredPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}