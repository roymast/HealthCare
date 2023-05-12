using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class MyClient : MySocket
    {
        public string MyName = "roy";
        public System.Action<Messages.DaysInsertedData> DaysInsertedDataReq;
        public System.Action<Messages.SpecificDayData> SpecificDayDataReq;

        public System.Action<bool> LoginStatus;
        public System.Action<bool> SignUpStatus;

        Messages.BaseMessage BaseMessage;
        // Start is called before the first frame update
        void Start()
        {
            DaysInsertedDataReq += (x) => HandleDaysInsertedDataReq(x);
            SpecificDayDataReq += (x) => HandleSpecificDayDataReq(x);
        }                        

        public override void ReceiveMessgae(Messages.BaseMessage baseMessage)
        {
            if (baseMessage.user_name != MyName)
                return;

            if (baseMessage is Messages.DaysInsertedData)
                DaysInsertedDataReq?.Invoke(baseMessage as Messages.DaysInsertedData);
            else if (baseMessage is Messages.SpecificDayData)
                SpecificDayDataReq?.Invoke(baseMessage as Messages.SpecificDayData);
            else if (baseMessage is Messages.LoginStatus)
                LoginStatus?.Invoke((baseMessage as Messages.LoginStatus).isOk);
            else if (baseMessage is Messages.SignUpStatus)
                SignUpStatus?.Invoke((baseMessage as Messages.SignUpStatus).isOk);
            else
                Debug.Log("client: Not recognised msg");
        }

        void HandleDaysInsertedDataReq(Messages.DaysInsertedData daysInsertedDataMsg) { Debug.Log("HandleDaysInsertedDataReq"); }
        void HandleSpecificDayDataReq(Messages.SpecificDayData specificDayDataMsg) { Debug.Log("HandleSpecificDayDataReq"); }

        public Messages.BaseMessage CreateBaseMsg()
        {
            BaseMessage = new Messages.BaseMessage();
            BaseMessage.user_name = MyName;
            return BaseMessage;
        } 
        public void SendSignUpMsg(string name, string pass)
        {
            MyName = name;
            Messages.SignUpMsg signUpMsg = new Messages.SignUpMsg();
            signUpMsg.user_password = pass;
            SendGenericMessage(signUpMsg);
        }
        public void SendLoginMsg (string name, string pass)
        {
            MyName = name;
            Messages.LoginMsg loginMsg = new Messages.LoginMsg();            
            loginMsg.user_password = pass;
            SendGenericMessage(loginMsg);
        }
        public void SendAskForDaysInsertedData ()
        {
            CreateBaseMsg();
            Messages.AskForDaysInsertedData msg = new Messages.AskForDaysInsertedData();
            SendGenericMessage(msg);
        }
        public void SendAskForSpecificDayData ()
        {            
            CreateBaseMsg();
            Messages.AskForSpecificDayData msg = new Messages.AskForSpecificDayData();
            SendGenericMessage(msg);
        }
        public void SendGenericMessage(Messages.BaseMessage message)
        {
            message.user_name = MyName;
            NetSendMessage(message);
            BaseMessage = null;
        }

    }
}
