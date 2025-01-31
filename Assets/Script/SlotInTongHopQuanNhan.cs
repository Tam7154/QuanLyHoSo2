using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInTongHopQuanNhan : MonoBehaviour
{

    TongHopQuanNhanPanel tongHopQuanNhanPanel;

    [Header("gg")]
    public InfoPerson infoPerson;

    [Header("LAN")]
    public InfoMember infoMember;
    public Dictionary<string,string> dicMember;

    float lastTime;
    private void Start()
    {
        tongHopQuanNhanPanel = FindObjectOfType<TongHopQuanNhanPanel>();
    }

    public void OnClick()
    {
        if (Time.time - lastTime <= .4f)
        {
            Manager.Instance.tongHopQuanNhanPanel.ShowInfoPanel(infoMember, dicMember);
            //Manager.Instance.tongHopQuanNhanPanel.ShowInfoPanel(infoPerson);
            lastTime = Time.time;
        }
        else
        {
            lastTime = Time.time;
            if (tongHopQuanNhanPanel.curSelect)
            {
                if (tongHopQuanNhanPanel.curSelect == this)
                {
                    GetComponent<Image>().color = Color.white;
                    tongHopQuanNhanPanel.curSelect = null;
                    return;
                }

                tongHopQuanNhanPanel.curSelect.GetComponent<Image>().color = Color.white;
            }
            GetComponent<Image>().color = Color.green;
            tongHopQuanNhanPanel.curSelect = this;
        }
    }
}
