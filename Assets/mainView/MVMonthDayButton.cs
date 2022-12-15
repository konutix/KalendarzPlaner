using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MVMonthDayButton : MonoBehaviour
{
    public DateTime date;
    public void ShowDay()
    {
        if(SceneManager.GetActiveScene().name == "MainView")
        {
            SavedEvents.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("DayView");
        }

        if (SceneManager.GetActiveScene().name == "ST_TeamEventAdd")
        {
            Event currentEvent = new Event();
            currentEvent.startDate = date;
            print(currentEvent.startDate.ToString("dd.MM.yy"));
            TimeSpan oneHour = TimeSpan.FromHours(1);
            currentEvent.endDate = currentEvent.startDate.Add(oneHour);

            Repeating repeat;
            repeat.amount = 1;
            repeat.type = RepeatingType.None;

            currentEvent.repeating = repeat;
            TimeSpan tenMinutes = TimeSpan.FromMinutes(10);
            currentEvent.reminders = new List<TimeSpan>();
            currentEvent.reminders.Add(tenMinutes);
            currentEvent.eventColor = OurColors.blue;
            SavedEvents.events.Add(currentEvent);
            SavedEvents.currentlyEditedEvent = SavedEvents.events.Count-1;
            SavedEvents.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("DayView2");
        }
    }
}
