using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class MyServer : MySocket
    {
        [SerializeField] DataBase.MyDataBase MyDataBase;
        Messages.ErrorMessages ErrorMessages;

        public System.Action<Messages.SignUpMsg> SignUpReq;
        public System.Action<Messages.LoginMsg> LoginReq;
        public System.Action<Messages.AskForDaysInsertedData> AskForDaysInsertedDataReq;
        public System.Action<Messages.AskForSpecificDayData> AskForSpecificDayDataReq;
        public System.Action<Messages.SpecificDayData> SpecificDayData;

        // Start is called before the first frame update
        void Start()
        {
            SignUpReq +=  (x) => HandleSignUpReq(x);
            LoginReq += (x) => HandleLoginReq(x);
            AskForDaysInsertedDataReq += (x) => HandleAskForDaysInsertedDataReq(x);
            AskForSpecificDayDataReq += (x) => HandleAskForSpecificDayDataReq(x);
            SpecificDayData += (x) => HandleSpecificDayData(x);
            ErrorMessages = new Messages.ErrorMessages();
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
            else if (baseMessage is Messages.SpecificDayData)
                SpecificDayData?.Invoke(baseMessage as Messages.SpecificDayData);
            else
                Debug.Log("Server: Not recognized req");
        }

        void HandleSignUpReq(Messages.SignUpMsg msg) 
        {
            Messages.LoginSignUpStatus statusMsg = new Messages.LoginSignUpStatus();
            statusMsg.user_name = msg.user_name;
            statusMsg.isOk = MyDataBase.TrySignUp(msg);
            if (!statusMsg.isOk)
                statusMsg.errorMsg = ErrorMessages.GetMsg(Messages.ErrorMessages.ErrorMsgID.TAKEN_NAME);
            NetSendMessage(statusMsg);
            Debug.Log(statusMsg.isOk ? "Yay" : "Fuck!");

        }
        void HandleLoginReq(Messages.LoginMsg msg) 
        {
            Messages.LoginMsg dbData = MyDataBase.TryLogin(msg);
            Messages.LoginSignUpStatus loginStatus = new Messages.LoginSignUpStatus();
            loginStatus.user_name = msg.user_name;
            loginStatus.isOk = (dbData != null && dbData.user_name != string.Empty);
            if (!loginStatus.isOk)
                loginStatus.errorMsg = ErrorMessages.GetMsg(Messages.ErrorMessages.ErrorMsgID.Incorrect);
            NetSendMessage(loginStatus);
        }
        void HandleAskForDaysInsertedDataReq(Messages.AskForDaysInsertedData msg) { Debug.Log("HandleAskForDaysInsertedDataReq: "+ msg.user_name); }
        void HandleAskForSpecificDayDataReq(Messages.AskForSpecificDayData msg) 
        {
            Messages.SpecificDayData data = MyDataBase.GetSpecificDayData(msg);            
            NetSendMessage(data);
        }
        void HandleSpecificDayData(Messages.SpecificDayData msg)
        {
            Debug.Log($"day insert finished with: {MyDataBase.InsertNewDay(msg)}");
        }
        
    }
}
