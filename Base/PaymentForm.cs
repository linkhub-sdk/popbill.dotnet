using System;
using System.Text;
using System.Runtime.Serialization;

namespace Popbill
{
    [DataContract]
    public class PaymentForm
    {
        [DataMember]
        public String settlerName;

        [DataMember]
        public String settlerEmail;

        [DataMember]
        public String notifyHP;

        [DataMember]
        public String paymentName;

        [DataMember]
        public String settleCost;
        
    }
}
