using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class IssueResponse
    {
        [DataMember]
        public long code;
        [DataMember]
        public String message;
        [DataMember]
        public String ntsConfirmNum;
    }
}
