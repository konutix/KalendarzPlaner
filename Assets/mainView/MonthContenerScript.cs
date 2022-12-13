using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonthContenerScript : MonoBehaviour
{
    public GameObject MonthButton;

    public GameObject monthPrefab;

    public GameObject[] displayedMonths;

    public System.DateTime dateCurrent;

    // Start is called before the first frame update
    void Start()
    {
        displayedMonths = new GameObject[5];

        dateCurrent = System.DateTime.UtcNow.ToLocalTime();
        dateCurrent = new System.DateTime(dateCurrent.Year, dateCurrent.Month, 1, 1, 1, 1, 1, dateCurrent.Kind);

        MonthButton.GetComponentInChildren<Text>().text = dateCurrent.ToString("MMMM yyyy");

        for (int i = 0; i < 5; i++)
        {
            displayedMonths[i] = Instantiate(monthPrefab, transform);

            displayedMonths[i].GetComponent<MainViewDays>().dateCurrent = dateCurrent.AddMonths(i - 2);

            displayedMonths[i].gameObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(540 + i * 1080, 0);

            displayedMonths[i].GetComponent<MainViewDays>().SetupMonth();
        }

    }

    public void ResetMonths()
    {
        for (int i = 0; i < 5; i++)
        {
            Object.Destroy(displayedMonths[i]);
        }

        dateCurrent = System.DateTime.UtcNow.ToLocalTime();
        dateCurrent = new System.DateTime(dateCurrent.Year, dateCurrent.Month, 1, 1, 1, 1, 1, dateCurrent.Kind);

        MonthButton.GetComponentInChildren<Text>().text = dateCurrent.ToString("MMMM yyyy");

        for (int i = 0; i < 5; i++)
        {
            displayedMonths[i] = Instantiate(monthPrefab, transform);

            displayedMonths[i].GetComponent<MainViewDays>().dateCurrent = dateCurrent.AddMonths(i - 2);

            displayedMonths[i].gameObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(540 + i * 1080, 0);

            displayedMonths[i].GetComponent<MainViewDays>().SetupMonth();
        }

        GetComponent<RectTransform>().anchoredPosition = new Vector2(-2160, 410);
    }

    public void SetNewMonth()
    {
        for (int i = 0; i < 5; i++)
        {
            Object.Destroy(displayedMonths[i]);
        }

        MonthButton.GetComponentInChildren<Text>().text = dateCurrent.ToString("MMMM yyyy");

        for (int i = 0; i < 5; i++)
        {
            displayedMonths[i] = Instantiate(monthPrefab, transform);

            displayedMonths[i].GetComponent<MainViewDays>().dateCurrent = dateCurrent.AddMonths(i - 2);

            displayedMonths[i].gameObject.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(540 + i * 1080, 0);

            displayedMonths[i].GetComponent<MainViewDays>().SetupMonth();
        }

        GetComponent<RectTransform>().anchoredPosition = new Vector2(-2160, 410);
    }
}
