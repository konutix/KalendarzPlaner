using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchResults : MonoBehaviour
{

    List<Event> events;
    List<SearchResult> results;
    [SerializeField] GameObject resultPrefab;
    // Start is called before the first frame update
    void Start()
    {
        events = new List<Event>();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Search(string search)
    {
        foreach (SearchResult result in results)
        {
            Destroy(result.gameObject);
        }
        results.Clear();
        foreach(Event e in events)
        {
            if((search.Length > 0 && e.eventName.Contains(search)) || search.Length == 0)
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
    }
}
