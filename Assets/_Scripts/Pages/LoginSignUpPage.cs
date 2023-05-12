using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSignUpPage : MonoBehaviour
{
    [SerializeField] MyNetworking.MyClient MyClient;
    [SerializeField] UIInput.UIInputText Name;
    [SerializeField] UIInput.UIInputText Pass;
    [SerializeField] ErrorTextDisplayer errorText;    

    private void Start()
    {
        MyClient.LoginSignUpStatus += (x) => OnStatus(x);        
        Name.Init("User Name");
        Pass.Init("Password");
    }
    void OnStatus(MyNetworking.Messages.LoginSignUpStatus statusMsg)
    {
        if (statusMsg.isOk)
            SceneManager.LoadScene("Main");
        else
            errorText.OnDisplayError(statusMsg.errorMsg, "OnError");
    }    
    public void SendSignUp()
    {
        MyClient.SendSignUpMsg(Name.GetINput(), Pass.GetINput());
    }
    public void SendLogin()
    {
        MyClient.SendLoginMsg(Name.GetINput(), Pass.GetINput());
    }
    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
