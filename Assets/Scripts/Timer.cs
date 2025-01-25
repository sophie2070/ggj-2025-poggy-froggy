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
    public Canvas gameOverCanvas;
    public Canvas WinCanvas;
    public Animator bubbleAnimation;
    public Animator kwalAnimation;


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
                bubbleAnimation.enabled = false;
                kwalAnimation.enabled = false;
                StopTimer();
                gameOverCanvas.enabled = true;
            }
        }

        if (timeStopped)
        {
            bubbleAnimation.enabled = false;
            kwalAnimation.enabled = false;
            WinCanvas.enabled = true;
            StopTimer();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
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

