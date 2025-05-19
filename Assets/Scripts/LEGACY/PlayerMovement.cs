using UnityEngine;

namespace LEGACY
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _runMultiplier = 2f;
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.2f;

        private bool _isGrounded;

        private void FixedUpdate()
        {
            float moveInput = Input.GetAxis("Horizontal");
            bool runHeld = Input.GetKey(KeyCode.LeftShift);
            float speed = runHeld ? _moveSpeed * _runMultiplier : _moveSpeed;
            _rb.linearVelocity = new Vector2(moveInput * speed, _rb.linearVelocity.y);
        }

        private void Update()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            }
        }
    }
}