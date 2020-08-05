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
        [DataMember]
        public int? state;
        [DataMember]
        public string sendDT;
        [DataMember]
        public string receiveNum;
        [DataMember]
        public string receiveName;
        [DataMember]
        public string content;
        [DataMember]
        public int? result;
        [DataMember]
        public string resultDT;
        [DataMember]
        public string altContent;
        [DataMember]
        public int? altContentType;
        [DataMember]
        public string altSendType;
        [DataMember]
        public string altResultDT;
        [DataMember]
        public string contentType;
        [DataMember]
        public string altResult;
        [DataMember]
        public string altSendDT;
        [DataMember]
        public string receiptNum;
        [DataMember]
        public string requestNum;
        [DataMember]
        public string interOPRefKey;
    }
}
