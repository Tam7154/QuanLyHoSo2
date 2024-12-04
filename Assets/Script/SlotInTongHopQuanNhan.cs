using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInTongHopQuanNhan : MonoBehaviour
{

    TongHopQuanNhanPanel tongHopQuanNhanPanel;
    public InfoPerson infoPerson;

    float lastTime;
    private void Start()
    {
        tongHopQuanNhanPanel = FindObjectOfType<TongHopQuanNhanPanel>();
    }

    public void OnClick()
    {
        if (Time.time - lastTime <= .2f)
        {
            Manager.Instance.tongHopQuanNhanPanel.ShowInfo(infoPerson);
        }
        else
        {

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
        lastTime = Time.time;
    }
}
