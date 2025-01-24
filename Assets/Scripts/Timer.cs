using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    void Main(string[] args)
    {
    }

    void Update()
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

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

