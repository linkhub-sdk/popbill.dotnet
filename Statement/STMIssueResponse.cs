using System;
using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class STMIssueResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public String message;
        [DataMember]
        public String invoiceNum;
    }
}