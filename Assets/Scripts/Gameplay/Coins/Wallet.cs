using System;
using UnityEngine;
using Zenject;

public class Wallet : IWallet
{
    public event Action<int> OnCoinsChanged;
    
    private int _coins;

    public int Coins => _coins;

    public void AddCoins(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");
            
        _coins += amount;
        OnCoinsChanged?.Invoke(_coins);
    }

    public bool TrySpendCoins(int amount)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative");
            
        if (_coins >= amount)
        {
            _coins -= amount;
            OnCoinsChanged?.Invoke(_coins);
            return true;
        }
        
        return false;
    }
} 