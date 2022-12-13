using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchResult : MonoBehaviour
{
    public Event Event;
    Image buttonImage;
    [SerializeField] Text resultName;
    [SerializeField] Text resultDate;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.color = OurColors.GetColor(Event.eventColor);
        resultName.text = Event.eventName;
        resultDate.text = Event.startDate.ToString("dd.MM.yy      hh:mm") + "\n" + Event.endDate.ToString("dd.MM.yy      HH:mm");
    }

}
