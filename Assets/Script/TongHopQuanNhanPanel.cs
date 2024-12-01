using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongHopQuanNhanPanel : MonoBehaviour
{
    public SlotInTongHopQuanNhan curSelect;

    public Transform content;

    public List<Filter> filters;

    public void Add()
    {
        Instantiate(Resources.Load("Add QuanNhan")as GameObject, GameObject.Find("Canvas").transform.GetChild(0));
    }
    public void Delete()
    {
        Manager.Instance.Delete(curSelect.name);
    }
    public void Edit()
    {

    }
    public void Export()
    {

    }
}