using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemHoSoQuanNhan : MonoBehaviour
{
    public ScrollRect scrollRect;
    public string id;

    public InfoMember infoMember;
    public async void Add()
    {
        #region use LAN
        Dictionary<string, object> dic = new Dictionary<string, object>();
        foreach (Transform item in scrollRect.content)
        {
            if (!item.name.StartsWith("permanent"))
            {
                if (item.name.StartsWith("toggle"))
                {
                    dic.Add(item.name, item.GetComponent<Toggle>().isOn);
                }
                else if (item.name.StartsWith("ip"))
                {
                    dic.Add(item.name.Remove(0, 3), item.GetComponent<TMP_InputField>().text);
                }
            }
        }

        InfoMember newMember = new InfoMember();
        newMember.info = new List<string>();
        newMember.nameInfo = new List<string>();

        foreach (var item in dic)
        {
            newMember.nameInfo.Add(item.Key);
            newMember.info.Add(item.Value.ToString());
        }

        Manager.Instance.Add(newMember);
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

    public async void SubmitEdit(string id)
    {
        #region use LAN
        InfoMember newInfo = new InfoMember();
        foreach (Transform item in scrollRect.content)
        {
            if (!item.name.StartsWith("permanent"))
            {
                if (item.name.StartsWith("toggle"))
                {
                    newInfo.nameInfo.Add(item.name);
                    newInfo.info.Add(item.GetComponent<Toggle>().isOn.ToString());
                }
                else if (item.name.StartsWith("ip"))
                {
                    newInfo.nameInfo.Add(item.name.Remove(0, 3));
                    newInfo.info.Add(item.GetComponent<TMP_InputField>().text);
                }
            }
        }
        Manager.Instance.Delete(infoMember);
        Manager.Instance.Add(newInfo);

        //foreach (var item in Manager.Instance.infoMembers.members)
        //{
        //    for (int i = 0; i < item.nameInfo.Count; i++)
        //    {
        //        if (item.nameInfo[i] == "id" && item.info[i] == id)
        //        {
        //            foreach (Transform jtem in scrollRect.content)
        //            {
        //                if (!jtem.name.StartsWith("permanent"))
        //                {
        //                    if (jtem.name.StartsWith("toggle"))
        //                    {

        //                    }
        //                }
        //            }
        //            return;
        //        }
        //    }
        //}
        Manager.Instance.ReloadAllInfo();
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
        //            //newInfo.infoPerson.Add(item.name.Remove(0, 7), item.GetComponent<Toggle>().isOn);
        //            newInfo.infoPerson.Add(item.name, item.GetComponent<Toggle>().isOn);
        //        }
        //        else if (item.name.StartsWith("ip"))
        //        {
        //            newInfo.infoPerson.Add(item.name.Remove(0, 3), item.GetComponent<TMP_InputField>().text);
        //        }
        //        else if (item.name.StartsWith("Avatar"))
        //        {

        //        }
        //    }
        //}
        //newInfo.infoPerson.Add("id", Manager.Instance.tongHopQuanNhanPanel.curSelect.
        //    infoPerson.infoPerson.GetValueOrDefault("id"));
        //await Manager.Instance.Add(newInfo);
        #endregion
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
