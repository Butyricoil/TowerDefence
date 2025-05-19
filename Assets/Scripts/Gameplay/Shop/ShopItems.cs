using UnityEngine;
using Zenject;

public abstract class BaseShopItem : IShopItem
{
    protected ShopItemData _data;
    protected int _usesLeft;
    
    public string Name => _data.name;
    public string Description => _data.description;
    public int Price => _data.price;
    public Sprite Icon => _data.icon;
    
    protected BaseShopItem(ShopItemData data)
    {
        _data = data;
        _usesLeft = data.maxUses;
    }
    
    public virtual bool CanAfford(IWallet wallet)
    {
        return wallet.Coins >= Price;
    }
    
    public virtual bool TryPurchase(IWallet wallet)
    {
        if (!CanAfford(wallet) || _usesLeft == 0)
            return false;
            
        if (wallet.TrySpendCoins(Price))
        {
            if (_usesLeft > 0)
                _usesLeft--;
                
            OnPurchase();
            return true;
        }
        
        return false;
    }
    
    protected abstract void OnPurchase();
}

public class WeaponShopItem : BaseShopItem
{
    private DiContainer _container;
    
    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }
    
    public WeaponShopItem(ShopItemData data) : base(data) { }
    
    protected override void OnPurchase()
    {
        if (_data.prefab != null)
        {
            // Spawn weapon at player position or in inventory
            _container.InstantiatePrefab(_data.prefab);
        }
    }
}

public class BuildingShopItem : BaseShopItem
{
    private BuildingPlacer _buildingPlacer;
    
    [Inject]
    private void Construct(BuildingPlacer buildingPlacer)
    {
        _buildingPlacer = buildingPlacer;
    }
    
    public BuildingShopItem(ShopItemData data) : base(data) { }
    
    protected override void OnPurchase()
    {
        if (_data.prefab != null)
        {
            _buildingPlacer.StartPlacing(_data.prefab);
        }
    }
}

public class ConsumableShopItem : BaseShopItem
{
    private DiContainer _container;
    
    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }
    
    public ConsumableShopItem(ShopItemData data) : base(data) { }
    
    protected override void OnPurchase()
    {
        if (_data.prefab != null)
        {
            _container.InstantiatePrefab(_data.prefab);
        }
    }
} 