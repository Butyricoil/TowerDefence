using UnityEngine;

public interface IPlayer
{
    Vector2 Position { get; }
    float Speed { get; }
    bool IsMounted { get; }
    int Coins { get; }
    
    void Move(float direction);
    void Mount();
    void Dismount();
    void CollectCoin(int amount);
} 