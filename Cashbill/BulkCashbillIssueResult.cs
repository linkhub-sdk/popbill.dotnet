using System;
using System.Runtime.Serialization;

namespace Popbill.Cashbill
{
    [DataContract]
    public class BulkCashbillIssueResult
    {
        [DataMember]
        public long code;
        [DataMember]
        public string mgtKey;
        [DataMember]
        public string confirmNum;
        [DataMember]
        public string tradeDate;
        [DataMember]
        public string tradeDT;
    }
}