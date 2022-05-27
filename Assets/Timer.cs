using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timeDisplay;
    [SerializeField] int startingTime;
    [SerializeField] MenuScreen menu; 
    int currentTime;
    ResidentTracker residentTracker;
    
    void Start()
    {
        currentTime = startingTime;
        timeDisplay.text = startingTime.ToString();
        residentTracker = FindObjectOfType<ResidentTracker>();
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

        if (currentTime <= 0)
        {
            EndLevel();
            return "0:00";
        }
        else if (seconds >= 10)
        {
            return minutes + ":" + seconds;
        }
        else
        {
            return minutes + ":0" + seconds;
        }
        
    }

    private void EndLevel()
    {
        if (!residentTracker.LevelComplete)
        {
            menu.gameObject.SetActive(true);
            menu.SelectMenu(MenuScreen.Menus.LevelLost);
        }
    }
}
