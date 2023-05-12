using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public class MyNetwork : MonoBehaviour
    {
        public MyClient MyClient;
        public MyServer MyServer;                

        public void NetSendMessage(MySocket sender, Messages.BaseMessage baseMessage)
        {
            DebugPrinting(sender, baseMessage);
            if (sender is MyClient)
                MyServer.ReceiveMessgae(baseMessage);            
            else                            
                MyClient.ReceiveMessgae(baseMessage);            
        }
        void DebugPrinting(MySocket sender, Messages.BaseMessage baseMessage)
        {
            string senderStr = sender is MyClient ? "clientSends: \t" : "serverSends: \t";
            string msgType = "";
            if (baseMessage is Messages.AskForDaysInsertedData)
                msgType = "Messages.AskForDaysInsertedData";
            else if (baseMessage is Messages.AskForSpecificDayData)
                msgType = "Messages.AskForSpecificDayData";
            else if (baseMessage is Messages.DaysInsertedData)
                msgType = "Messages.DaysInsertedData";
            else if (baseMessage is Messages.LoginMsg)
                msgType = "Messages.LoginMsg";            
            else if (baseMessage is Messages.SignUpMsg)
                msgType = "Messages.SignUpMsg";
            else if (baseMessage is Messages.LoginSignUpStatus)
                msgType = "Messages.SignUpStatus";
            else if (baseMessage is Messages.SpecificDayData)
                msgType = "Messages.SpecificDayData";
            else
                msgType = "Now I really have no fucking idea what is going on";
            Debug.Log(senderStr + msgType);

        }
    }
}
