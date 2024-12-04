using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TongHopQuanNhanPanel : MonoBehaviour
{
    public SlotInTongHopQuanNhan curSelect;

    public Transform content;

    public List<Filter> filters;

    public void ShowInfo(InfoPerson infoPerson)
    {
        GameObject g = Instantiate(Resources.Load("Quan Nhan") as GameObject, GameObject.Find("Canvas").transform);
        Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        foreach (var item in infoPerson.infoPerson)
        {
            Transform gg = t.Find(item.Key);
            if (gg != null)
            {
                TextMeshProUGUI text = t.Find(item.Key).GetComponent<TextMeshProUGUI>();
                text.text += ": " + item.Value;
            }
        }

        g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(g);
        });

        g.transform.GetChild(0).transform.Find("Export").GetComponent<Button>().onClick.AddListener(() =>
        {
            NotificationManager.CreateNoti("Chức năng đang phát triển!");
        });
    }
    public void ShowListInfo()
    {
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Manager.Instance.infoPerson)
        {
            bool b = true;
            foreach (var jtem in filters)
            {
                if (!jtem.data[item.infoPerson[jtem.gameObject.name].ToString()])
                {
                    b = false;
                    break;
                }
            }

            if (b)
            {
                GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, content);
                slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("ten").ToString();
                slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("don vi").ToString();
                slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("nam sinh").ToString();
                slot.name = $"{item.infoPerson.GetValueOrDefault("ten")}:{item.infoPerson.GetValueOrDefault("don vi")}:{item.infoPerson.GetValueOrDefault("nam sinh")}";
                slot.GetComponent<SlotInTongHopQuanNhan>().infoPerson = item;

                //slot.g
            }
        }
    }

    public void Add()
    {
        if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            Instantiate(Resources.Load("Add QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
        }
    }
    public void Delete()
    {
        if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            if (curSelect != null)
            {
                Manager.Instance.Delete(curSelect.name);
            }
        }
    }
    public void Edit()
    {
        if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            Instantiate(Resources.Load("Add QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
        }
    }
    public void Export()
    {
        NotificationManager.CreateNoti("Chức năng đang phát triển!");
    }
}