using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemHoSoQuanNhan : MonoBehaviour
{
    public ScrollRect scrollRect;

    public void Add()
    {
        InfoPerson newInfo = new InfoPerson();
        newInfo.name = scrollRect.content.Find("ip ten").GetComponent<TMP_InputField>().text;
        newInfo.infoPerson = new Dictionary<string, object>();
        foreach (Transform item in scrollRect.content)
        {
            newInfo.infoPerson.Add(item.name.Remove(0, 3), item.GetComponent<TMP_InputField>().text);
        }


        Manager.Instance.Add(newInfo);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
