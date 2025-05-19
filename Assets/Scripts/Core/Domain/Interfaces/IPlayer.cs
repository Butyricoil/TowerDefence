using UnityEngine;

public interface IPlayer
{
    Vector2 Position { get; }
    float Speed { get; }
    int Coins { get; }
    bool IsMounted { get; set; }

    void Move(Vector2 direction);
    void CollectCoin(int amount);
    void DropCoin();
    void Jump();
    void Sprint(bool isSprinting);
    void Dismount();
    void Mount();
} 