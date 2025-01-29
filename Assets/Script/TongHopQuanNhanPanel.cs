using Michsky.MUIP;
using NPOI.XWPF.UserModel;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using Button = UnityEngine.UI.Button;
using Application = UnityEngine.Application;

public class TongHopQuanNhanPanel : MonoBehaviour
{
    public SlotInTongHopQuanNhan curSelect;

    public Transform content;

    public List<Filter> filters;
    public List<DropdownMultiSelect> dropdownMultiSelects;

    public void ShowInfoPanel(InfoMember infoMember, Dictionary<string, string> dic = null)
    {
        GameObject g = Instantiate(Resources.Load("Quan Nhan") as GameObject, GameObject.Find("Canvas").transform);
        Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;

        #region use LAN
        Dictionary<string, string> map = new Dictionary<string, string>();
        for (int i = 0; i < infoMember.nameInfo.Count; i++)
        {
            map.Add(infoMember.nameInfo[i], infoMember.info[i]);
        }

        foreach (var item in map)
        {
            Transform gg = t.Find(item.Key.ToLower());
            if (gg != null)
            {
                if (item.Key.StartsWith("toggle"))
                {
                    Toggle toggle = t.Find(item.Key).GetComponent<Toggle>();
                    toggle.isOn = bool.Parse(item.Value.ToString());
                }
                else if (item.Key == "avatar")
                {

                }
                else
                {
                    TextMeshProUGUI text = t.Find(item.Key).GetComponent<TextMeshProUGUI>();
                    text.text += " " + item.Value;
                }
            }
        }

        string path = $"{Application.streamingAssetsPath}\\{map["ho va ten"]} - {map["nam sinh"]} - {map["don vi"]}.png";
        if (File.Exists(path))
        {
            Transform avatar = t.GetChild(0);

            avatar.transform.GetChild(0).gameObject.SetActive(true);
            avatar.transform.GetChild(1).gameObject.SetActive(true);
            avatar.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);

            byte[] imageBytes = System.IO.File.ReadAllBytes(path);
            Texture2D txt = new Texture2D(3, 2);
            txt.LoadImage(imageBytes);
            avatar.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(txt.width / 2, txt.height / 2));
            avatar.transform.GetChild(2).gameObject.SetActive(false);
        }

        g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
            {
                Destroy(g);
            });

        g.transform.GetChild(0).transform.Find("Export").GetComponent<Button>().onClick.AddListener(() =>
        {
            g.GetComponent<InfoQuanNhan>().Export(curSelect);
        });
        #endregion
    }

    public void ShowInfoPanel(InfoPerson infoPerson)
    {
        GameObject g = Instantiate(Resources.Load("Quan Nhan") as GameObject, GameObject.Find("Canvas").transform);
        Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;

        #region use gg
        //foreach (var item in infoPerson.infoPerson)
        //{
        //    Transform gg = t.Find(item.Key);
        //    if (gg != null)
        //    {
        //        if (item.Key.StartsWith("toggle"))
        //        {
        //            Toggle toggle = t.Find(item.Key).GetComponent<Toggle>();
        //            toggle.isOn = bool.Parse(item.Value.ToString());
        //        }
        //        else
        //        {
        //            TextMeshProUGUI text = t.Find(item.Key).GetComponent<TextMeshProUGUI>();
        //            text.text += " " + item.Value;
        //        }
        //    }
        //}

        //g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    Destroy(g);
        //});

        //g.transform.GetChild(0).transform.Find("Export").GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    g.GetComponent<InfoQuanNhan>().Export(curSelect);
        //});
        #endregion
    }
    public void ShowListInfo()
    {
        #region use LAN
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }

        List<Dictionary<string, string>> map = new List<Dictionary<string, string>>();
        foreach (var item in Manager.Instance.infoMembers.members)
        {
            map.Add(new Dictionary<string, string>());
            for (int i = 0; i < item.nameInfo.Count; i++)
            {
                map[map.Count - 1].Add(item.nameInfo[i], item.info[i]);
            }
        }

        var sortedDictionaries = map.OrderBy(d => d["don vi"]).ToList();

        //foreach (var dictionaries in sortedDictionaries)
        //{
        //    print(dictionaries["don vi"]);
        //}

        foreach (var item in sortedDictionaries)
        //foreach (var item in Manager.Instance.infoMembers.members)
        {
            //Dictionary<string, string> infoDic = new Dictionary<string, string>();
            //for (int i = 0; i < item.nameInfo.Count; i++)
            //{
            //    infoDic.Add(item.nameInfo[i], item.info[i]);
            //}

            bool b = true;
            foreach (var jtem in item)
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
                slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text =item.GetValueOrDefault("ho va ten");
                slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =item.GetValueOrDefault("don vi").ToString();
                slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =item.GetValueOrDefault("nam sinh").ToString();
                //slot.name = infoDic.GetValueOrDefault("id").ToString();

                //slot.GetComponent<SlotInTongHopQuanNhan>().infoMember = item;
                slot.GetComponent<SlotInTongHopQuanNhan>().dicMember = item;
            }
        }


        //List<InfoPerson> newInfo = Manager.Instance.infoPerson.ToList();
        //for (int i = 0; i < newInfo.Count; i++)
        //{
        //    for (int j = i + 1; j < newInfo.Count; j++)
        //    {
        //        int c = int.Parse(newInfo[i].infoPerson.GetValueOrDefault("don vi").ToString());
        //        int c2 = int.Parse(newInfo[j].infoPerson.GetValueOrDefault("don vi").ToString());
        //        if (c > c2)
        //        {
        //            int temp = c;
        //            newInfo[j].infoPerson["don vi"] = c;
        //            newInfo[i].infoPerson["don vi"] = c2;
        //        }
        //    }
        //}

        //foreach (var item in newInfo)
        //{
        //    bool b = true;
        //    foreach (var jtem in item.infoPerson)
        //    {
        //        foreach (var ntem in dropdownMultiSelects)
        //        {
        //            if (jtem.Key == ntem.gameObject.name)
        //            {
        //                var v = jtem.Value;
        //                foreach (var mtem in ntem.items)
        //                {
        //                    if (mtem.itemName == v.ToString() &&
        //                        !mtem.isOn)
        //                    {
        //                        b = false;
        //                        break;
        //                    }
        //                }
        //                if (!b)
        //                {
        //                    break;
        //                }
        //            }
        //            if (!b)
        //            {
        //                break;
        //            }
        //        }
        //        if (!b)
        //        {
        //            break;
        //        }
        //    }

        //    if (b)
        //    {
        //        GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, content);
        //        slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("ho va ten").ToString();
        //        slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("don vi").ToString();
        //        slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("nam sinh").ToString();
        //        slot.name = item.infoPerson.GetValueOrDefault("id").ToString();
        //        slot.GetComponent<SlotInTongHopQuanNhan>().infoPerson = item;
        //    }
        //}
        #endregion

        #region  use gg
        //foreach (Transform item in content)
        //{
        //    Destroy(item.gameObject);
        //}

        //List<InfoPerson> newInfo = Manager.Instance.infoPerson.ToList();
        //for (int i = 0; i < newInfo.Count; i++)
        //{
        //    for (int j = i + 1; j < newInfo.Count; j++)
        //    {
        //        int c = int.Parse(newInfo[i].infoPerson.GetValueOrDefault("don vi").ToString());
        //        int c2 = int.Parse(newInfo[j].infoPerson.GetValueOrDefault("don vi").ToString());
        //        if (c > c2)
        //        {
        //            int temp = c;
        //            newInfo[j].infoPerson["don vi"] = c;
        //            newInfo[i].infoPerson["don vi"] = c2;
        //        }
        //    }
        //}

        //foreach (var item in newInfo)
        //{
        //    bool b = true;
        //    foreach (var jtem in item.infoPerson)
        //    {
        //        foreach (var ntem in dropdownMultiSelects)
        //        {
        //            if (jtem.Key == ntem.gameObject.name)
        //            {
        //                var v = jtem.Value;
        //                foreach (var mtem in ntem.items)
        //                {
        //                    if (mtem.itemName == v.ToString() &&
        //                        !mtem.isOn)
        //                    {
        //                        b = false;
        //                        break;
        //                    }
        //                }
        //                if (!b)
        //                {
        //                    break;
        //                }
        //            }
        //            if (!b)
        //            {
        //                break;
        //            }
        //        }
        //        if (!b)
        //        {
        //            break;
        //        }
        //    }

        //    if (b)
        //    {
        //        GameObject slot = Instantiate(Resources.Load("Slot in TongHopQuanNhan") as GameObject, content);
        //        slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("ho va ten").ToString();
        //        slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("don vi").ToString();
        //        slot.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.infoPerson.GetValueOrDefault("nam sinh").ToString();
        //        slot.name = item.infoPerson.GetValueOrDefault("id").ToString();
        //        slot.GetComponent<SlotInTongHopQuanNhan>().infoPerson = item;
        //    }
        //}
        #endregion
    }

    public void Add()
    {
        #region use LAN
        if (int.Parse(Manager.Instance.currentAccount.role) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            Instantiate(Resources.Load("Add QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
        }
        #endregion

        #region use gg
        //if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        //{
        //    NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        //}
        //else
        //{
        //    Instantiate(Resources.Load("Add QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
        //}
        #endregion
    }
    public void Delete()
    {
        #region use LAN
        if (int.Parse(Manager.Instance.currentAccount.role) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            if (curSelect != null)
            {
                Manager.Instance.Delete(curSelect.infoMember);
            }
        }
        #endregion
        #region use gg
        //if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        //{
        //    NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        //}
        //else
        //{
        //    if (curSelect != null)
        //    {
        //        Manager.Instance.Delete(curSelect.infoMember);
        //    }
        //}
        #endregion
    }
    public void Edit()
    {
        #region use LAN
        if (int.Parse(Manager.Instance.currentAccount.role) >= 3)
        {
            NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        }
        else
        {
            if (curSelect)
            {
                string id = "";

                GameObject g = Instantiate(Resources.Load("Edit QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
                g.GetComponent<ThemHoSoQuanNhan>().infoMember = curSelect.infoMember;

                Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;

                Dictionary<string, string> map = new Dictionary<string, string>();
                for (int i = 0; i < curSelect.infoMember.nameInfo.Count; i++)
                {
                    if (curSelect.infoMember.nameInfo[i] == "id")
                    {
                        id = curSelect.infoMember.info[i];
                        continue;
                    }
                    map.Add(curSelect.infoMember.nameInfo[i], curSelect.infoMember.info[i]);
                }

                foreach (var item in map)
                {
                    if (item.Key.StartsWith("toggle"))
                    {
                        Toggle toggle = t.Find(item.Key).GetComponent<Toggle>();
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
                        else
                        {
                            Debug.LogError("no input field: " + item.Key);
                        }
                        //TextMeshProUGUI text = t.Find(item.Key).GetComponent<TextMeshProUGUI>();
                        //text.text += " " + item.Value;
                    }
                }

                string path = $"{Application.streamingAssetsPath}\\{map["ho va ten"]} - {map["nam sinh"]} - {map["don vi"]}.png";
                if (File.Exists(path))
                {
                    Transform avatar = t.GetChild(0);

                    avatar.transform.GetChild(0).gameObject.SetActive(true);
                    avatar.transform.GetChild(1).gameObject.SetActive(true);
                    avatar.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                    byte[] imageBytes = System.IO.File.ReadAllBytes(path);
                    Texture2D txt = new Texture2D(3, 2);
                    txt.LoadImage(imageBytes);
                    avatar.transform.GetChild(1).GetComponent<Image>().sprite = Sprite.Create(txt, new Rect(0, 0, txt.width, txt.height), new Vector2(txt.width / 2, txt.height / 2));
                    avatar.transform.GetChild(2).gameObject.SetActive(false);
                }

                //g.transform.GetChild(0).transform.Find("Submit").GetComponent<Button>().onClick.AddListener(() =>
                //{
                //g.transform.parent.parent.GetComponent<ThemHoSoQuanNhan>().SubmitEdit(id);
                //});
                g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
                {
                    Destroy(g);
                });
            }
        }
        #endregion

        #region use gg
        //if (int.Parse(Manager.Instance.curUser.levelControl) >= 3)
        //{
        //    NotificationManager.CreateNoti("Quyền hạn của bạn chưa đủ!");
        //}
        //else
        //{
        //    if (curSelect)
        //    {
        //        GameObject g = Instantiate(Resources.Load("Edit QuanNhan") as GameObject, GameObject.Find("Canvas").transform.GetChild(0));

        //        Transform t = g.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform;
        //        foreach (var item in curSelect.infoPerson.infoPerson)
        //        {
        //            if (item.Key.StartsWith("toggle"))
        //            {
        //                Transform gg = t.Find(item.Key.ToString());
        //                Toggle toggle = gg.GetComponent<Toggle>();
        //                toggle.isOn = bool.Parse(item.Value.ToString());
        //            }
        //            else
        //            {
        //                Transform gg = t.Find("ip " + item.Key);
        //                if (gg != null)
        //                {
        //                    TMP_InputField ip = gg.GetComponent<TMP_InputField>();
        //                    ip.text = item.Value.ToString();
        //                }
        //            }
        //        }

        //        //g.transform.GetChild(0).transform.Find("Submit").GetComponent<Button>().onClick.AddListener(() =>
        //        //{
        //        //    g.transform.parent.parent.GetComponent<ThemHoSoQuanNhan>().SubmitEdit();
        //        //});
        //        g.transform.GetChild(0).transform.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        //        {
        //            Destroy(g);
        //        });
        //    }
        //}
        #endregion
    }

    public void Export()
    {
        NotificationManager.CreateNoti("Chức năng đang phát triển!");
    }
}