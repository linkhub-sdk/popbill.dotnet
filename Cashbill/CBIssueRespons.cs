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
        public String message;
        [DataMember]
        public String confirmNum;
        [DataMember]
        public String tradeDate;
    }
}