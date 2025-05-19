using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResidentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _residentPrefab;
    [SerializeField] private float _spawnInterval = 5f;
    [SerializeField] private int _maxResidents = 10;
    
    private List<ResidentSpawnPoint> _spawnPoints = new List<ResidentSpawnPoint>();
    private float _spawnTimer;
    private int _currentResidentCount;
    
    [Inject] private DiContainer _container;

    private void Start()
    {
        FindSpawnPoints();
    }

    private void FindSpawnPoints()
    {
        _spawnPoints.Clear();
        _spawnPoints.AddRange(FindObjectsOfType<ResidentSpawnPoint>());
    }

    private void Update()
    {
        if (_currentResidentCount >= _maxResidents)
            return;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0f;
            TrySpawnResident();
        }
    }

    private void TrySpawnResident()
    {
        var availableSpawnPoint = GetRandomAvailableSpawnPoint();
        if (availableSpawnPoint != null)
        {
            SpawnResident(availableSpawnPoint);
        }
    }

    private ResidentSpawnPoint GetRandomAvailableSpawnPoint()
    {
        var availablePoints = _spawnPoints.FindAll(point => !point.IsOccupied);
        if (availablePoints.Count == 0)
            return null;

        return availablePoints[Random.Range(0, availablePoints.Count)];
    }

    private void SpawnResident(ResidentSpawnPoint spawnPoint)
    {
        GameObject residentObject = _container.InstantiatePrefab(
            _residentPrefab, 
            spawnPoint.Position, 
            Quaternion.identity, 
            null
        );

        var resident = residentObject.GetComponent<Resident>();
        if (resident != null)
        {
            resident.Initialize(spawnPoint);
            _currentResidentCount++;
        }
    }

    public void OnResidentDestroyed()
    {
        _currentResidentCount--;
    }
} 