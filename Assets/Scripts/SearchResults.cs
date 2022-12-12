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
    List<SearchResult> results;
    [SerializeField] GameObject resultPrefab;
    SortEnum sortEnum;
    
    // Start is called before the first frame update
    void Start()
    {
        sortEnum = SortEnum.StartDateInc;
        events = new List<Event>();
        searchedEvents = new List<Event>();
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
            e.eventColor = Color.green;
            events.Add(e);
        }
        Search("");
    }

    public void Search(string search)
    {
        searchedEvents.Clear();
        foreach(Event e in events)
        {
            if((search.Length > 0 && e.eventName.Contains(search)) || search.Length == 0)
            {
                searchedEvents.Add(e);
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
        searchedEvents.Sort(Comparer);
    }

    void SpawnEvents()
    {
        foreach (SearchResult result in results)
        {
            Destroy(result.gameObject);
        }
        results.Clear();
        foreach (Event e in searchedEvents)
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
