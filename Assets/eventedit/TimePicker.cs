using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour
{
    public Text titleText;
    public InputField day;
    public InputField month;
    public InputField year;
    public InputField hour;
    public InputField minute;

    public void SetupFields(string title, System.DateTime dateTime)
    {
        titleText.text = title;
        day.text = dateTime.Day.ToString();
        month.text = dateTime.Month.ToString();
        year.text = dateTime.Year.ToString();
        hour.text = dateTime.Hour.ToString();
        minute.text = dateTime.Minute.ToString();
    }

    public System.DateTime GetValues()
    {
        int d = int.Parse(day.text);
        int m = int.Parse(month.text);
        int y = int.Parse(year.text);
        int h = int.Parse(hour.text);
        int min = int.Parse(minute.text);

        return new System.DateTime(y, m, d, h, min, 0);
    }
}
