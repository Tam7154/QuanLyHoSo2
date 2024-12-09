using Michsky.MUIP;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TongHopQuanNhanPanel : MonoBehaviour
{
    public SlotInTongHopQuanNhan curSelect;

    public Transform content;

    public List<Filter> filters;
    public List<DropdownMultiSelect> dropdownMultiSelects;

    public void ShowInfoPanel(InfoPerson infoPerson)
    {
        GameObject g = Instantiate(Resources.Load("Quan Nhan") as GameObject, GameObject.Find("Canvas").transform);
        Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        foreach (var item in infoPerson.infoPerson)
        {
            Transform gg = t.Find(item.Key);
            if (gg != null)
            {
                if (item.Key.StartsWith("toggle"))
                {
                    Toggle toggle = t.Find(item.Key).GetComponent<Toggle>();
                    toggle.isOn = bool.Parse(item.Value.ToString());
                }
                else
                {
                    TextMeshProUGUI text = t.Find(item.Key).GetComponent<TextMeshProUGUI>();
                    text.text += " " + item.Value;
                }
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

        List<InfoPerson> newInfo = Manager.Instance.infoPerson.ToList();
        for (int i = 0; i < newInfo.Count; i++)
        {
            for (int j = i + 1; j < newInfo.Count; j++)
            {
                int c = int.Parse(newInfo[i].infoPerson.GetValueOrDefault("don vi").ToString());
                int c2 = int.Parse(newInfo[j].infoPerson.GetValueOrDefault("don vi").ToString());
                if (c > c2)
                {
                    int temp = c;
                    newInfo[j].infoPerson["don vi"] = c;
                    newInfo[i].infoPerson["don vi"] = c2;
                }
            }
        }

        foreach (var item in newInfo)
        {
            bool b = true;
            foreach (var jtem in item.infoPerson)
            {
                foreach (var ntem in dropdownMultiSelects)
                {
                    if (jtem.Key == ntem.gameObject.name)
                    {
                        var v = jtem.Value;
                        foreach (var mtem in ntem.items)
                        {
                            if (mtem.itemName == v.ToString() &&
                                !mtem.isOn)
                            {
                                b = false;
                                break;
                            }
                        }
                        if (!b)
                        {
                            break;
                        }
                    }
                    if (!b)
                    {
                        break;
                    }
                }
                if (!b)
                {
                    break;
                }
            }

            if (b)
            {
                GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, content);
                slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("ho va ten").ToString();
                slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("don vi").ToString();
                slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("nam sinh").ToString();
                slot.name = item.infoPerson.GetValueOrDefault("id").ToString();
                slot.GetComponent<SlotInTongHopQuanNhan>().infoPerson = item;
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
            if (curSelect)
            {
                GameObject g = Instantiate(Resources.Load("Edit QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));

                Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
                foreach (var item in curSelect.infoPerson.infoPerson)
                {
                    if (item.Key.StartsWith("toggle"))
                    {
                        Transform gg = t.Find(item.Key.ToString());
                        Toggle toggle = gg.GetComponent<Toggle>();
                        toggle.isOn = bool.Parse(item.Value.ToString());
                    }
                    else
                    {
                        Transform gg = t.Find("ip " + item.Key);
                        if (gg != null)
                        {
                            TMP_InputField ip = gg.GetComponent<TMP_InputField>();
                            ip.text = item.Value.ToString();
                        }
                    }
                }

                //g.transform.GetChild(0).transform.Find("Submit").GetComponent<Button>().onClick.AddListener(() =>
                //{
                //    g.transform.parent.parent.GetComponent<ThemHoSoQuanNhan>().SubmitEdit();
                //});
                g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
                {
                    Destroy(g);
                });
            }
        }
    }
    public void Export()
    {
        NotificationManager.CreateNoti("Chức năng đang phát triển!");
    }
}