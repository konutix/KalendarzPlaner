using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class DayGetHandlesTime : MonoBehaviour
{
    public RectTransform number0;
    public RectTransform number1;
    public int HoursDistance;
    public RectTransform upHandle;
    public RectTransform downHandle;

    private UnityAction<object> onDragEndEvent;

    public TextMeshProUGUI textTime;

    private void Awake()
    {
        onDragEndEvent = new UnityAction<object>(OnDragEndEvent);
    }

    private void OnEnable()
    {
        EventManager.StartListening("EndDragEvent", onDragEndEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EndDragEvent", onDragEndEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
        HoursDistance = (int)(number0.position.y - number1.position.y);
        OnDragEndEvent(null);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDragEndEvent(object data)
    {
        string newText = GetTimeHandle(upHandle) + " - " + GetTimeHandle(downHandle);
        textTime.text = newText;
    }

    public string GetTimeHandle(RectTransform handle)
    {
        int t;

        t = (int)((number0.position.y - handle.position.y) / HoursDistance);
        string result = t.ToString("00") + ":";
        t = (int)((number0.position.y - t * HoursDistance - handle.position.y)/2);
        result += t.ToString("00");

        return result;
    }
}
