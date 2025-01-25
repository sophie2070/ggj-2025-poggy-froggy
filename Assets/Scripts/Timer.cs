using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Timer: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float startTime = 150;
    [SerializeField] float remainingTime;
    private float stopTime;
    private float timerTime;
    public bool timeStopped = false;


    private void Start()
    {
        remainingTime = startTime;
    }

    void Update()
    {
        if (!timeStopped)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                Debug.Log("Game Over");
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);

        if(Input.GetMouseButtonDown(0))
        {
            timeStopped = true;
            StopTimer();
        }

        if(Input.GetMouseButtonDown (1))
        {
            ResetTimer();
        }
    }

    void StopTimer()
    {
        if (timeStopped)
        {
            stopTime = remainingTime;
        }
    }

    void ResetTimer()
    {
        remainingTime = startTime;
    }
}

