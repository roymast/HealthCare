using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class MyClient : MySocket
    {
        public string MyName = "roy";
        public System.Action<Messages.DaysInsertedData> DaysInsertedDataReq;
        public System.Action<Messages.SpecificDayData> SpecificDayDataGot;
        
        public System.Action<Messages.LoginSignUpStatus> LoginSignUpStatus;

        Messages.BaseMessage BaseMessage;
        Messages.ErrorMessages ErrorMessages;
        // Start is called before the first frame update
        void Start()
        {
            DaysInsertedDataReq += (x) => HandleDaysInsertedDataReq(x);
            SpecificDayDataGot += (x) => HandleSpecificDayDataGot(x);
            ErrorMessages = new Messages.ErrorMessages();
            CalendarDay.OnCalendarDayClick += (dayData) => SendAskForSpecificDayData(dayData);
            DayDataUI.OnSave += (dayData) => SendSpecificDayData(dayData);
        }                                

        public override void ReceiveMessgae(Messages.BaseMessage baseMessage)
        {
            if (baseMessage.user_name != MyName)
                return;

            if (baseMessage is Messages.DaysInsertedData)
                DaysInsertedDataReq?.Invoke(baseMessage as Messages.DaysInsertedData);
            else if (baseMessage is Messages.SpecificDayData)
                SpecificDayDataGot?.Invoke(baseMessage as Messages.SpecificDayData);
            else if (baseMessage is Messages.LoginSignUpStatus)
                LoginSignUpStatus?.Invoke(baseMessage as Messages.LoginSignUpStatus);

            else
                Debug.Log("client: Not recognised msg");
        }

        void HandleDaysInsertedDataReq(Messages.DaysInsertedData daysInsertedDataMsg) { Debug.Log("HandleDaysInsertedDataReq"); }
        void HandleSpecificDayDataGot(Messages.SpecificDayData specificDayDataMsg) 
        { 
            Debug.Log($"HandleSpecificDayDataReq: {specificDayDataMsg.DayData.ToSring()}"); 
        }

        public Messages.BaseMessage CreateBaseMsg()
        {
            BaseMessage = new Messages.BaseMessage();
            BaseMessage.user_name = MyName;
            return BaseMessage;
        } 
        public string CheckForNameAndPassword(string name, string pass)            
        {
            if (name.Length < 4)
                return ErrorMessages.GetMsg(Messages.ErrorMessages.ErrorMsgID.NAME_ERROR);
            if (pass.Length < 4)
                return ErrorMessages.GetMsg(Messages.ErrorMessages.ErrorMsgID.PASS_ERROR);
            if (name.Contains('\'') || name.Contains('\"') || pass.Contains('\'') || pass.Contains('\"'))   //sql ingection
                return ErrorMessages.GetMsg(Messages.ErrorMessages.ErrorMsgID.Illegal);
            else
                return string.Empty;
        }
        void LoginSignUpClientError(string error)
        {
            Messages.LoginSignUpStatus loginSignUpStatus = new Messages.LoginSignUpStatus();
            loginSignUpStatus.user_name = MyName;
            loginSignUpStatus.isOk = false;
            loginSignUpStatus.errorMsg = error;
            LoginSignUpStatus?.Invoke(loginSignUpStatus);
        }
        public void SendSignUpMsg(string name, string pass)
        {
            string error = CheckForNameAndPassword(name, pass);
            if (error != string.Empty)
            {
                LoginSignUpClientError(error);
                return;
            }

            MyName = name;
            Messages.SignUpMsg signUpMsg = new Messages.SignUpMsg();
            signUpMsg.user_password = pass;
            SendGenericMessage(signUpMsg);
        }
        public void SendLoginMsg (string name, string pass)
        {
            string error = CheckForNameAndPassword(name, pass);
            Debug.Log(error);
            if (error != string.Empty)
            {
                LoginSignUpClientError(error);
                return;
            }

            MyName = name;
            Messages.LoginMsg loginMsg = new Messages.LoginMsg();            
            loginMsg.user_password = pass;
            SendGenericMessage(loginMsg);
        }
        public void SendAskForDaysInsertedData ()
        {            
            Messages.AskForDaysInsertedData msg = new Messages.AskForDaysInsertedData();
            SendGenericMessage(msg);
        }
        public void SendAskForSpecificDayData (DayData dayData)
        {                        
            Messages.AskForSpecificDayData msg = new Messages.AskForSpecificDayData();
            msg.day = dayData.day;
            msg.month = dayData.month;
            msg.year = dayData.year;
            SendGenericMessage(msg);
        }

        public void SendSpecificDayData(DayData dayData)
        {
            Messages.SpecificDayData msg = new Messages.SpecificDayData();
            msg.DayData = dayData;
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
