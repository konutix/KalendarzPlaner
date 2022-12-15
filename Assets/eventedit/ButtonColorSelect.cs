using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorSelect : MonoBehaviour
{
    EventEditorScript eventEditorScript;
    RawImage rawImage;

    void Start()
    {
        rawImage = GetComponentInChildren<RawImage>();
        eventEditorScript = GameObject.Find("EventManager").GetComponent<EventEditorScript>();
    }

    public void OnClick()
    {
        eventEditorScript.HandleColorSelectButton(GetComponent<ColorToggle>().GetOurColor());
    }
}
