using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class MessageResult
    {
        [DataMember(Name="state")]
        private int? _state = null;
        [DataMember(Name = "subject")]
        private string _subject = null;
        [DataMember(Name = "type")]
        private string _type = null;
        [DataMember(Name = "content")]
        private string _content = null;
        [DataMember(Name = "sendNum")]
        private string _sendNum = null;
        [DataMember(Name = "receiveNum")]
        private string _receiveNum = null;
        [DataMember(Name = "receiveName")]
        private string _receiveName = null;
        [DataMember(Name = "reserveDT")]
        private string _reserveDT = null;
        [DataMember(Name = "sendDT")]
        private string _sendDT = null;
        [DataMember(Name = "resultDT")]
        private string _resultDT = null;
        [DataMember(Name = "sendResult")]
        private string _sendResult = null;
        [DataMember(Name = "tranNet")]
        private string _tranNet = null;
        [DataMember(Name = "receiptDT")]
        private string _receiptDT = null;

        public int? state { get { return _state; } }
        public string subject { get { return _subject; } }
        public MessageType type { get { return (MessageType)Enum.Parse(typeof(MessageType),_type); } }
        public string content { get { return _content; } }
        public string sendNum { get { return _sendNum; } }
        public string receiveNum { get { return _receiveNum; } }
        public string receiveName { get { return _receiveName; } }
        public string reserveDT { get { return _reserveDT; } }
        public string sendDT { get { return _sendDT; } }
        public string resultDT { get { return _resultDT; } }
        public string sendResult { get { return _sendResult; } }
        public string tranNet { get { return _tranNet; } }
        public string receiptDT { get { return _receiptDT; } }

    }
}
