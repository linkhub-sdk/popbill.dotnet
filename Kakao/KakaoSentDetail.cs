using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Kakao
{
    [DataContract]
    public class KakaoSentDetail
    {
        [DataMember(Name = "state")]
        public int? _state;
        [DataMember(Name = "sendDT")]
        public string _sendDT;
        [DataMember(Name = "receiveNum")]
        public string _receiveNum;
        [DataMember(Name = "receiveName")]
        public string _receiveName;
        [DataMember(Name = "content")]
        public string _content;
        [DataMember(Name = "result")]
        public int? _result;
        [DataMember(Name = "resultDT")]
        public string _resultDT;
        [DataMember(Name = "altContent")]
        public string _altContent;
        [DataMember(Name = "altContentType")]
        public int? _altContentType;
        [DataMember(Name = "altSendType")]
        public string _altSendType;
        [DataMember(Name = "altResultDT")]
        public string _altResultDT;
        [DataMember(Name = "contentType")]
        public string _contentType;


        public int? state { get { return _state; } }
        public string sendDT { get { return _sendDT; } }
        public string receiveNum { get { return _receiveNum; } }
        public string receiveName { get { return _receiveName; } }
        public string content { get { return _content; } }
        public int? result { get { return _result; } }
        public string resultDT { get { return _resultDT; } }
        public string altContent { get { return _altContent; } }
        public int? altContentType { get { return _altContentType; } }
        public string altSendType { get { return _altSendType; } }
        public string altResultDT { get { return _altResultDT; } }
        public string contentType { get { return _contentType; } }
    }
}
