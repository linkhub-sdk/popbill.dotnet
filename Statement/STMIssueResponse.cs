using System.Runtime.Serialization;

namespace Popbill.Statement
{
    [DataContract]
    public class STMIssueResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public string message;
        [DataMember]
        public string invoiceNum;
    }
}