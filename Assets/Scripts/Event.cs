using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RepeatingType
{
    None, Daily, Weekly, Monthly, Yearly
}
[Serializable]
public struct Repeating
{
    public RepeatingType type;
    public int amount;
}
[Serializable]
public struct Event
{

    public string eventName;
    public DateTime startDate;
    public DateTime endDate;
    public OurColors eventColor;
    public bool isAllDay;
    public Repeating repeating;
    public List<TimeSpan> reminders;
    public string notes;

}

public static class SavedEvents
{
    public static List<Event> events = new List<Event>();
    public static int currentlyEditedEvent = -1;
    public static string lastScene = "";
}
