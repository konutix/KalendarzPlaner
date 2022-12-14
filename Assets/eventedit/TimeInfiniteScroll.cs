using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TimeInfiniteScroll : MonoBehaviour, IEndDragHandler//, IBeginDragHandler, IDragHandler, IScrollHandler
{
    public InfiniteScrollContent scrollContent;
    float maxSpeed = 700.0f;

    ScrollRect scrollRect;

    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollContent.scrollRect = GetComponent<ScrollRect>();
    }

    bool asd = false;
    void Update()
    {
        var vel = scrollRect.velocity;
        vel.y = Mathf.Clamp(vel.y, -maxSpeed, maxSpeed);
        scrollRect.velocity = vel;

        if (Mathf.Abs(vel.y) < 5.141592f)
        {
            if (asd)
            {
                scrollRect.StopMovement();
                asd = false;
                scrollContent.ResetPosition();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        asd = true;
        scrollContent.ResetPosition();
        // scrollRect.StopMovement();
    }
}
