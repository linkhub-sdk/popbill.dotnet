using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class Message
    {
        [DataMember(Name="snd")]
        public string sendNum;
        [DataMember(Name = "sndnm")]
        public string senderName;
        [DataMember(Name="rcv")]
        public string receiveNum;
        [DataMember(Name="rcvnm")]
        public string receiveName;
        [DataMember(Name="sjt")]
        public string subject;
        [DataMember(Name="msg")]
        public string content;
    }
}
