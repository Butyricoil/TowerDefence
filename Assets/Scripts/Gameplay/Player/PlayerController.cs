using UnityEngine;
using Zenject;

// Важно: этот компонент должен быть только на префабе, не размещайте Player на сцене вручную!
public class PlayerController : MonoBehaviour, IPlayer
{
    [Inject] private PlayerConfig _config;
    
    private Rigidbody2D _rb;
    private int _coins;
    private float _currentSpeed;
    private bool _isGrounded;
    
    public Vector2 Position => transform.position;
    public float Speed => _currentSpeed;
    public int Coins => _coins;
    public bool IsMounted { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = _config.walkSpeed;
        _coins = _config.startingCoins;
    }
    
    public void Move(Vector2 direction)
    {
        _rb.linearVelocity = new Vector2(direction.x * _currentSpeed, _rb.linearVelocity.y);
        
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.AddForce(Vector2.up * _config.jumpForce, ForceMode2D.Impulse);
        }
    }
    
    public void Sprint(bool isSprinting)
    {
        _currentSpeed = isSprinting ? _config.mountedSpeed : _config.walkSpeed;
    }

    public void Dismount()
    {
        throw new System.NotImplementedException();
    }

    public void Mount()
    {
        throw new System.NotImplementedException();
    }

    public void CollectCoin(int amount)
    {
        _coins += amount;
    }
    
    public void DropCoin()
    {
        if (_coins > 0)
        {
            _coins--;
            Debug.Log("Монетка брошена на пол!");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
} 