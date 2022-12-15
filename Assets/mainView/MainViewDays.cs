using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainViewDays : MonoBehaviour
{
    public GameObject dayPrefab;
    public float xBegin = 60.0f;
    public float yBegin = 60.0f;
    public float daySize = 100.0f;
    public float xPadding = 25.0f;
    public float yPadding = 25.0f;

    public System.DateTime dateCurrent;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetupMonth()
    {
        System.DateTime dateNow = System.DateTime.UtcNow.ToLocalTime();

        System.DateTime dateFirst = new System.DateTime(dateCurrent.Year, dateCurrent.Month, 1, 1, 1, 1, 1, dateCurrent.Kind);

        int firstDay = 0;

        switch (dateFirst.DayOfWeek)
        {
            case System.DayOfWeek.Monday: firstDay = 1; break;
            case System.DayOfWeek.Tuesday: firstDay = 2; break;
            case System.DayOfWeek.Wednesday: firstDay = 3; break;
            case System.DayOfWeek.Thursday: firstDay = 4; break;
            case System.DayOfWeek.Friday: firstDay = 5; break;
            case System.DayOfWeek.Saturday: firstDay = 6; break;
            case System.DayOfWeek.Sunday: firstDay = 7; break;
        }

        GameObject day;

        int dayIterator = 0;

        for (int w = 0; w < 6; w++)
        {
            for (int d = 0; d < 7; d++)
            {


                if ((dayIterator > 0 && dayIterator < System.DateTime.DaysInMonth(dateCurrent.Year, dateCurrent.Month)) || w * 7 + d + 1 == firstDay)
                {
                    day = Instantiate(dayPrefab, transform);
                    dayIterator++;
                    day.GetComponentInChildren<Text>().text = dayIterator.ToString();
                    day.GetComponent<MVMonthDayButton>().date = dateCurrent.AddDays(dayIterator-1);
                    day.gameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(xBegin + d * daySize + d * xPadding, -yBegin - w * daySize - w * yPadding);

                    if(dateCurrent.Year == dateNow.Year && dateCurrent.Month == dateNow.Month && dateNow.Day == dayIterator)
                    {
                        day.GetComponent<Image>().color = new Color32(120, 142, 225, 255);
                    }
                }
            }
        }
    }
}
