using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepeatPicker : MonoBehaviour
{
    public InputField amountField;
    public Dropdown dropdown;

    public void SetupFields(EventEditorScript.Repeating repeating)
    {
        dropdown.value = (int)repeating.type;
        amountField.text = "" + repeating.amount;
    }

    public EventEditorScript.Repeating GetValues()
    {
        EventEditorScript.Repeating r = new EventEditorScript.Repeating();
        r.type = (EventEditorScript.RepeatingType)dropdown.value;
        r.amount = int.Parse(amountField.text);
        return r;
    }
}
