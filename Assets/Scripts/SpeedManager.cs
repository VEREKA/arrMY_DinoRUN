using System;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static SpeedManager Instance { get; private set; }

    [Header("Speed Settings")]
    public float minSpeed = 5f;
    public float maxSpeed = 18f;
    public float speedMultiplier = 0.12f;

    public float CurrentSpeed { get; private set; }

    public event Action<float> OnSpeedChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        CurrentSpeed = minSpeed;
    }

    private void Update()
    {
        if (CurrentSpeed < maxSpeed)
        {
            CurrentSpeed += speedMultiplier * Time.deltaTime;
            if (CurrentSpeed > maxSpeed) CurrentSpeed = maxSpeed;
            OnSpeedChanged?.Invoke(CurrentSpeed);
        }
    }

    public void ResetSpeed()
    {
        CurrentSpeed = minSpeed;
        OnSpeedChanged?.Invoke(CurrentSpeed);
    }

    public void SetSpeed(float speed)
    {
        CurrentSpeed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        OnSpeedChanged?.Invoke(CurrentSpeed);
    }
}
