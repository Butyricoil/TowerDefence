using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Kingdom/Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float mountedSpeed = 8f;
    public float jumpForce = 10f;
    
    [Header("Mount")]
    public float mountCooldown = 2f;
    public float mountRange = 2f;
    
    [Header("Resources")]
    public int startingCoins = 10;
    public int villagerCost = 5;
} 