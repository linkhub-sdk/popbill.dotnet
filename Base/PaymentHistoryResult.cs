using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class PaymentHistory
    {
        [DataMember]
        public String productType;

        [DataMember]
        public String productName;

        [DataMember]
        public String settleType;

        [DataMember]
        public String settlerName;

        [DataMember]
        public String settlerEmail;

        [DataMember]
        public String settleCost;

        [DataMember]
        public String settlePoint;

        [DataMember]
        public int settleState;

        [DataMember]
        public String regDT;

        [DataMember]
        public String stateDT;
    }
}
