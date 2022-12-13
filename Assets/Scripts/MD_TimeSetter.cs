using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MD_TimeSetter : MonoBehaviour
{
    public int hour;
    public int min;

    [SerializeField] public GameObject timeSetterWindow;

    public int target_id;
    [SerializeField] public TextMeshProUGUI field_0;
    [SerializeField] public TextMeshProUGUI field_1;
    [SerializeField] public TextMeshProUGUI field_2;
    /// ADD HERE

    private int new_hour;
    private int new_min;

    public void SetTargetID(int id)
    {
        target_id = id;
        Show();
    }

    public void Show()
    {
        new_hour = -1;
        new_min = -1;
        timeSetterWindow.SetActive(true);
    }

    public void Hide()
    {
        timeSetterWindow.SetActive(false);
    }
    
    public void SetH(int h)
    {
        new_hour = h;
        check();
    }

    public void SetM(int m)
    {
        new_min = m;
        check();
    }

    public void check()
    {
        if(new_hour != -1 && new_min != -1)
        {
            hour = new_hour;
            min = new_min;

            string txt_h;
            if(hour < 10)
                txt_h = "0" + hour.ToString();
            else
                txt_h = hour.ToString();

            string txt_m;
            if(min < 10)
                txt_m = "0" + min.ToString();
            else
                txt_m = min.ToString();
            
            string res = txt_h + ":" + txt_m;

            switch(target_id)
            {
                case 0: field_0.GetComponent<TextMeshProUGUI>().text = res; break;
                case 1: field_1.GetComponent<TextMeshProUGUI>().text = res; break;
                case 2: field_2.GetComponent<TextMeshProUGUI>().text = res; break;
                /// ADD HERE
            }
            
            Hide();
        }
    }
}
