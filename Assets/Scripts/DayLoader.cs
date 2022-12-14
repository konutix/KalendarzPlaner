using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DayLoader : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 screenRes;

    private UnityAction<object> onDragEvent;
    private Image image;

    private void Awake()
    {
        onDragEvent = new UnityAction<object>(OnDragEvent);
    }

    private void OnEnable()
    {
        EventManager.StartListening("DragEvent", onDragEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("DragEvent", onDragEvent);
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        screenRes = new Vector2(Screen.width / 2, Screen.height / 2);
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = (Vector2)Input.mousePosition - screenRes;
    }

    private void OnDragEvent(object data)
    {
        image.fillAmount = (float)data;
    }
}
