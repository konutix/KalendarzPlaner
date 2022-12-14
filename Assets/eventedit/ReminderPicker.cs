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
        // dropdown.value = (int)repeating.type;
        // amountContent.Setup(repeating.amount);
    }

    public System.TimeSpan GetValues()
    {
        System.TimeSpan ts;
        return ts;
    }
}
