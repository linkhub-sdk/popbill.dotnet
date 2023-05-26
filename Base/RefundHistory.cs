using System;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Popbill
{
    [DataContract]
    public class RefundHistory
    {
        [DataMember]
        public String regDT;

        [DataMember]
        public String requestPoint;

        [DataMember]
        public String accountBank;

        [DataMember]
        public String accountNum;

        [DataMember]
        public String accountName;

        [DataMember]
        public int state;

        [DataMember]
        public String reason;
    }
}
