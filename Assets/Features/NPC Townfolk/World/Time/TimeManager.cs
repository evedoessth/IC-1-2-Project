using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private VillagerTime villagerTime;
    [SerializeField] private float timeMultiplier;

    [SerializeField] private float startHour;

    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Light variables")]
    [SerializeField] private Light sunLight;

    [SerializeField] private float sunriseHour;
    
    [SerializeField] private float sunsetHour;
    
    [SerializeField] private Color dayAmbientLight;

    [SerializeField] private Color nightAmbientLight; 

    [SerializeField] private AnimationCurve lightChangeCurve;

    [SerializeField] private float maxSunLightIntensity;

    [SerializeField] private Light moonLight;

    [SerializeField] private float maxMoonLightIntensity;

    
    private DateTime currentTime;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    private bool isStartingHour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        isStartingHour = true;

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        var previousTime = currentTime.Hour;
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        var nextTime = currentTime.Hour;

        if (nextTime > previousTime || isStartingHour)
        {
            villagerTime.currentHour = currentTime.Hour;
            isStartingHour = false;
            villagerTime.OnHourChanged.Invoke();
        }
        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

        private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);

        }
        else
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }



    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }
}
