using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MD_TeammatesList : MonoBehaviour
{
    [SerializeField] public GameObject root;
    [SerializeField] public GameObject personTemplate;
    [SerializeField] public GameObject friends;
    [SerializeField] public GameObject months;

    int bid = 0;
    private List<GameObject> created = new List<GameObject>();
    int ilu_ludzi = 0;

    public void ShowFriens()
    {
        friends.SetActive(true);
    }

    public void AddPerson(string name)
    {
        GameObject go = Instantiate(personTemplate);
        go.GetComponent<Transform>().SetParent(root.GetComponent<Transform>(), false);
        go.SetActive(true);
        int tmp_bid = bid;
        go.transform.GetChild(1).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => DeletePerson(tmp_bid));
        go.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        bid++;
        created.Add(go);
        friends.SetActive(false);
        ilu_ludzi++;
    }

    public void DeletePerson(int id)
    {
        Destroy(created[id]);
    }

    public void UnGrayDays()
    {
        ilu_ludzi--;
        GameObject month = months.transform.GetChild(2).gameObject;

        if(ilu_ludzi == 0)
        {
            for(int i=0; i<29; i++)
            {
                month.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }
        else
        {
            System.Random rnd = new System.Random();

            for(int i=0; i<29; i++)
            {
                if(rnd.Next() % 100 < 25)
                {
                    month.transform.GetChild(i).gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
                }
            }
        }
    }
}
