using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class Messages
    {
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
        public class LoginStatus : BaseMessage
        {
            public bool isOk;
        }
        public class SignUpStatus : BaseMessage
        {
            public bool isOk;
        }
        #endregion

        #region DaysInserted
        public class DaysInsertedData : BaseMessage
        {
            int year;
            int month;
            List<int> daysInserted;
        }
        public class AskForDaysInsertedData : BaseMessage
        {
            int year;
            int month;
        }
        #endregion

        #region SpecificDayData
        public class SpecificDayData : BaseMessage
        {
            int year;
            int month;
            int day;
            DayData DayData;
        }
        public class AskForSpecificDayData : BaseMessage
        {
            int year;
            int month;
            int day;
        }
        #endregion
    }
}
