using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int _startingCoins = 10;
    [SerializeField] private int _startingSubjects = 3;
    [SerializeField] private GameObject _subjectPrefab;
    [SerializeField] private Transform _spawnPoint;
    
    [Header("References")]
    [SerializeField] private DayNightCycle _dayNightCycle;
    [SerializeField] private MonsterSpawner _monsterSpawner;
    
    private IWallet _wallet;
    private List<Subject> _subjects = new List<Subject>();
    private bool _isGameOver;
    
    [Inject]
    private void Construct(IWallet wallet)
    {
        _wallet = wallet;
        _wallet.AddCoins(_startingCoins);
    }
    
    private void Start()
    {
        InitializeGame();
    }
    
    private void InitializeGame()
    {
        SpawnInitialSubjects();
        _dayNightCycle.OnTimeOfDayChanged += OnTimeOfDayChanged;
    }
    
    private void SpawnInitialSubjects()
    {
        for (int i = 0; i < _startingSubjects; i++)
        {
            SpawnSubject();
        }
    }
    
    public void SpawnSubject()
    {
        if (_subjectPrefab != null && _spawnPoint != null)
        {
            GameObject subjectObject = Instantiate(_subjectPrefab, _spawnPoint.position, Quaternion.identity);
            var subject = subjectObject.GetComponent<Subject>();
            if (subject != null)
            {
                _subjects.Add(subject);
            }
        }
    }
    
    private void OnTimeOfDayChanged(TimeOfDay newTimeOfDay)
    {
        switch (newTimeOfDay)
        {
            case TimeOfDay.Dawn:
                OnDawn();
                break;
            case TimeOfDay.Day:
                OnDay();
                break;
            case TimeOfDay.Night:
                OnNight();
                break;
        }
    }
    
    private void OnDawn()
    {
        // Reset daily activities
        foreach (var subject in _subjects)
        {
            // Reset subject states
        }
    }
    
    private void OnDay()
    {
        // Enable normal activities
        _monsterSpawner.enabled = false;
    }
    
    private void OnNight()
    {
        // Prepare for night attacks
        _monsterSpawner.enabled = true;
    }
    
    public void OnSubjectDied(Subject subject)
    {
        _subjects.Remove(subject);
        
        if (_subjects.Count == 0)
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        if (_isGameOver)
            return;
            
        _isGameOver = true;
        Debug.Log("Game Over!");
        // Show game over UI, etc.
    }
    
    private void OnDestroy()
    {
        if (_dayNightCycle != null)
        {
            _dayNightCycle.OnTimeOfDayChanged -= OnTimeOfDayChanged;
        }
    }
} 