using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSignUpPage : MonoBehaviour
{
    [SerializeField] MyNetworking.MyClient MyClient;
    [SerializeField] UIInput.UIInputText Name;
    [SerializeField] UIInput.UIInputText Pass;

    private void Start()
    {
        MyClient.LoginStatus += (x) => OnStatus(x);
        MyClient.SignUpStatus += (x) => OnStatus(x);
        Name.Init("User Name");
        Pass.Init("Password");
    }
    void OnStatus(bool isLogedIn)
    {       
        if (isLogedIn)
            SceneManager.LoadScene("Main");        
    }
    public void Submit()
    {
        MyClient.SendLoginMsg(Name.GetINput(), Pass.GetINput());
    }
    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
