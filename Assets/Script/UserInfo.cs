using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    public string name;
    public string displayName;
    public string description;
    public string icon;
    public string iconUrl;
    public string title;
    public string descriptionUrl;

    public string donVi;
    public string chucVu;
    public string levelControl;
    public bool isKey;

    public UserInfo(Dictionary<string, object> keyValuePairs)
    {
        levelControl = keyValuePairs["level control"].ToString();
        donVi = keyValuePairs["don vi"].ToString();
    }
}
