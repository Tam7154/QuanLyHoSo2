using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField ipUserName;
    public TMP_InputField ipPassword;

    private void Start()
    {
        ipPassword.onSubmit.AddListener((e) =>
        {
            LoginBtn();
        });
        ipUserName.onSubmit.AddListener((e) =>
        {
            LoginBtn();
        });
    }

    public void LoginBtn()
    {
        Manager.Instance.CheckForLogin(ipUserName.text, ipPassword.text);
    }
}
