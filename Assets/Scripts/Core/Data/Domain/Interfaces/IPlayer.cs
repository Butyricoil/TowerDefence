using UnityEngine;

public interface IPlayer
{
    Vector2 Position { get; }
    IWallet Wallet { get; }

    void Move(Vector2 direction);
    void DropCoin();
    void Jump();
    void Sprint(bool isSprinting);
}