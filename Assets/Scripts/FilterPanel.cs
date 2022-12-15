using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterPanel : MonoBehaviour
{

    RectTransform rect;
    [SerializeField] Button button;
    [SerializeField] RectTransform appRect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            if (Input.mousePosition.x < rect.position.x + rect.rect.xMin ||
                Input.mousePosition.x > rect.position.x + rect.rect.xMax ||
                Input.mousePosition.y < rect.position.y + rect.rect.yMin ||
                Input.mousePosition.y > rect.position.y + rect.rect.yMax)
            {
                if(Input.mousePosition.x > appRect.position.x + appRect.rect.xMin &&
                   Input.mousePosition.x < appRect.position.x + appRect.rect.xMax &&
                   Input.mousePosition.y > appRect.position.y + appRect.rect.yMin &&
                   Input.mousePosition.y < appRect.position.y + appRect.rect.yMax)
                {
                    if (Input.GetMouseButton(0))
                    {
                        gameObject.SetActive(false);
                        button.enabled = true;
                    }
                }
            }
        }
    }

    public void ToggleHide()
    {
        gameObject.SetActive(true);
        button.enabled = false;
    }
}
