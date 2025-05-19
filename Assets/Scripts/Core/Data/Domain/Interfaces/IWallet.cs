using System;

public interface IWallet
{
    event Action<int> OnCoinsChanged;
    int Coins { get; }
    void AddCoins(int amount);
    bool TrySpendCoins(int amount);
} 