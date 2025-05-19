using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private string _shopName;
    [SerializeField] private List<ShopItemData> _availableItems;
    
    private IWallet _playerWallet;
    private List<IShopItem> _shopItems = new List<IShopItem>();
    
    public string ShopName => _shopName;
    public IReadOnlyList<IShopItem> AvailableItems => _shopItems;
    
    [Inject]
    private void Construct(IWallet playerWallet)
    {
        _playerWallet = playerWallet;
        InitializeShopItems();
    }
    
    private void InitializeShopItems()
    {
        _shopItems.Clear();
        foreach (var itemData in _availableItems)
        {
            IShopItem item = CreateShopItem(itemData);
            if (item != null)
            {
                _shopItems.Add(item);
            }
        }
    }
    
    private IShopItem CreateShopItem(ShopItemData data)
    {
        switch (data.itemType)
        {
            case ShopItemType.Weapon:
                return new WeaponShopItem(data);
            case ShopItemType.Building:
                return new BuildingShopItem(data);
            case ShopItemType.Consumable:
                return new ConsumableShopItem(data);
            default:
                Debug.LogWarning($"Unknown shop item type: {data.itemType}");
                return null;
        }
    }
    
    public bool TryPurchaseItem(IShopItem item)
    {
        if (item == null || !_shopItems.Contains(item))
            return false;
            
        return item.TryPurchase(_playerWallet);
    }
}

[System.Serializable]
public class ShopItemData
{
    public string name;
    public string description;
    public int price;
    public Sprite icon;
    public ShopItemType itemType;
    public GameObject prefab;
    public float cooldown;
    public int maxUses = -1; // -1 for unlimited
}

public enum ShopItemType
{
    Weapon,
    Building,
    Consumable
} 