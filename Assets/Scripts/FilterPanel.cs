using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterPanel : MonoBehaviour
{

    RectTransform rect;
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
                if(Input.GetMouseButton(0))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void ToggleHide()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
