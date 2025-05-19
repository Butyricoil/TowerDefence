using UnityEngine;
using Zenject;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] protected WeaponType _weaponType;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected float _attackRange = 2f;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] protected float _projectileSpeed = 10f;
    
    [Header("References")]
    [SerializeField] protected GameObject _projectilePrefab;
    [SerializeField] protected Transform _firePoint;
    
    protected float _currentCooldown;
    protected Subject _owner;
    
    public WeaponType Type => _weaponType;
    public int Damage => _damage;
    public float AttackRange => _attackRange;
    
    public virtual void Initialize(Subject owner)
    {
        _owner = owner;
        transform.SetParent(owner.transform);
        transform.localPosition = Vector3.zero;
    }
    
    protected virtual void Update()
    {
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
    }
    
    public virtual bool TryAttack(Vector3 target)
    {
        if (_currentCooldown > 0)
            return false;
            
        float distance = Vector3.Distance(transform.position, target);
        if (distance > _attackRange)
            return false;
            
        PerformAttack(target);
        _currentCooldown = _attackCooldown;
        return true;
    }
    
    protected virtual void PerformAttack(Vector3 target)
    {
        switch (_weaponType)
        {
            case WeaponType.Melee:
                PerformMeleeAttack(target);
                break;
            case WeaponType.Ranged:
                PerformRangedAttack(target);
                break;
        }
    }
    
    protected virtual void PerformMeleeAttack(Vector3 target)
    {
        // Check for enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRange);
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }
    
    protected virtual void PerformRangedAttack(Vector3 target)
    {
        if (_projectilePrefab == null)
            return;
            
        Vector3 direction = (target - transform.position).normalized;
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
        
        var projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.Initialize(direction, _damage, _projectileSpeed);
        }
    }
}

public enum WeaponType
{
    Melee,
    Ranged
} 