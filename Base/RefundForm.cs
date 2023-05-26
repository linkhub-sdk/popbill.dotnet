using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class RefundForm
    {
        [DataMember]
        public String ContactName;

        [DataMember]
        public String TEL;

        [DataMember]
        public String RequestPoint;

        [DataMember]
        public String AccountBank;

        [DataMember]
        public String AccountNum;

        [DataMember]
        public String AccountName;

        [DataMember]
        public String Reason;
    }
}
