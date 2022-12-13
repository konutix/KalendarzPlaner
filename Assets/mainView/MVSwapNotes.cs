using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVSwapNotes : MonoBehaviour
{
    public GameObject Notes;
    public GameObject Lists;

    void Start()
    {
        Notes.SetActive(true);
        Lists.SetActive(false);
    }

    public void SwapToNotes()
    {
        Notes.SetActive(true);
        Lists.SetActive(false);
    }

    public void SwapToList()
    {
        Notes.SetActive(false);
        Lists.SetActive(true);
    }
}
