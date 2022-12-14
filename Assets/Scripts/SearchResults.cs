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
        events = new List<Event>();
        searchedEvents = new List<Event>();
        filteredEvents = new List<Event>();
        results = new List<SearchResult>();
        for(int i = 0; i < 10; i++)
        {
            Event e = new Event();
            e.eventName = "Event" + i;
            e.startDate = System.DateTime.Now;
            e.startDate = e.startDate.AddDays(5-i);
            e.endDate = System.DateTime.Now;
            e.endDate = e.endDate.AddDays(5 - i);
            e.endDate = e.endDate.AddHours(i);
            e.eventColor = OurColors.green;
            events.Add(e);
        }

        events[0].eventName = "Urodziny Dziadka Zbyszka";
        events[0].eventColor = OurColors.red;
        events[1].eventName = "Urodziny Dziadka Marka";
        events[1].eventColor = OurColors.green;
        events[2].eventName = "Piwo";
        events[2].eventColor = OurColors.blue;
        events[3].eventName = "Nocne jedzenie";
        events[3].eventColor = OurColors.lightblue;
        events[4].eventName = "Ananas";
        events[4].eventColor = OurColors.yellow;
        events[5].eventName = "Wydarzenie specjalne";
        events[5].eventColor = OurColors.orange;
        events[6].eventName = "Ua Ua";
        events[6].eventColor = OurColors.purple;
        events[7].eventName = "Coœ tu jest napisane";
        events[7].eventColor = OurColors.white;
        events[8].eventName = "jfjdjopasfj";
        events[8].eventColor = OurColors.pink;
        events[9].eventName = "Wydarzenie10";
        events[9].eventColor = OurColors.black;
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
            if (((search.Length > 0 && e.eventName.Contains(search)) || search.Length == 0))
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

    static int CompareBase(Event x, Event y, int equation)
    {
        if (x == null)
        {
            if (y == null)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (y == null)
            {
                return 1;
            }
            else
            {
                return equation;
            }
        }
    }

    class IncStartDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, x.startDate.CompareTo(y.startDate));
        }
    }

    class DecStartDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, -1 * x.startDate.CompareTo(y.startDate));
        }
    }

    class IncEndDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, x.endDate.CompareTo(y.endDate));
        }
    }

    class DecEndDateComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, -1 * x.endDate.CompareTo(y.endDate));
        }
    }

    class IncNameComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, x.eventName.CompareTo(y.eventName));
        }
    }

    class DecNameComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return CompareBase(x, y, -1 * x.eventName.CompareTo(y.eventName));
        }
    }
}
