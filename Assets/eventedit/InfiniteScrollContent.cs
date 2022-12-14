using UnityEngine;
using UnityEngine.UI;

public class InfiniteScrollContent : MonoBehaviour
{
    public GameObject scrollItemPrefab;
    public string[] values;
    
    [HideInInspector] public ScrollRect scrollRect;

    float itemHeight = 100.0f;

    float initialPos;
    float basePos;
    RectTransform rt;

    public int selectedValue = 0;


    public void Setup(int initialValue)
    {
        rt = GetComponent<RectTransform>();
        int magic = (values.Length%2==0)?0:1;
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, (0.5f + values.Length/2 + magic) * itemHeight);
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, itemHeight * (values.Length+2));
        basePos = rt.anchoredPosition.y;
        initialPos = basePos;

        foreach (Transform child in transform) 
        {
            Destroy(child.gameObject);
        }

        int selected = initialValue;
        selectedValue = selected;
        
        int offset = values.Length + selected - values.Length / 2;
        if (values.Length % 2 == 1) offset -= 1;

        for (int i = 0; i < values.Length; i++)
        {
            var newGO = Instantiate(scrollItemPrefab, transform);
            newGO.transform.localPosition = new Vector2(0.0f, -(i + 1) * itemHeight);
            newGO.GetComponentInChildren<Text>().text = values[(i+offset)%values.Length];
        }
    }

    void Update()
    {
        float posDiff = rt.anchoredPosition.y - basePos;
        int que = 0;
        while (posDiff > itemHeight)
        {
            que += 1;
            posDiff -= itemHeight;
            basePos = rt.anchoredPosition.y;
            
            var firstChild = transform.GetChild(0);
            var lastChild = transform.GetChild(values.Length-1);
            var lastChildPos = lastChild.localPosition;

            lastChildPos += new Vector3(0.0f, -itemHeight, 0.0f);
            firstChild.localPosition = lastChildPos;
            firstChild.SetAsLastSibling();
        }
        while (posDiff < -itemHeight)
        {
            que -= 1;
            posDiff += itemHeight;
            basePos = rt.anchoredPosition.y;
            
            var firstChild = transform.GetChild(0);
            var lastChild = transform.GetChild(values.Length-1);
            var firstChildPos = firstChild.localPosition;

            firstChildPos += new Vector3(0.0f, itemHeight, 0.0f);
            lastChild.localPosition = firstChildPos;
            lastChild.SetAsFirstSibling();
        }

        int selectedIndex = values.Length/2;
        if (values.Length%2==0) selectedIndex -= 1;
        for (int i = 0; i < values.Length; i++)
        {
            var text = transform.GetChild(i).GetComponentInChildren<Text>();
            if (text)
            {
                if (selectedIndex == i)
                {
                    selectedValue = (int.Parse(text.text));
                    text.color = Color.cyan;
                }
                else
                {
                    text.color = Color.black;
                }
            }
        }
    }

    public void ResetPosition()
    {
        float posDiff = initialPos - rt.anchoredPosition.y;
        if (Mathf.Abs(posDiff) < itemHeight) return;

        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + posDiff);
        basePos = rt.anchoredPosition.y;
        for (int i = 0; i < values.Length; i++)
        {
            var child = transform.GetChild(i);
            child.localPosition = new Vector2(0.0f, -(i + 1) * itemHeight);
        }
    }

}