using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrayDay : MonoBehaviour
{
    public GameObject months;

    public void GrayDays()
    {
        System.Random rnd = new System.Random();

        GameObject month = months.transform.GetChild(2).gameObject;

        for(int i=0; i<29; i++)
        {
            if(rnd.Next() % 100 < 25)
            {
                month.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    }
}
