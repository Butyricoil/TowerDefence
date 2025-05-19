using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class Building : MonoBehaviour
{
    [Header("Building Settings")]
    [SerializeField] protected BuildingType _buildingType;
    [SerializeField] protected int _maxWorkers = 1;
    [SerializeField] protected float _workRadius = 5f;
    [SerializeField] protected int _upgradeLevel = 1;
    [SerializeField] protected int _upgradeCost = 10;
    
    [Header("Production")]
    [SerializeField] protected float _productionInterval = 5f;
    [SerializeField] protected int _productionAmount = 1;
    
    protected List<Subject> _workers = new List<Subject>();
    protected float _productionTimer;
    protected IWallet _wallet;
    
    public BuildingType Type => _buildingType;
    public int UpgradeLevel => _upgradeLevel;
    public bool IsFullyStaffed => _workers.Count >= _maxWorkers;
    
    [Inject]
    private void Construct(IWallet wallet)
    {
        _wallet = wallet;
    }
    
    protected virtual void Update()
    {
        if (_workers.Count > 0)
        {
            _productionTimer += Time.deltaTime;
            if (_productionTimer >= _productionInterval)
            {
                _productionTimer = 0f;
                Produce();
            }
        }
    }
    
    public virtual bool TryAssignWorker(Subject worker)
    {
        if (_workers.Count >= _maxWorkers)
            return false;
            
        _workers.Add(worker);
        OnWorkerAssigned(worker);
        return true;
    }
    
    public virtual void RemoveWorker(Subject worker)
    {
        if (_workers.Remove(worker))
        {
            OnWorkerRemoved(worker);
        }
    }
    
    protected virtual void OnWorkerAssigned(Subject worker)
    {
        // Override in derived classes
    }
    
    protected virtual void OnWorkerRemoved(Subject worker)
    {
        // Override in derived classes
    }
    
    protected virtual void Produce()
    {
        // Override in derived classes
    }
    
    public virtual bool TryUpgrade()
    {
        if (!_wallet.TrySpendCoins(_upgradeCost))
            return false;
            
        _upgradeLevel++;
        _maxWorkers++;
        _productionAmount++;
        _productionInterval *= 0.9f; // 10% faster production
        
        OnUpgraded();
        return true;
    }
    
    protected virtual void OnUpgraded()
    {
        // Override in derived classes
    }
    
    public virtual void TakeDamage(int damage)
    {
        // Buildings are immune to damage by default
        // Override in derived classes if needed
    }
}

public enum BuildingType
{
    House,
    Farm,
    ArcherTower,
    Barracks,
    Wall,
    Gate
} 