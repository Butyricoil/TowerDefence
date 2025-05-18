using UnityEngine;
using Zenject;

// Важно: этот компонент должен быть только на префабе, не размещайте Player на сцене вручную!
public class PlayerController : MonoBehaviour, IPlayer
{
    [Inject] private PlayerConfig _config;
    
    private Rigidbody2D _rb;
    private bool _isMounted;
    private int _coins;
    private float _currentSpeed;
    
    public Vector2 Position => transform.position;
    public float Speed => _currentSpeed;
    public bool IsMounted => _isMounted;
    public int Coins => _coins;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = _config.walkSpeed;
        _coins = _config.startingCoins;
    }
    
    public void Move(float direction)
    {
        _rb.linearVelocity = new Vector2(direction * _currentSpeed, _rb.linearVelocity.y);
        
        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
    }
    
    public void Mount()
    {
        if (!_isMounted)
        {
            _isMounted = true;
            _currentSpeed = _config.mountedSpeed;
        }
    }
    
    public void Dismount()
    {
        if (_isMounted)
        {
            _isMounted = false;
            _currentSpeed = _config.walkSpeed;
        }
    }
    
    public void CollectCoin(int amount)
    {
        _coins += amount;
    }
} 