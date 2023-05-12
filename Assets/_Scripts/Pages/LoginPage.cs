using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoginPage : MonoBehaviour
{
    [SerializeField] MyNetworking.MyClient MyClient;
    [SerializeField] UIInput.UIInputText Name;
    [SerializeField] UIInput.UIInputText Pass;

    private void Start()
    {
        MyClient.LoginStatus += (x) => OnLoginStatus(x);
        Name.Init("User Name");
        Pass.Init("Password");
    }
    void OnLoginStatus(bool isLogedIn)
    {
        Debug.Log("Login status: " + isLogedIn);
    }
    public void Submit()
    {
        MyClient.SendLoginMsg(Name.GetINput(), Pass.GetINput());
    }
}
