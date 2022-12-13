using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MD_TeammatesList : MonoBehaviour
{
    [SerializeField] public GameObject root;
    [SerializeField] public GameObject personTemplate;
    [SerializeField] public GameObject friends;

    int bid = 0;
    private List<GameObject> created = new List<GameObject>();

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
    }

    public void DeletePerson(int id)
    {
        Destroy(created[id]);
    }
}
