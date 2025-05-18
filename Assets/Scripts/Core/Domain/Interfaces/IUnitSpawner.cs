using UnityEngine;

public interface IUnitSpawner
{
    void SpawnUnit(Vector2 position, UnitType unitType);
    void SpawnVillager(Vector2 position);
    bool CanSpawnUnit(UnitType unitType);
}

public enum UnitType
{
    Villager,
    Archer,
    Builder
} 