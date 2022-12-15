using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SortEnum
{
    StartDateInc,
    StartDateDec,
    EndDateInc,
    EndDateDec,
    NameInc,
    NameDec
}

public class SearchResults : MonoBehaviour
{

    List<Event> events;
    List<Event> searchedEvents;
    List<Event> filteredEvents;
    List<SearchResult> results;
    [SerializeField] GameObject resultPrefab;
    
    SortEnum sortEnum;
    Dictionary<OurColors,bool> checkedColors;
    bool showOlderEvents;

    // Start is called before the first frame update
    void Start()
    {
        sortEnum = SortEnum.StartDateInc;
        searchedEvents = new List<Event>();
        filteredEvents = new List<Event>();
        results = new List<SearchResult>();
        /*
        events = new List<Event>();
        for(int i = 0; i < 10; i++)
        {
            Event e = new Event();
            e.eventName = "Event" + i;
            e.startDate = DateTime.Now;
            e.startDate = e.startDate.AddDays(5-i);
            e.endDate = DateTime.Now;
            e.endDate = e.endDate.AddDays(5 - i);
            e.endDate = e.endDate.AddHours(i);
            e.eventColor = OurColors.green;
            events.Add(e);
        }
        Event tempEvent = events[0];
        tempEvent.eventName = "Urodziny Dziadka Zbyszka";
        tempEvent.eventColor = OurColors.red;
        tempEvent = events[1];
        tempEvent.eventName = "Urodziny Dziadka Marka";
        tempEvent.eventColor = OurColors.green;
        tempEvent = events[2];
        tempEvent.eventName = "Piwo";
        tempEvent.eventColor = OurColors.blue;
        tempEvent = events[3];
        tempEvent.eventName = "Nocne jedzenie";
        tempEvent.eventColor = OurColors.lightblue;
        tempEvent = events[4];
        tempEvent.eventName = "Ananas";
        tempEvent.eventColor = OurColors.yellow;
        tempEvent = events[5];
        tempEvent.eventName = "Wydarzenie specjalne";
        tempEvent.eventColor = OurColors.orange;
        tempEvent = events[6];
        tempEvent.eventName = "Ua Ua";
        tempEvent.eventColor = OurColors.purple;
        tempEvent = events[7];
        tempEvent.eventName = "Coœ tu jest napisane";
        tempEvent.eventColor = OurColors.white;
        tempEvent = events[8];
        tempEvent.eventName = "jfjdjopasfj";
        tempEvent.eventColor = OurColors.pink;
        tempEvent = events[9];
        tempEvent.eventName = "Wydarzenie10";
        tempEvent.eventColor = OurColors.black;
        */
        checkedColors = new Dictionary<OurColors,bool>();
        checkedColors.Add(OurColors.blue, true);
        checkedColors.Add(OurColors.red, true);
        checkedColors.Add(OurColors.green, true);
        checkedColors.Add(OurColors.lightblue, true);
        checkedColors.Add(OurColors.orange, true);
        checkedColors.Add(OurColors.yellow, true);
        checkedColors.Add(OurColors.purple, true);
        checkedColors.Add(OurColors.pink, true);
        checkedColors.Add(OurColors.white, true);
        checkedColors.Add(OurColors.black, true);
        events = SavedEvents.events;
        showOlderEvents = true;
        Search("");
    }

    public void ChangeColorFilter(OurColors color)
    {
        checkedColors[color] = !checkedColors[color];
        Filter();
    }

    public void ChangeShowOlderEvents()
    {
        showOlderEvents = !showOlderEvents;
        Filter();
    }

    public void Search(string search)
    {
        searchedEvents.Clear();
        foreach(Event e in events)
        {
            if (((search.Length > 0 && e.eventName.ToLower().Contains(search.ToLower())) || search.Length == 0))
            {
                searchedEvents.Add(e);
            }
        }
        Filter();
    }

    public void Filter()
    {
        filteredEvents.Clear();
        foreach (Event e in searchedEvents)
        {
            if (checkedColors.ContainsKey(e.eventColor) && checkedColors[e.eventColor])
            {
                if(showOlderEvents || (!showOlderEvents && e.endDate > DateTime.Now))
                {
                    filteredEvents.Add(e);
                }
            }
        }
        SortEvents();
        SpawnEvents();
    }


    void SortEvents()
    {
        IComparer<Event> Comparer;

        switch (sortEnum)
        {
            case SortEnum.StartDateInc:
                Comparer = new IncStartDateComparer();
                break;
            case SortEnum.StartDateDec:
                Comparer = new DecStartDateComparer();
                break;
            case SortEnum.EndDateInc:
                Comparer = new IncEndDateComparer();
                break;
            case SortEnum.EndDateDec:
                Comparer = new DecEndDateComparer();
                break;
            case SortEnum.NameInc:
                Comparer = new IncNameComparer();
                break;
            case SortEnum.NameDec:
                Comparer = new DecNameComparer();
                break;
            default:
                Comparer = new IncStartDateComparer();
                break;
        }
        filteredEvents.Sort(Comparer);
    }

    void SpawnEvents()
    {
        foreach (SearchResult result in results)
        {
            Destroy(result.gameObject);
        }
        results.Clear();
        foreach (Event e in filteredEvents)
        {
            GameObject resultGO = Instantiate(resultPrefab);
            results.Add(resultGO.GetComponent<SearchResult>());
            SearchResult result = results[results.Count - 1];
            result.Event = e;
            RectTransform rect = resultGO.GetComponent<RectTransform>();
            rect.parent = GetComponent<RectTransform>();
            result.Setup();
        }
    }

    public void SortAndSpawn(int enumInt)
    {
        sortEnum = (SortEnum)enumInt;
        SortEvents();
        SpawnEvents();
    }

    class IncStartDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.startDate.CompareTo(y.startDate);
        }
    }

    class DecStartDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return -1 * x.startDate.CompareTo(y.startDate);
        }
    }

    class IncEndDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.endDate.CompareTo(y.endDate);
        }
    }

    class DecEndDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return -1 * x.endDate.CompareTo(y.endDate);
        }
    }

    class IncNameComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.eventName.CompareTo(y.eventName);
        }
    }

    class DecNameComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return -1 * x.eventName.CompareTo(y.eventName);
        }
    }
}
