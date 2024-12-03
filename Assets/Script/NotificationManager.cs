using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class NotificationManager
{
    public static void CreateNoti(string s)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Notification Slot") as GameObject, GameObject.Find("Canvas").transform);
        TextMeshProUGUI text = g.GetComponentInChildren<TextMeshProUGUI>();
        text.text = s;
        text.ForceMeshUpdate();

        Vector2 newSize = g.GetComponent<RectTransform>().sizeDelta;
        newSize.x = text.textBounds.size.x + 50;
        g.GetComponent<RectTransform>().sizeDelta = newSize;
        GameObject.Destroy(g, 3);
    }
}
