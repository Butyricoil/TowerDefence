using UnityEngine;
using Zenject;

public class Subject : MonoBehaviour
{
    [Header("Basic Stats")]
    [SerializeField] protected float _moveSpeed = 3f;
    [SerializeField] protected float _workRadius = 5f;
    [SerializeField] protected int _maxHealth = 100;
    
    [Header("State")]
    [SerializeField] protected SubjectState _currentState;
    [SerializeField] protected SubjectType _subjectType;
    
    protected int _currentHealth;
    protected Vector3 _targetPosition;
    protected Building _currentWorkplace;
    protected IWallet _wallet;
    
    public SubjectType Type => _subjectType;
    public SubjectState State => _currentState;
    public bool IsArmed => _currentWeapon != null;
    
    protected Weapon _currentWeapon;
    
    [Inject]
    private void Construct(IWallet wallet)
    {
        _wallet = wallet;
        _currentHealth = _maxHealth;
    }
    
    protected virtual void Start()
    {
        SetState(SubjectState.Idle);
    }
    
    protected virtual void Update()
    {
        switch (_currentState)
        {
            case SubjectState.Idle:
                UpdateIdle();
                break;
            case SubjectState.Working:
                UpdateWorking();
                break;
            case SubjectState.Fighting:
                UpdateFighting();
                break;
            case SubjectState.Fleeing:
                UpdateFleeing();
                break;
        }
    }
    
    protected virtual void UpdateIdle()
    {
        // Look for work or threats
        if (_currentWorkplace == null)
        {
            FindWork();
        }
    }
    
    protected virtual void UpdateWorking()
    {
        if (_currentWorkplace == null)
        {
            SetState(SubjectState.Idle);
            return;
        }
        
        // Work logic in derived classes
    }
    
    protected virtual void UpdateFighting()
    {
        // Combat logic in derived classes
    }
    
    protected virtual void UpdateFleeing()
    {
        // Run away from threats
        if (_currentWorkplace != null)
        {
            MoveTowards(_currentWorkplace.transform.position);
        }
    }
    
    protected void SetState(SubjectState newState)
    {
        _currentState = newState;
        OnStateChanged(newState);
    }
    
    protected virtual void OnStateChanged(SubjectState newState)
    {
        // Override in derived classes
    }
    
    protected void MoveTowards(Vector3 target)
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
    
    protected virtual void FindWork()
    {
        // Override in derived classes
    }
    
    public virtual void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
        else if (_currentHealth < _maxHealth * 0.3f)
        {
            SetState(SubjectState.Fleeing);
        }
    }
    
    protected virtual void Die()
    {
        // Drop coins
        if (_wallet != null)
        {
            _wallet.AddCoins(Random.Range(1, 4));
        }
        
        Destroy(gameObject);
    }
    
    public virtual void EquipWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        // Update appearance and stats
    }
}

public enum SubjectState
{
    Idle,
    Working,
    Fighting,
    Fleeing
}

public enum SubjectType
{
    Villager,
    Archer,
    Builder,
    Knight
} 