using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{
    public GameObject dropDown;
    public Dictionary<string, bool> data;
    public ScrollRect scrollRect;

    public void ShowFilter()
    {
        dropDown.SetActive(true);

        foreach (var item in data)
        {
            GameObject g = Instantiate(Resources.Load("Slot Boxcheck") as GameObject, scrollRect.content);
            g.GetComponentInChildren<TextMeshProUGUI>().text = item.Key;
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                print("A");
            });

        }

        print(data.Count);
    }
}
