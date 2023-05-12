using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNetworking
{
    public abstract class MySocket : MonoBehaviour
    {
        [SerializeField] protected MyNetwork MyNetwork;        
        public virtual void NetSendMessage(Messages.BaseMessage baseMessage)
        {
            MyNetwork.NetSendMessage(this, baseMessage);
        }
        public abstract void ReceiveMessgae(Messages.BaseMessage baseMessage);                
    }
}
