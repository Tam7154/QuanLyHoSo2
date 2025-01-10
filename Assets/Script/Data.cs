using System.Collections;
using System.Collections.Generic;
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
    public List<string> nameInfo = new List<string>();
    public List<string> info = new List<string>();
}

public class Data : MonoBehaviour
{

}
