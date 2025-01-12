using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[System.Serializable]
public class InfoAccount
{
    public string id;
    public string password;
    public string role;
}

[System.Serializable]
public class Accounts
{
    public List<InfoAccount> accounts = new List<InfoAccount>();
}

[System.Serializable]
public class InfoMembers
{
    public List<InfoMember> members = new List<InfoMember>();
}

[System.Serializable]
public class InfoMember
{
    public string nameMember;
    public List<string> nameInfo = new List<string>();
    public List<string> info = new List<string>();
}

[System.Serializable]
public class SystemData
{
    public string currentId;
}

public static class Data
{
    public static void Save(string path, object data)
    {
        string content = JsonUtility.ToJson(data);
        string v = Convert.ToBase64String(Encoding.UTF8.GetBytes(content));
        File.WriteAllText(path, v);
        Debug.Log($"saved in {path}");
    }
    public static T Load<T>(string path)
    {
        string json = File.ReadAllText(path);
        string v = Encoding.UTF8.GetString(Convert.FromBase64String(json));
        var t = JsonUtility.FromJson<T>(v);
        return t;
    }
}
