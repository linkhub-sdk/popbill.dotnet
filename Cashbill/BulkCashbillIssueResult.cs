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
        public String mgtKey;
        [DataMember]
        public String confirmNum;
        [DataMember]
        public String tradeDate;
    }
}