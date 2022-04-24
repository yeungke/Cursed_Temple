using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerLogic : MonoBehaviour
{
    [SerializeField] private Text timeText;

    // Count down the game timer
    private void CountDown()
    {
        DisplayTime(GameManager.instance.timeValue);
    }

    // Displays an extra second on the clock; separates the timer to minutes and seconds
    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }
}
