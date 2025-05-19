using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class MonsterSpawnData
    {
        public GameObject monsterPrefab;
        public float spawnChance = 1f;
        public int minCount = 1;
        public int maxCount = 3;
    }

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnInterval = 10f;
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private Transform _playerTransform;
    
    [Header("Monster Types")]
    [SerializeField] private List<MonsterSpawnData> _dayMonsters;
    [SerializeField] private List<MonsterSpawnData> _nightMonsters;
    [SerializeField] private List<MonsterSpawnData> _dawnMonsters;
    
    private float _spawnTimer;
    private DayNightCycle _dayNightCycle;
    
    [Inject]
    private void Construct(DayNightCycle dayNightCycle)
    {
        _dayNightCycle = dayNightCycle;
        _dayNightCycle.OnTimeOfDayChanged += OnTimeOfDayChanged;
    }

    private void OnDestroy()
    {
        if (_dayNightCycle != null)
        {
            _dayNightCycle.OnTimeOfDayChanged -= OnTimeOfDayChanged;
        }
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0f;
            TrySpawnMonsters();
        }
    }

    private void TrySpawnMonsters()
    {
        List<MonsterSpawnData> currentMonsters = GetCurrentMonsterList();
        if (currentMonsters.Count == 0) return;

        foreach (var monsterData in currentMonsters)
        {
            if (Random.value <= monsterData.spawnChance)
            {
                int count = Random.Range(monsterData.minCount, monsterData.maxCount + 1);
                SpawnMonsters(monsterData.monsterPrefab, count);
            }
        }
    }

    private List<MonsterSpawnData> GetCurrentMonsterList()
    {
        switch (_dayNightCycle.CurrentTimeOfDay)
        {
            case TimeOfDay.Day:
                return _dayMonsters;
            case TimeOfDay.Night:
                return _nightMonsters;
            case TimeOfDay.Dawn:
                return _dawnMonsters;
            default:
                return _dayMonsters;
        }
    }

    private void SpawnMonsters(GameObject monsterPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = _playerTransform.position + new Vector3(randomCircle.x, randomCircle.y, 0);
            
            Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void OnTimeOfDayChanged(TimeOfDay newTimeOfDay)
    {
        // Reset spawn timer when time of day changes
        _spawnTimer = 0f;
    }
} 