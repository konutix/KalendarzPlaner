using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVListPontController : MonoBehaviour
{
    public MVListObjectController controller;

    public void RemovePoint()
    {
        controller.RemovePoint(gameObject);
        Destroy(gameObject);
    }
}
