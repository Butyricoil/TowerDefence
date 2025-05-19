using UnityEngine;
using Zenject;

public class Resident : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _wanderRadius = 5f;
    [SerializeField] private float _wanderTimer = 3f;
    
    private ResidentSpawnPoint _spawnPoint;
    private Vector3 _targetPosition;
    private float _currentWanderTimer;
    private ResidentSpawner _spawner;

    [Inject]
    private void Construct(ResidentSpawner spawner)
    {
        _spawner = spawner;
    }

    public void Initialize(ResidentSpawnPoint spawnPoint)
    {
        _spawnPoint = spawnPoint;
        _spawnPoint.SetOccupied(true);
        transform.position = _spawnPoint.Position;
        SetNewWanderTarget();
    }

    private void Update()
    {
        MoveTowardsTarget();
        UpdateWanderTimer();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (_targetPosition - transform.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void UpdateWanderTimer()
    {
        _currentWanderTimer += Time.deltaTime;
        if (_currentWanderTimer >= _wanderTimer)
        {
            _currentWanderTimer = 0f;
            SetNewWanderTarget();
        }
    }

    private void SetNewWanderTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _wanderRadius;
        _targetPosition = _spawnPoint.Position + new Vector3(randomCircle.x, randomCircle.y, 0);
    }

    private void OnDestroy()
    {
        if (_spawnPoint != null)
        {
            _spawnPoint.SetOccupied(false);
        }
        _spawner?.OnResidentDestroyed();
    }
} 