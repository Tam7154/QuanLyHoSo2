using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotInTongHopQuanNhan : MonoBehaviour
{
    //Aoyama%20Aina
    //SUKE-080
    //MILK-067
    //sayama love
    TongHopQuanNhanPanel tongHopQuanNhanPanel;
    public InfoPerson infoPerson;
    private void Start()
    {
        tongHopQuanNhanPanel = FindObjectOfType<TongHopQuanNhanPanel>();
    }

    public void OnClick()
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
}
