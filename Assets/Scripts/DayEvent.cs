using UnityEngine;
using UnityEngine.EventSystems;

public class DayEvent : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDayMovable
{

    public float timeConsumed = 0;
    public float timeWait = 1.0f;
    public bool isHeld = false;
    public bool isMoved = false;
    public float lastMousePos;
    public float currentMousePos;

    public RectTransform rectTransformEvent;
    private Canvas canvas;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        Debug.Log("A");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        isMoved = false;
        timeConsumed = 0;
        EventManager.TriggerEvent("EndDragEvent", null);
        EventManager.TriggerEvent("DragEvent", 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isHeld)
        {
            if (timeConsumed < timeWait)
            {
                timeConsumed += Time.deltaTime;
                EventManager.TriggerEvent("DragEvent", timeConsumed / timeWait);
            }
            else
            {
                EventManager.TriggerEvent("BeginDragEvent", this);
                isHeld = false;
                isMoved = true;
                lastMousePos = Input.mousePosition.y - Screen.height / 2;
            }

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isMoved)
        {
            Vector2 drag = new Vector2(0, eventData.delta.y / canvas.scaleFactor);
            rectTransformEvent.anchoredPosition += drag;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHeld = false;
        timeConsumed = 0;
        EventManager.TriggerEvent("DragEvent", 0.0f);
    }

    public void Move(Vector2 drag)
    {
        rectTransformEvent.anchoredPosition += drag;
    }
}
