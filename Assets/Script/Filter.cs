using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{
    public GameObject dropDown;
    public Dictionary<string, bool> data = new Dictionary<string, bool>();
    public ScrollRect scrollRect;

    public void ShowFilter()
    {
        foreach (Transform item in scrollRect.content)
        {
            Destroy(item.gameObject);
        }

        dropDown.GetComponent<RectTransform>().sizeDelta = new Vector2(400, data.Count * 110);
        dropDown.SetActive(true);

        foreach (var item in data)
        {
            GameObject g = Instantiate(Resources.Load("Slot Boxcheck") as GameObject, scrollRect.content);
            g.name = item.Key;
            g.GetComponentInChildren<TextMeshProUGUI>().text = item.Key;
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                //chọn và show các filter
                var v = data.GetValueOrDefault(item.Key);
                data[g.name] = !v;

                ShowColorBoxCheck();
                Manager.Instance.tongHopQuanNhanPanel.ShowListInfo();
            });
        }
        ShowColorBoxCheck();
    }

    void ShowColorBoxCheck()
    {
        foreach (Transform item in scrollRect.content)
        {
            bool v = data.GetValueOrDefault(item.name);
            Color c = Color.grey;
            if (v)
            {
                c = Color.green;
            }
            item.GetComponent<Image>().color = c;
        }
    }

    public void Close()
    {
        dropDown.SetActive(false);
    }
}
