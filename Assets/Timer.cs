using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timeDisplay;
    [SerializeField] int startingTime;
    int currentTime;
    
    void Start()
    {
        currentTime = startingTime;
        timeDisplay.text = startingTime.ToString();
    }

    private void Update()
    {
        currentTime = startingTime - (int)Time.timeSinceLevelLoad;
        timeDisplay.text = ConvertTimeInSeconds();
    }

    private string ConvertTimeInSeconds()
    {
        int minutes = 0;
        int seconds = currentTime;
        for(int i = seconds; i >= 60; i -= 60)
        {
            minutes++;
            seconds -= 60;
        }
        if (seconds >= 10)
        {
            return minutes + ":" + seconds;
        }
        else
        {
            return minutes + ":0" + seconds;
        }
    }
}
