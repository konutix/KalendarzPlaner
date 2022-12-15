using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToWhite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.GetComponent<Image>().color == Color.black)
        {
            GetComponent<Text>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
