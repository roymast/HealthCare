using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class MyServer : MySocket
    {
        [SerializeField] DataBase.MyDataBase MyDataBase;        

        public System.Action<Messages.SignUpMsg> SignUpReq;
        public System.Action<Messages.LoginMsg> LoginReq;
        public System.Action<Messages.AskForDaysInsertedData> AskForDaysInsertedDataReq;
        public System.Action<Messages.AskForSpecificDayData> AskForSpecificDayDataReq;

        // Start is called before the first frame update
        void Start()
        {
            SignUpReq +=  (x) => HandleSignUpReq(x);
            LoginReq += (x) => HandleLoginReq(x);
            AskForDaysInsertedDataReq += (x) => HandleAskForDaysInsertedDataReq(x);
            AskForSpecificDayDataReq += (x) => HandleAskForSpecificDayDataReq(x);
        }

        public override void ReceiveMessgae(Messages.BaseMessage baseMessage)
        {            
            if (baseMessage is Messages.SignUpMsg)
                SignUpReq?.Invoke(baseMessage as Messages.SignUpMsg);
            else if (baseMessage is Messages.LoginMsg)
                LoginReq?.Invoke(baseMessage as Messages.LoginMsg);
            else if (baseMessage is Messages.AskForDaysInsertedData)
                AskForDaysInsertedDataReq?.Invoke(baseMessage as Messages.AskForDaysInsertedData);
            else if (baseMessage is Messages.AskForSpecificDayData)
                AskForSpecificDayDataReq?.Invoke(baseMessage as Messages.AskForSpecificDayData);
            else
                Debug.Log("Server: Not recognized req");
        }

        void HandleSignUpReq(Messages.SignUpMsg msg) 
        {
            Messages.SignUpStatus statusMsg = new Messages.SignUpStatus();
            statusMsg.user_name = msg.user_name;
            statusMsg.isOk = MyDataBase.TrySignUp(msg);
            NetSendMessage(statusMsg);
            Debug.Log(statusMsg.isOk ? "Yay" : "Fuck!");

        }
        void HandleLoginReq(Messages.LoginMsg msg) 
        {
            Messages.LoginMsg dbData = MyDataBase.GetUser(msg);
            Messages.LoginStatus loginStatus = new Messages.LoginStatus();
            loginStatus.user_name = msg.user_name;
            loginStatus.isOk = (dbData != null && dbData.user_name != string.Empty);
            NetSendMessage(loginStatus);
        }
        void HandleAskForDaysInsertedDataReq(Messages.AskForDaysInsertedData msg) { Debug.Log("HandleAskForDaysInsertedDataReq: "+ msg.user_name); }
        void HandleAskForSpecificDayDataReq(Messages.AskForSpecificDayData msg) { Debug.Log("HandleAskForSpecificDayDataReq: "+ msg.user_name); }        
        
    }
}
