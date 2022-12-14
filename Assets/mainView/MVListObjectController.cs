using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVListObjectController : MonoBehaviour
{
    public MVListController controller;

    public GameObject ListPointPrefab;

    public List<GameObject> Points;

    float listH = 60.0f;
    float listPad = 15.0f;

    public void RemoveList()
    {
        controller.RemoveList(gameObject);
        Destroy(gameObject);
    }

    public void AddPoint()
    {
        GameObject obj = Instantiate(ListPointPrefab, transform);

        obj.GetComponent<MVListPontController>().controller = this;

        obj.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(-40, -190.0f + (-listH - listPad) * Points.Count);

        Points.Add(obj);
    }

    public void RemovePoint(GameObject obj)
    {
        Points.Remove(obj);

        for (int i = 0; i < Points.Count; i++)
        {
            Points[i].GetComponent<RectTransform>().anchoredPosition =
                new Vector2(-40, -190.0f + (-listH - listPad) * i);
        }
    }
}
