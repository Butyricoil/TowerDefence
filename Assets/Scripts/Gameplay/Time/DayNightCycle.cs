using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;
using Zenject;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] private float _dayDuration = 300f; // 5 minutes per day
    [SerializeField] private float _currentTime = 0f;
    
    [Header("Light Settings")]
    [SerializeField] private Color _dayLightColor = Color.white;
    [SerializeField] private Color _nightLightColor = new Color(0.1f, 0.1f, 0.3f);
    [SerializeField] private float _minLightIntensity = 0.1f;
    [SerializeField] private float _maxLightIntensity = 1f;
    
    [Header("References")]
    [SerializeField] private Light2D _globalLight;
    
    public event Action<TimeOfDay> OnTimeOfDayChanged;
    public event Action<float> OnTimeChanged;
    
    public TimeOfDay CurrentTimeOfDay { get; private set; }
    public float DayProgress => _currentTime / _dayDuration;
    
    private void Update()
    {
        UpdateTime();
        UpdateLighting();
    }
    
    private void UpdateTime()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _dayDuration)
        {
            _currentTime = 0f;
        }
        
        OnTimeChanged?.Invoke(DayProgress);
        
        TimeOfDay newTimeOfDay = GetTimeOfDay();
        if (newTimeOfDay != CurrentTimeOfDay)
        {
            CurrentTimeOfDay = newTimeOfDay;
            OnTimeOfDayChanged?.Invoke(CurrentTimeOfDay);
        }
    }
    
    private void UpdateLighting()
    {
        float dayProgress = DayProgress;
        float lightIntensity;
        
        // Calculate light intensity based on time of day
        if (dayProgress < 0.25f) // Dawn
        {
            lightIntensity = Mathf.Lerp(_minLightIntensity, _maxLightIntensity, dayProgress * 4f);
            _globalLight.color = Color.Lerp(_nightLightColor, _dayLightColor, dayProgress * 4f);
        }
        else if (dayProgress < 0.75f) // Day
        {
            lightIntensity = _maxLightIntensity;
            _globalLight.color = _dayLightColor;
        }
        else // Dusk
        {
            lightIntensity = Mathf.Lerp(_maxLightIntensity, _minLightIntensity, (dayProgress - 0.75f) * 4f);
            _globalLight.color = Color.Lerp(_dayLightColor, _nightLightColor, (dayProgress - 0.75f) * 4f);
        }
        
        _globalLight.intensity = lightIntensity;
    }
    
    private TimeOfDay GetTimeOfDay()
    {
        float progress = DayProgress;
        
        if (progress < 0.25f)
            return TimeOfDay.Dawn;
        if (progress < 0.75f)
            return TimeOfDay.Day;
        return TimeOfDay.Night;
    }
}

public enum TimeOfDay
{
    Dawn,
    Day,
    Night
} 