using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DayChangeLenght : MonoBehaviour, IDragHandler
{

    private RectTransform rectTransform;
    private Canvas canvas;

    public RectTransform ScrollView;
    public RectTransform Notepad;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 drag = new Vector2(0, eventData.delta.y / canvas.scaleFactor);
        rectTransform.anchoredPosition += drag;
        ScrollView.sizeDelta -= drag;
        ScrollView.anchoredPosition += drag/2;
        Notepad.sizeDelta += drag;
        Notepad.anchoredPosition += drag / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
