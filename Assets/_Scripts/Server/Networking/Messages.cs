using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class Messages
    {
        public class ErrorMessages
        {            
            public ErrorMessages()
            {

            }
            public Dictionary<ErrorMsgID, string> _idToString = new Dictionary<ErrorMsgID, string>()
            {
                {ErrorMsgID.NAME_ERROR, "Server: Name to short" },
                {ErrorMsgID.PASS_ERROR, "Server: Password to short" },
                {ErrorMsgID.TAKEN_NAME, "Server: Name already taken" },
                {ErrorMsgID.Incorrect, "Server: Name or password incorrect" },
                {ErrorMsgID.Illegal, "Server: Name or Password contains illegal characters" },

            };
            public enum ErrorMsgID
            {
                NAME_ERROR,
                PASS_ERROR,                
                TAKEN_NAME,
                Illegal,
                Incorrect
            } 
            public string GetMsg(ErrorMsgID errorMsgID)
            {
                string errorStr;
                _idToString.TryGetValue(errorMsgID, out errorStr);
                if (errorStr == string.Empty)
                    errorStr = "Unknown Error";
                return errorStr;
            }            
        }

        public class BaseMessage
        {
            public string user_name;
        }
        #region LoginSignUp
        public class LoginMsg : BaseMessage
        {
            public string user_password;
        }
        public class SignUpMsg : BaseMessage
        {
            public string user_password;
        }
        public class LoginSignUpStatus : BaseMessage
        {
            public bool isOk;
            public string errorMsg;
        }        
        #endregion

        #region DaysInserted
        public class DaysInsertedData : BaseMessage
        {
            public int year;
            public int month;
            public List<int> daysInserted;
        }
        public class AskForDaysInsertedData : BaseMessage
        {
            public int year;
            public int month;
        }
        #endregion

        #region SpecificDayData
        public class SpecificDayData : BaseMessage
        {            
            public DayData DayData;            
        }
        public class AskForSpecificDayData : BaseMessage
        {
            public int year;
            public int month;
            public int day;
        }
        #endregion
    }
}
