using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReminderRemoveButton : MonoBehaviour
{
    EventEditorScript eventEditor;
    public System.TimeSpan timespan;

    void Start()
    {
        eventEditor = GameObject.Find("EventManager").GetComponent<EventEditorScript>();
    }

    public void OnRemoveButton()
    {
        eventEditor.HandleRemoveReminder(timespan);
    }
}
