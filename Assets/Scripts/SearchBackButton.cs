using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBackButton : MonoBehaviour
{
    public ChangeScene sceneChange;
    public GameObject filterObject;
    public void OnClick()
    {
        if (filterObject.activeSelf)
        {
            filterObject.SetActive(false);
        }
        else
        {
            sceneChange.SceneChange();
        }
    }
}
