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
        //string hoVaTen = scrollRect.content.Find("ip ho va ten").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string ngayNhapNgu= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string chucVu= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string donVi= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;
        //string namSinh= scrollRect.content.Find("ip nam sinh").GetComponent<TMP_InputField>().text;

        InfoPerson newInfo = new InfoPerson();
        newInfo.name = "a";
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
