using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReminderPicker : MonoBehaviour
{
    public TimeInfiniteScroll amountScroll;
    InfiniteScrollContent amountContent;

    public Dropdown dropdown;

    void Awake()
    {
        amountContent = amountScroll.scrollContent;
    }

    public void SetupFields()
    {
        amountContent.values = new string[99];
        for (int i = 0; i < 99; i++)
        {
            if (i < 9) amountContent.values[i] = "0" + (i+1).ToString();
            else amountContent.values[i] = (i+1).ToString();
        }

        amountContent.Setup(10);
    }

    public System.TimeSpan GetValues()
    {
        switch (dropdown.value)
        {
            case 0: return System.TimeSpan.FromMinutes(amountContent.selectedValue);
            case 1: return System.TimeSpan.FromHours(amountContent.selectedValue);
            case 2: return System.TimeSpan.FromDays(amountContent.selectedValue);
        }

        return new System.TimeSpan();
    }
}
