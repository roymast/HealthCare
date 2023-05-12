using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SignUpPage : MonoBehaviour
{
    [SerializeField] MyNetworking.MyClient MyClient;
    [SerializeField] UIInput.UIInputText Name;
    [SerializeField] UIInput.UIInputText Pass;

    private void Start()
    {
        MyClient.SignUpStatus += (x) => OnSignUpStatus(x);
        Name.Init("User Name");
        Pass.Init("Password");
    }
    void OnSignUpStatus(bool isLogedIn)
    {
        Debug.Log("SignUp status: " + isLogedIn);
        if (isLogedIn)
           SceneManager.LoadScene("Main");        
    }
    public void Submit()
    {
        MyClient.SendSignUpMsg(Name.GetINput(), Pass.GetINput());
    }
}
