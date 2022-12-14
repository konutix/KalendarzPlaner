using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTimePicker : MonoBehaviour
{
    public TimeInfiniteScroll dayScroller;
    public TimeInfiniteScroll monthScroller;
    public TimeInfiniteScroll yearScroller;
    public TimeInfiniteScroll hourScroller;
    public TimeInfiniteScroll minuteScroller;

    InfiniteScrollContent dayContent;
    InfiniteScrollContent monthContent;
    InfiniteScrollContent yearContent;
    InfiniteScrollContent hourContent;
    InfiniteScrollContent minuteContent;


    int selectedDay;
    int selectedMonth;
    int selectedYear;
    int selectedHour;
    int selectedMinute;

    int baseYear = 2018;
    int maxYears = 12;

    void Awake()
    {
        dayContent = dayScroller.scrollContent;
        monthContent = monthScroller.scrollContent;
        yearContent = yearScroller.scrollContent;
        hourContent = hourScroller.scrollContent;
        minuteContent = minuteScroller.scrollContent;

        yearContent.values = new string[maxYears];
        for (int i = 0; i < maxYears; i++)
        {
            yearContent.values[i] = (baseYear + i).ToString();
        }

        hourContent.values = new string[24];
        for (int i = 0; i < 24; i++)
        {
            if (i <= 9) hourContent.values[i] = "0" + (i).ToString();
            else hourContent.values[i] = (i).ToString();
        }

        minuteContent.values = new string[60];
        for (int i = 0; i < 60; i++)
        {
            if (i <= 9) minuteContent.values[i] = "0" + (i).ToString();
            else minuteContent.values[i] = (i).ToString();
        }
    }

    public void CheckDaysInMonth()
    {
        int numDays = System.DateTime.DaysInMonth(yearContent.selectedValue, monthContent.selectedValue);
        if (numDays != dayContent.values.Length)
        {
            dayContent.values = new string[numDays];
            for (int i = 0; i < numDays; i++)
            {
                if (i < 9)
                {
                    dayContent.values[i] = "0" + (i+1).ToString();
                }
                else
                {
                    dayContent.values[i] = (i+1).ToString();
                }
            }

            if (selectedDay > numDays) selectedDay = numDays;
            dayContent.Setup(selectedDay);
        }
    }

    public void SetupContent(System.DateTime dateTime)
    {
        selectedDay = dateTime.Day;
        selectedMonth = dateTime.Month;
        selectedYear = dateTime.Year;
        selectedHour = dateTime.Hour;
        selectedMinute = dateTime.Minute;

        dayContent.Setup(selectedDay);
        monthContent.Setup(selectedMonth);
        yearContent.Setup(selectedYear-baseYear+1);
        hourContent.Setup(selectedHour+1);
        minuteContent.Setup(selectedMinute+1);
    }

    public System.DateTime GetValues()
    {
        return new System.DateTime(selectedYear, selectedMonth, selectedDay, selectedHour, selectedMinute, 0);
    }

    void Update()
    {
        selectedDay = dayContent.selectedValue;
        selectedMonth = monthContent.selectedValue;
        selectedYear = yearContent.selectedValue;
        selectedHour = hourContent.selectedValue;
        selectedMinute = minuteContent.selectedValue;
        CheckDaysInMonth();
    }
}
