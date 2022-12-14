using UnityEngine;
using UnityEngine.Events;

public class DayContentManager : MonoBehaviour
{
    private UnityAction<object> onDragBeginEvent;
    private UnityAction<object> onDragEndEvent;

    private bool IsHeld;
    private RectTransform rectTransform;
    private IDayMovable rectTransformEvent;
    private Vector2 screenRes;

    private void Awake()
    {
        onDragBeginEvent = new UnityAction<object>(OnDragBeginEvent);
        onDragEndEvent = new UnityAction<object>(OnDragEndEvent);
    }

    private void OnEnable()
    {
        EventManager.StartListening("BeginDragEvent", onDragBeginEvent);
        EventManager.StartListening("EndDragEvent", onDragEndEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("BeginDragEvent", onDragBeginEvent);
        EventManager.StopListening("EndDragEvent", onDragEndEvent);
    }

    private void OnDragBeginEvent(object data)
    {
        IsHeld = true;
        rectTransformEvent = (IDayMovable)data;
    }

    private void OnDragEndEvent(object data)
    {
        IsHeld = false;
        rectTransformEvent = null;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        screenRes = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (IsHeld)
        {
            Vector2 mousePos = (Vector2)Input.mousePosition - screenRes;
            if (mousePos.y < 730 && mousePos.y > 630)
            {
                rectTransform.anchoredPosition -= new Vector2(0, 1);
                rectTransformEvent.Move(new Vector2(0, 1));
            }
            if (mousePos.y < -90 && mousePos.y > -190)
            {
                rectTransform.anchoredPosition += new Vector2(0, 1);
                rectTransformEvent.Move(new Vector2(0, -1));
            }
        }
    }

}
