using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected EnemyType _enemyType;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected float _attackRange = 1.5f;
    [SerializeField] protected float _attackCooldown = 1f;
    
    [Header("Loot")]
    [SerializeField] protected int _minCoins = 1;
    [SerializeField] protected int _maxCoins = 3;
    
    protected int _currentHealth;
    protected float _currentCooldown;
    protected Vector3 _targetPosition;
    protected Subject _currentTarget;
    protected IWallet _wallet;
    
    public EnemyType Type => _enemyType;
    
    [Inject]
    private void Construct(IWallet wallet)
    {
        _wallet = wallet;
        _currentHealth = _maxHealth;
    }
    
    protected virtual void Update()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        
        UpdateTarget();
        UpdateMovement();
        UpdateAttack();
    }
    
    protected virtual void UpdateTarget()
    {
        if (_currentTarget == null || !_currentTarget.gameObject.activeInHierarchy)
        {
            FindNewTarget();
        }
    }
    
    protected virtual void UpdateMovement()
    {
        if (_currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, _currentTarget.transform.position);
            if (distance > _attackRange)
            {
                MoveTowards(_currentTarget.transform.position);
            }
        }
    }
    
    protected virtual void UpdateAttack()
    {
        if (_currentTarget != null && _currentCooldown <= 0)
        {
            float distance = Vector3.Distance(transform.position, _currentTarget.transform.position);
            if (distance <= _attackRange)
            {
                Attack();
            }
        }
    }
    
    protected virtual void FindNewTarget()
    {
        // Find nearest subject or building
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        float closestDistance = float.MaxValue;
        Subject closestSubject = null;
        
        foreach (var collider in colliders)
        {
            var subject = collider.GetComponent<Subject>();
            if (subject != null)
            {
                float distance = Vector3.Distance(transform.position, subject.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestSubject = subject;
                }
            }
        }
        
        _currentTarget = closestSubject;
    }
    
    protected virtual void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * _moveSpeed * Time.deltaTime;
        
        // Flip sprite based on movement direction
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(direction.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
    
    protected virtual void Attack()
    {
        if (_currentTarget != null)
        {
            _currentTarget.TakeDamage(_damage);
            _currentCooldown = _attackCooldown;
        }
    }
    
    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    
    protected virtual void Die()
    {
        // Drop coins
        if (_wallet != null)
        {
            int coins = Random.Range(_minCoins, _maxCoins + 1);
            _wallet.AddCoins(coins);
        }
        
        Destroy(gameObject);
    }
}

public enum EnemyType
{
    Greedling,
    Breeder,
    Portal,
    CrownStealer
} 