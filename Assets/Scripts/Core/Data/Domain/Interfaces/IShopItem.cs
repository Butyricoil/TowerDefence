using UnityEngine;

public interface IShopItem
{
    string Name { get; }
    string Description { get; }
    int Price { get; }
    Sprite Icon { get; }
    bool CanAfford(IWallet wallet);
    bool TryPurchase(IWallet wallet);
} 