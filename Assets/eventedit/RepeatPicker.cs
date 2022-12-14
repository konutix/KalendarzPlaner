using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatPicker : MonoBehaviour
{
    public TimeInfiniteScroll amountScroll;
    InfiniteScrollContent amountContent;

    public Dropdown dropdown;

    void Awake()
    {
        amountContent = amountScroll.scrollContent;
    }

    public void SetupFields(EventEditorScript.Repeating repeating)
    {
        dropdown.value = (int)repeating.type;
        amountContent.Setup(repeating.amount);

        OnDropdownChange();
    }

    public void OnDropdownChange()
    {
        if (dropdown.value == 0)
        {
            amountScroll.gameObject.SetActive(false);
        }
        else
        {
            amountScroll.gameObject.SetActive(true);
        }
    }

    public EventEditorScript.Repeating GetValues()
    {
        EventEditorScript.Repeating r = new EventEditorScript.Repeating();
        r.type = (EventEditorScript.RepeatingType)dropdown.value;
        r.amount = amountContent.selectedValue;
        return r;
    }
}
