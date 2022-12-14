using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MVMonthScroll: MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public GameObject addPanel;

    public GameObject MonthButton;

    public MonthContenerScript MonthContainer;
    public RectTransform containerTransform;

    bool dragging = false;

    void Update()
    {
        if (!dragging)
        {
            Vector2 dist = new Vector2(-2160, 410) - containerTransform.anchoredPosition;
            containerTransform.anchoredPosition += dist * 0.05f;
        }
    }

    public void onMonthmove(Vector2 vec)
    {
        Debug.Log(vec.x);

        MonthContainer.dateCurrent = MonthContainer.displayedMonths[2].GetComponent<MainViewDays>().dateCurrent;
        MonthButton.GetComponentInChildren<Text>().text =
                MonthContainer.displayedMonths[2].GetComponent<MainViewDays>().dateCurrent.ToString("MMMM yyyy");

        if (vec.x < 0.375f)
        {
            MonthContainer.dateCurrent = MonthContainer.displayedMonths[1].GetComponent<MainViewDays>().dateCurrent;
            MonthButton.GetComponentInChildren<Text>().text = 
                MonthContainer.displayedMonths[1].GetComponent<MainViewDays>().dateCurrent.ToString("MMMM yyyy");
        }

        if (vec.x > 0.625f)
        {
            MonthContainer.dateCurrent = MonthContainer.displayedMonths[3].GetComponent<MainViewDays>().dateCurrent;
            MonthButton.GetComponentInChildren<Text>().text =
                MonthContainer.displayedMonths[3].GetComponent<MainViewDays>().dateCurrent.ToString("MMMM yyyy");
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        dragging = true;
    }

    public void OnEndDrag(PointerEventData data)
    {
        dragging = false;

        MonthContainer.SetNewMonth();
    }

    public void GoToSearch()
    {
        SceneManager.LoadScene("Search");
    }

    public void GoToAddGroupEvent()
    {
        SceneManager.LoadScene("ST_TeamEventAdd");
    }

    public void GoToAddEvent()
    {
        SceneManager.LoadScene("eventedit");
    }

    public void ShowHideAdd()
    {
        addPanel.SetActive(!addPanel.active);
    }
}
