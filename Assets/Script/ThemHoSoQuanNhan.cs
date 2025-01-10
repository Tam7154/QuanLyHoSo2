using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemHoSoQuanNhan : MonoBehaviour
{
    public ScrollRect scrollRect;

    public async void Add()
    {
        #region use LAN
        #endregion


        #region use gg
        //InfoPerson newInfo = new InfoPerson();
        //newInfo.name = scrollRect.content.Find("ip ho va ten").GetComponent<TMP_InputField>().text;
        //newInfo.infoPerson = new Dictionary<string, object>();
        //foreach (Transform item in scrollRect.content)
        //{
        //    if (!item.name.StartsWith("permanent"))
        //    {
        //        if (item.name.StartsWith("toggle"))
        //        {
        //            newInfo.infoPerson.Add(item.name, item.GetComponent<Toggle>().isOn);
        //        }
        //        else if (item.name.StartsWith("ip"))
        //        {
        //            newInfo.infoPerson.Add(item.name.Remove(0, 3), item.GetComponent<TMP_InputField>().text);
        //        }
        //    }
        //}

        //await Manager.Instance.Add(newInfo);
        #endregion
    }

    public async void SubmitEdit()
    {
        InfoPerson newInfo = new InfoPerson();
        newInfo.name = scrollRect.content.Find("ip ho va ten").GetComponent<TMP_InputField>().text;
        newInfo.infoPerson = new Dictionary<string, object>();
        foreach (Transform item in scrollRect.content)
        {
            if (!item.name.StartsWith("permanent"))
            {
                if (item.name.StartsWith("toggle"))
                {
                    //newInfo.infoPerson.Add(item.name.Remove(0, 7), item.GetComponent<Toggle>().isOn);
                    newInfo.infoPerson.Add(item.name, item.GetComponent<Toggle>().isOn);
                }
                else if (item.name.StartsWith("ip"))
                {
                    newInfo.infoPerson.Add(item.name.Remove(0, 3), item.GetComponent<TMP_InputField>().text);
                }
                else if (item.name.StartsWith("Avatar"))
                {

                }
            }
        }
        newInfo.infoPerson.Add("id", Manager.Instance.tongHopQuanNhanPanel.curSelect.
            infoPerson.infoPerson.GetValueOrDefault("id"));
        await Manager.Instance.Add(newInfo);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
