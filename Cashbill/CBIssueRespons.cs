using System;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class CBIssueResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public string message;
        [DataMember]
        public string confirmNum;
        [DataMember]
        public string tradeDate;
        [DataMember]
        public string tradeDT;
    }
}