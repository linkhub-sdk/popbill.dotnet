using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill.Message
{
    [DataContract]
    public class MessageState
    {
        [DataMember]
        public string rNum;
        [DataMember]
        public string sn;
        [DataMember]
        public string stat;
        [DataMember]
        public string rlt;
        [DataMember]
        public string sDT;
        [DataMember]
        public string rDT;
        [DataMember]
        public string net;

        /* 구 전송결과 코드
         * 2018-10-02 
         */
        [DataMember]
        public string srt;
    }
}
