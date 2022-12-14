using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DayChangeEventLenght : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDayMovable
{

    private RectTransform rectTransform;
    private Canvas canvas;

    public bool isUp = false;

    public RectTransform Event;
    public RectTransform EventCollider;
    public RectTransform secondHandle;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 drag = new Vector2(0, eventData.delta.y / canvas.scaleFactor);
        Move(drag);
    }

    public void Move(Vector2 drag)
    {
        rectTransform.anchoredPosition += drag / 2;
        secondHandle.anchoredPosition -= drag / 2;

        if (isUp)
        {
            Event.sizeDelta += drag;
            EventCollider.sizeDelta += drag;
            Event.anchoredPosition += drag / 2;
        }
        else
        {
            Event.sizeDelta -= drag;
            EventCollider.sizeDelta -= drag;
            Event.anchoredPosition += drag / 2;

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        EventManager.TriggerEvent("BeginDragEvent", this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventManager.TriggerEvent("EndDragEvent", null);
    }
}
