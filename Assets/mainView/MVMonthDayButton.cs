using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MVMonthDayButton : MonoBehaviour
{
    public void ShowDay()
    {
        if(SceneManager.GetActiveScene().name == "MainView")
        {
            SceneManager.LoadScene("DayView");
        }

        if (SceneManager.GetActiveScene().name == "ST_TeamEventAdd")
        {
            SceneManager.LoadScene("DayView2");
        }
    }
}
