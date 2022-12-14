using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVListController : MonoBehaviour
{
    public RectTransform ScrollListContainer;

    public GameObject ListPrefab;

    public List<GameObject> lists;

    float listH = 375.0f;
    float listPad = 15.0f;

    public void AddNewList()
    {
        GameObject obj = Instantiate(ListPrefab, ScrollListContainer.transform);

        obj.GetComponent<MVListObjectController>().controller = this;

        obj.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(0, (-listH / 2 - listPad) + (-listH - listPad) * lists.Count);

        lists.Add(obj);
    }

    public void RemoveList(GameObject obj)
    {
        lists.Remove(obj);
        
        for(int i = 0; i < lists.Count; i++)
        {
            lists[i].GetComponent<RectTransform>().anchoredPosition =
                new Vector2(0, (-listH / 2 - listPad) + (-listH - listPad) * i);
        }
    }
}
