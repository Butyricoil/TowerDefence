using UnityEngine;
using Zenject;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _magnetRadius = 3f;
    
    private IPlayer _player;
    private bool _isBeingCollected;

    [Inject]
    private void Construct(IPlayer player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_isBeingCollected)
        {
            MoveTowardsPlayer();
        }
        else
        {
            CheckForPlayerProximity();
        }
    }

    private void CheckForPlayerProximity()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, _player.Position);
        if (distanceToPlayer <= _magnetRadius)
        {
            _isBeingCollected = true;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 playerPos = new Vector3(_player.Position.x, _player.Position.y, transform.position.z);
        Vector3 direction = (playerPos - transform.position).normalized;
        transform.position += direction * (_moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player.Wallet.AddCoins(1);
            Destroy(gameObject);
        }
    }
} 