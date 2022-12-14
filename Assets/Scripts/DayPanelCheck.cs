using UnityEngine;
using UnityEngine.EventSystems;

public class DayPanelCheck : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public bool isMouseOver = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}
